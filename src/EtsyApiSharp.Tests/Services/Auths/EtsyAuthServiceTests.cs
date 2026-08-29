using EtsyApiSharp.Helpers;
using EtsyApiSharp.Models;
using EtsyApiSharp.Services.Auths;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Text;
using Xunit;

namespace EtsyApiSharp.Tests.Services.Auths;

public class EtsyAuthServiceTests
{
    private const string CodeVerifier = "dBjftJeZ4CVP-mB92K27uhbUJU1p1r_wW1gFWFOEjXk";
    private const string RedirectUrl = "https://example.com/callback?source=test";

    [Fact]
    public void BuildAuthorizationUrl_ValidRequest_UsesEncodedEtsyParameters()
    {
        var service = CreateService();

        var result = service.BuildAuthorizationUrl(CodeVerifier, "state+value");

        Assert.Equal(
            "https://www.etsy.com/oauth/connect" +
            "?response_type=code" +
            "&client_id=client%20id" +
            "&redirect_uri=https%3A%2F%2Fexample.com%2Fcallback%3Fsource%3Dtest" +
            "&scope=shops_r%20listings_w" +
            "&state=state%2Bvalue" +
            "&code_challenge=E9Melhoa2OwvFrEMTJguCHaoeK1t8URWbuGJSstw-cM" +
            "&code_challenge_method=S256",
            result);
    }

    [Theory]
    [InlineData("short")]
    [InlineData("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa!")]
    public void BuildAuthorizationUrl_InvalidCodeVerifier_ThrowsArgumentException(string codeVerifier)
    {
        var service = CreateService();

        Assert.Throws<ArgumentException>(() => service.BuildAuthorizationUrl(codeVerifier, "state"));
    }

    [Fact]
    public void BuildAuthorizationUrl_EmptyState_ThrowsArgumentException()
    {
        var service = CreateService();

        Assert.Throws<ArgumentException>(() => service.BuildAuthorizationUrl(CodeVerifier, string.Empty));
    }

    [Theory]
    [InlineData("http://example.com/callback")]
    [InlineData("Https://example.com/callback")]
    [InlineData("not-a-url")]
    public void Constructor_InvalidRedirectUrl_ThrowsArgumentException(string redirectUrl)
    {
        var factory = new StubHttpClientFactory(new StubHttpMessageHandler(
            _ => throw new InvalidOperationException("No HTTP request was expected.")));

        Assert.Throws<ArgumentException>(() => new EtsyAuthService(
            factory,
            "client-id",
            redirectUrl,
            new[] { Scope.shops_r }));
    }

    [Fact]
    public async Task GetFirstAccessTokenAsync_ValidRequest_PostsExpectedFormAndParsesResponse()
    {
        var handler = new StubHttpMessageHandler(async request =>
        {
            Assert.Equal(HttpMethod.Post, request.Method);
            Assert.Equal("https://api.etsy.com/v3/public/oauth/token", request.RequestUri?.ToString());
            Assert.Equal("application/x-www-form-urlencoded", request.Content?.Headers.ContentType?.MediaType);

            var body = await request.Content!.ReadAsStringAsync();
            Assert.Contains("grant_type=authorization_code", body);
            Assert.Contains("client_id=client+id", body);
            Assert.Contains("redirect_uri=https%3A%2F%2Fexample.com%2Fcallback%3Fsource%3Dtest", body);
            Assert.Contains("code=authorization+code", body);
            Assert.Contains($"code_verifier={CodeVerifier}", body);

            return JsonResponse(
                HttpStatusCode.OK,
                "{\"access_token\":\"123.access\",\"token_type\":\"Bearer\",\"expires_in\":3600," +
                "\"refresh_token\":\"123.refresh\",\"scope\":\"shops_r listings_w\"}");
        });
        var factory = new StubHttpClientFactory(handler);
        var service = CreateService(factory);

        var result = await service.GetFirstAccessTokenAsync("authorization code", CodeVerifier);

        Assert.Equal(EtsyAuthService.HttpClientName, factory.RequestedName);
        Assert.Equal("123.access", result.AccessToken);
        Assert.Equal("Bearer", result.TokenType);
        Assert.Equal(3600, result.ExpiresIn);
        Assert.Equal("123.refresh", result.RefreshToken);
        Assert.Equal("shops_r listings_w", result.Scope);
    }

