using EtsyApiSharp.Models;
using EtsyApiSharp.Services.ShopEconomicOperatorManagements;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Text;
using Xunit;

namespace EtsyApiSharp.Tests.Services.ShopEconomicOperatorManagements;

public class EtsyShopEconomicOperatorManagementServiceTests
{
    [Fact]
    public async Task CreateShopEconomicOperatorAsync_ValidRequest_SendsListingsWriteOAuthAndForm()
    {
        var handler = new StubHttpMessageHandler(async request =>
        {
            Assert.Equal(HttpMethod.Post, request.Method);
            Assert.Equal("https://openapi.etsy.com/v3/application/shops/123/economic-operators", request.RequestUri?.ToString());
            Assert.Equal("Bearer 123.access-token", request.Headers.Authorization?.ToString());
            Assert.Equal("client-id:shared-secret", request.Headers.GetValues("x-api-key").Single());
            Assert.Equal("operator_name=Acme&email=safety%40acme.test&first_line=1+Main+Street&city=London&zip=SW1A+1AA&country_iso=GB&phone=%2B44+20+1234+5678", await request.Content!.ReadAsStringAsync());
            return JsonResponse(HttpStatusCode.OK, "{\"economic_operator_id\":7,\"operator_name\":\"Acme\"}");
        });
        var service = CreateService(new StubHttpClientFactory(handler));

        var result = await service.CreateShopEconomicOperatorAsync("123.access-token", 123, CreateRequest());

        Assert.True(result.Success);
        Assert.Equal(7, result.Data?.EconomicOperatorId);
    }

    [Fact]
    public async Task GetShopEconomicOperatorsAsync_ValidRequest_UsesListingsReadOAuthAndParsesList()
    {
        var factory = new StubHttpClientFactory(new StubHttpMessageHandler(request =>
        {
            Assert.Equal(HttpMethod.Get, request.Method);
            Assert.Equal("https://openapi.etsy.com/v3/application/shops/123/economic-operators", request.RequestUri?.ToString());
            Assert.Equal("Bearer 123.access-token", request.Headers.Authorization?.ToString());
            return Task.FromResult(JsonResponse(HttpStatusCode.OK, "{\"count\":1,\"results\":[{\"economic_operator_id\":7,\"country_iso\":\"GB\"}]}"));
        }));
        var service = CreateService(factory);

        var result = await service.GetShopEconomicOperatorsAsync("123.access-token", 123);

        Assert.Equal(EtsyShopEconomicOperatorManagementService.HttpClientName, factory.RequestedName);
        Assert.True(result.Success);
        Assert.Equal("GB", result.Data?.Results.Single().CountryIso);
    }

    [Fact]
    public async Task GetShopEconomicOperatorAsync_ValidRequest_UsesDocumentedRoute()
    {
        var handler = new StubHttpMessageHandler(request =>
        {
            Assert.Equal(HttpMethod.Get, request.Method);
            Assert.Equal("https://openapi.etsy.com/v3/application/shops/123/economic-operators/7", request.RequestUri?.ToString());
            return Task.FromResult(JsonResponse(HttpStatusCode.OK, "{\"economic_operator_id\":7,\"operator_name\":\"Acme\"}"));
        });
        var service = CreateService(new StubHttpClientFactory(handler));

        var result = await service.GetShopEconomicOperatorAsync("123.access-token", 123, 7);

        Assert.True(result.Success);
        Assert.Equal("Acme", result.Data?.OperatorName);
    }

