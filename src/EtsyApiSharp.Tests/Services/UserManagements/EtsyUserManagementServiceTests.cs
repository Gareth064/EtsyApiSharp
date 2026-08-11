using EtsyApiSharp.Models;
using EtsyApiSharp.Services.UserManagements;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Text;
using Xunit;

namespace EtsyApiSharp.Tests.Services.UserManagements;

public class EtsyUserManagementServiceTests
{
    [Fact]
    public async Task GetUserAsync_ValidRequest_SendsRequiredHeadersAndParsesUser()
    {
        var handler = new StubHttpMessageHandler(request =>
        {
            Assert.Equal(HttpMethod.Get, request.Method);
            Assert.Equal(
                "https://openapi.etsy.com/v3/application/users/123",
                request.RequestUri?.ToString());
            Assert.Equal("Bearer", request.Headers.Authorization?.Scheme);
            Assert.Equal("123.access-token", request.Headers.Authorization?.Parameter);
            Assert.Equal("client-id:shared-secret", request.Headers.GetValues("x-api-key").Single());

            var response = JsonResponse(
                HttpStatusCode.OK,
                "{\"user_id\":123,\"primary_email\":null,\"first_name\":\"Ada\"," +
                "\"last_name\":\"Lovelace\",\"image_url_75x75\":null}");
            response.Headers.Add("X-Limit-Per-Second", "10");
            response.Headers.Add("X-Etsy-Request-Uuid", "request-id");
            return Task.FromResult(response);
        });
        var factory = new StubHttpClientFactory(handler);
        var service = CreateService(factory);

        var result = await service.GetUserAsync("123.access-token", 123);

        Assert.Equal(EtsyUserManagementService.HttpClientName, factory.RequestedName);
        Assert.True(result.Success);
        Assert.Equal(200, result.ResponseCode);
        Assert.Equal("https://openapi.etsy.com/v3/application/users/123", result.RequestedResource);
        Assert.Equal(10, result.ResponseHeaders.LimitPerSecond);
        Assert.Equal("request-id", result.ResponseHeaders.EtsyRequestUuid);
        Assert.NotNull(result.Data);
        Assert.Equal(123, result.Data.UserId);
        Assert.Equal("Ada", result.Data.FirstName);
        Assert.Null(result.Data.PrimaryEmail);
        Assert.Null(result.Message);
    }

    [Fact]
    public async Task GetMeAsync_ValidRequest_ParsesSelfResponse()
    {
        var handler = new StubHttpMessageHandler(request =>
        {
            Assert.Equal("https://openapi.etsy.com/v3/application/users/me", request.RequestUri?.ToString());
            return Task.FromResult(JsonResponse(
                HttpStatusCode.OK,
                "{\"user_id\":123,\"shop_id\":456}"));
        });
        var service = CreateService(new StubHttpClientFactory(handler));

        var result = await service.GetMeAsync("123.access-token");

        Assert.True(result.Success);
        Assert.NotNull(result.Data);
        Assert.Equal(123, result.Data.UserId);
        Assert.Equal(456, result.Data.ShopId);
    }

    [Fact]
    public async Task GetUserAsync_EtsyError_ReturnsActualStatusAndError()
    {
        var handler = new StubHttpMessageHandler(request => Task.FromResult(JsonResponse(
            HttpStatusCode.NotFound,
            "{\"error\":\"User not found\"}")));
        var service = CreateService(new StubHttpClientFactory(handler));

        var result = await service.GetUserAsync("123.access-token", 999);

        Assert.False(result.Success);
        Assert.Equal(404, result.ResponseCode);
        Assert.Null(result.Data);
        Assert.Equal("User not found", result.Message);
        Assert.Equal("https://openapi.etsy.com/v3/application/users/999", result.RequestedResource);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public async Task GetUserAsync_InvalidUserId_ThrowsArgumentOutOfRangeException(long userId)
    {
        var service = CreateService();

        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(
            () => service.GetUserAsync("123.access-token", userId));
    }

    [Fact]
    public async Task GetMeAsync_CancelledRequest_PropagatesCancellation()
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
            () => service.GetMeAsync("123.access-token", cancellationSource.Token));
    }

    [Fact]
    public void AddEtsyUserManagementServiceScoped_RegisteredService_ResolvesFromDependencyInjection()
    {
        var services = new ServiceCollection();
        services.AddEtsyUserManagementServiceScoped("client-id", "shared-secret");
        using var provider = services.BuildServiceProvider();
        using var scope = provider.CreateScope();

        var service = scope.ServiceProvider.GetRequiredService<IEtsyUserManagementService>();

        Assert.IsType<EtsyUserManagementService>(service);
    }

    private static EtsyUserManagementService CreateService(IHttpClientFactory? factory = null) => new(
        factory ?? new StubHttpClientFactory(new StubHttpMessageHandler(
            _ => throw new InvalidOperationException("No HTTP request was expected."))),
        "client-id",
        "shared-secret");

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