    [Fact]
    public async Task GetRefreshAccessTokenAsync_EtsyError_ThrowsHttpRequestExceptionWithStatus()
    {
        var handler = new StubHttpMessageHandler(async request =>
        {
            var body = await request.Content!.ReadAsStringAsync();
            Assert.Contains("grant_type=refresh_token", body);
            Assert.Contains("client_id=client+id", body);
            Assert.Contains("refresh_token=123.refresh", body);
            Assert.DoesNotContain("redirect_uri", body);

            return JsonResponse(
                HttpStatusCode.BadRequest,
                "{\"error\":\"invalid_grant\",\"error_description\":\"The refresh token is invalid.\"}");
        });
        var service = CreateService(new StubHttpClientFactory(handler));

        var exception = await Assert.ThrowsAsync<HttpRequestException>(
            () => service.GetRefreshAccessTokenAsync("123.refresh"));

        Assert.Equal(HttpStatusCode.BadRequest, exception.StatusCode);
        Assert.Contains("invalid_grant: The refresh token is invalid.", exception.Message);
    }

    [Fact]
    public async Task GetRefreshAccessTokenAsync_CancelledRequest_PropagatesCancellation()
    {
        var handler = new StubHttpMessageHandler(async (_, cancellationToken) =>
        {
            await Task.Delay(Timeout.InfiniteTimeSpan, cancellationToken);
            throw new InvalidOperationException("The cancelled request unexpectedly completed.");
        });
        var service = CreateService(new StubHttpClientFactory(handler));
        using var cancellationSource = new CancellationTokenSource();
        cancellationSource.Cancel();

        await Assert.ThrowsAnyAsync<OperationCanceledException>(
            () => service.GetRefreshAccessTokenAsync("123.refresh", cancellationSource.Token));
    }

    [Fact]
    public void AddEtsyAuthServiceSingleton_RegisteredService_ResolvesFromDependencyInjection()
    {
        var services = new ServiceCollection();
        services.AddEtsyAuthServiceSingleton(
            "client-id",
            "https://example.com/callback",
            new[] { Scope.shops_r });
        using var provider = services.BuildServiceProvider();

        var first = provider.GetRequiredService<IEtsyAuthService>();
        var second = provider.GetRequiredService<IEtsyAuthService>();

        Assert.Same(first, second);
    }

    [Fact]
    public void CreateCodeVerifier_GeneratedValue_MeetsEtsyPkceRequirements()
    {
        var verifier = AuthHelper.CreateCodeVerifier();

        Assert.Equal(43, verifier.Length);
        Assert.Matches("^[A-Za-z0-9._~-]+$", verifier);
    }

    private static EtsyAuthService CreateService(IHttpClientFactory? factory = null) => new(
        factory ?? new StubHttpClientFactory(new StubHttpMessageHandler(
            _ => throw new InvalidOperationException("No HTTP request was expected."))),
        "client id",
        RedirectUrl,
        new[] { Scope.shops_r, Scope.listings_w });

    private static HttpResponseMessage JsonResponse(HttpStatusCode statusCode, string body) => new(statusCode)
    {
        Content = new StringContent(body, Encoding.UTF8, "application/json")
    };

    private sealed class StubHttpClientFactory : IHttpClientFactory
    {
        private readonly HttpClient httpClient;

        public StubHttpClientFactory(HttpMessageHandler handler)
        {
            httpClient = new HttpClient(handler);
        }

        public string? RequestedName { get; private set; }

        public HttpClient CreateClient(string name)
        {
            RequestedName = name;
            return httpClient;
        }
    }

    private sealed class StubHttpMessageHandler : HttpMessageHandler
    {
        private readonly Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>> sendAsync;

        public StubHttpMessageHandler(Func<HttpRequestMessage, Task<HttpResponseMessage>> sendAsync)
            : this((request, _) => sendAsync(request))
        {
        }

        public StubHttpMessageHandler(
            Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>> sendAsync)
        {
            this.sendAsync = sendAsync;
        }

        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken) => sendAsync(request, cancellationToken);
    }
}