    [Fact]
    public async Task UpdateShopEconomicOperatorAsync_ValidRequest_SendsListingsWriteOAuthAndForm()
    {
        var handler = new StubHttpMessageHandler(async request =>
        {
            Assert.Equal(HttpMethod.Patch, request.Method);
            Assert.Equal("https://openapi.etsy.com/v3/application/shops/123/economic-operators/7", request.RequestUri?.ToString());
            Assert.Equal("Bearer 123.access-token", request.Headers.Authorization?.ToString());
            Assert.Equal("operator_name=Acme+Ltd", await request.Content!.ReadAsStringAsync());
            return JsonResponse(HttpStatusCode.OK, "{\"economic_operator_id\":7,\"operator_name\":\"Acme Ltd\"}");
        });
        var service = CreateService(new StubHttpClientFactory(handler));

        var result = await service.UpdateShopEconomicOperatorAsync("123.access-token", 123, 7, new UpdateShopEconomicOperatorRequest { OperatorName = "Acme Ltd" });

        Assert.True(result.Success);
        Assert.Equal("Acme Ltd", result.Data?.OperatorName);
    }

    [Fact]
    public async Task ShopEconomicOperatorOperations_InvalidInput_ThrowsBeforeRequest()
    {
        var service = CreateService();

        await Assert.ThrowsAsync<ArgumentException>(() => service.GetShopEconomicOperatorsAsync(" ", 123));
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => service.GetShopEconomicOperatorAsync("token", 0, 7));
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => service.UpdateShopEconomicOperatorAsync("token", 123, 0, new UpdateShopEconomicOperatorRequest { City = "London" }));
        await Assert.ThrowsAsync<ArgumentException>(() => service.CreateShopEconomicOperatorAsync("token", 123, new CreateShopEconomicOperatorRequest()));
        await Assert.ThrowsAsync<ArgumentException>(() => service.UpdateShopEconomicOperatorAsync("token", 123, 7, new UpdateShopEconomicOperatorRequest()));
    }

    [Fact]
    public async Task GetShopEconomicOperatorsAsync_CancelledRequest_PropagatesCancellation()
    {
        var service = CreateService(new StubHttpClientFactory(new StubHttpMessageHandler(async (_, cancellationToken) =>
        {
            await Task.Delay(Timeout.InfiniteTimeSpan, cancellationToken);
            throw new InvalidOperationException("The cancelled request unexpectedly completed.");
        })));
        using var cancellationSource = new CancellationTokenSource();
        cancellationSource.Cancel();

        await Assert.ThrowsAnyAsync<OperationCanceledException>(() => service.GetShopEconomicOperatorsAsync("token", 123, cancellationSource.Token));
    }

    [Fact]
    public void AddEtsyShopEconomicOperatorManagementServiceTransient_RegisteredService_ResolvesFromDependencyInjection()
    {
        var services = new ServiceCollection();
        services.AddEtsyShopEconomicOperatorManagementServiceTransient("client-id", "shared-secret");
        using var provider = services.BuildServiceProvider();

        Assert.IsType<EtsyShopEconomicOperatorManagementService>(provider.GetRequiredService<IEtsyShopEconomicOperatorManagementService>());
    }

    private static CreateShopEconomicOperatorRequest CreateRequest() => new()
    {
        OperatorName = "Acme",
        Email = "safety@acme.test",
        FirstLine = "1 Main Street",
        City = "London",
        Zip = "SW1A 1AA",
        CountryIso = "GB",
        Phone = "+44 20 1234 5678"
    };

    private static EtsyShopEconomicOperatorManagementService CreateService(IHttpClientFactory? factory = null) => new(
        factory ?? new StubHttpClientFactory(new StubHttpMessageHandler(_ => throw new InvalidOperationException("No HTTP request was expected."))),
        "client-id",
        "shared-secret");

    private static HttpResponseMessage JsonResponse(HttpStatusCode statusCode, string body) => new(statusCode)
    {
        Content = new StringContent(body, Encoding.UTF8, "application/json")
    };

    private sealed class StubHttpClientFactory : IHttpClientFactory
    {
        private readonly HttpClient httpClient;
        public StubHttpClientFactory(HttpMessageHandler handler) => httpClient = new HttpClient(handler);
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
        public StubHttpMessageHandler(Func<HttpRequestMessage, Task<HttpResponseMessage>> sendAsync) : this((request, _) => sendAsync(request)) { }
        public StubHttpMessageHandler(Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>> sendAsync) => this.sendAsync = sendAsync;
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken) => sendAsync(request, cancellationToken);
    }
}
