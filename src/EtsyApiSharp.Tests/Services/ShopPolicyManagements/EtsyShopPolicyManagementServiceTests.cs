using EtsyApiSharp.Models.ShopPolicies;
using EtsyApiSharp.Services.ShopPolicyManagements;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Text;
using Xunit;

namespace EtsyApiSharp.Tests.Services.ShopPolicyManagements;

public class EtsyShopPolicyManagementServiceTests
{
    [Fact]
    public async Task ConsolidateShopReturnPoliciesAsync_ValidRequest_SendsShopsWriteOAuthAndForm()
    {
        var service = CreateService(new StubHttpClientFactory(new StubHttpMessageHandler(async request =>
        {
            Assert.Equal(HttpMethod.Post, request.Method);
            Assert.Equal("https://openapi.etsy.com/v3/application/shops/123/policies/return/consolidate", request.RequestUri?.ToString());
            AssertHeaders(request, true);
            Assert.Equal("source_return_policy_id=7&destination_return_policy_id=9", await request.Content!.ReadAsStringAsync());
            return JsonResponse(HttpStatusCode.OK, "{\"return_policy_id\":9,\"return_deadline\":30}");
        })));

        var result = await service.ConsolidateShopReturnPoliciesAsync("123.access-token", 123, new ConsolidateShopReturnPoliciesRequest { SourceReturnPolicyId = 7, DestinationReturnPolicyId = 9 });

        Assert.True(result.Success);
        Assert.Equal(9, result.Data?.ReturnPolicyId);
        Assert.Equal(30, result.Data?.ReturnDeadline);
    }

    [Fact]
    public async Task CreateShopReturnPolicyAsync_ValidRequest_SendsFormAndParsesPolicy()
    {
        var service = CreateService(new StubHttpClientFactory(new StubHttpMessageHandler(async request =>
        {
            Assert.Equal(HttpMethod.Post, request.Method);
            Assert.Equal("https://openapi.etsy.com/v3/application/shops/123/policies/return", request.RequestUri?.ToString());
            AssertHeaders(request, true);
            Assert.Equal("application/x-www-form-urlencoded", request.Content?.Headers.ContentType?.MediaType);
            Assert.Equal("accepts_returns=true&accepts_exchanges=false&return_deadline=30", await request.Content!.ReadAsStringAsync());
            return JsonResponse(HttpStatusCode.OK, "{\"return_policy_id\":8,\"shop_id\":123,\"accepts_returns\":true,\"accepts_exchanges\":false,\"return_deadline\":30}");
        })));

        var result = await service.CreateShopReturnPolicyAsync("123.access-token", 123, new CreateShopReturnPolicyRequest { AcceptsReturns = true, ReturnDeadline = 30 });

        Assert.True(result.Success);
        Assert.Equal(8, result.Data?.ReturnPolicyId);
        Assert.True(result.Data?.AcceptsReturns);
        Assert.False(result.Data?.AcceptsExchanges);
    }

    [Fact]
    public async Task GetShopReturnPoliciesAsync_UsesApiKeyWithoutOAuthAndParsesList()
    {
        var factory = new StubHttpClientFactory(new StubHttpMessageHandler(request =>
        {
            Assert.Equal(HttpMethod.Get, request.Method);
            Assert.Equal("https://openapi.etsy.com/v3/application/shops/123/policies/return", request.RequestUri?.ToString());
            AssertHeaders(request, false);
            return Task.FromResult(JsonResponse(HttpStatusCode.OK, "{\"count\":1,\"results\":[{\"return_policy_id\":8,\"shop_id\":123,\"accepts_returns\":true,\"accepts_exchanges\":true,\"return_deadline\":null}]}"));
        }));
        var service = CreateService(factory);

        var result = await service.GetShopReturnPoliciesAsync(123);

        Assert.Equal(EtsyShopPolicyManagementService.HttpClientName, factory.RequestedName);
        Assert.True(result.Success);
        Assert.Equal(1, result.Data?.Count);
        Assert.Equal(8, result.Data?.Results.Single().ReturnPolicyId);
        Assert.Null(result.Data?.Results.Single().ReturnDeadline);
    }

    [Fact]
    public async Task DeleteShopReturnPolicyAsync_ValidRequest_SendsShopsWriteOAuth()
    {
        var service = CreateService(new StubHttpClientFactory(new StubHttpMessageHandler(request =>
        {
            Assert.Equal(HttpMethod.Delete, request.Method);
            Assert.Equal("https://openapi.etsy.com/v3/application/shops/123/policies/return/8", request.RequestUri?.ToString());
            AssertHeaders(request, true);
            return Task.FromResult(new HttpResponseMessage(HttpStatusCode.NoContent));
        })));

        var result = await service.DeleteShopReturnPolicyAsync("123.access-token", 123, 8);

        Assert.True(result.Success);
    }

    [Fact]
    public async Task GetShopReturnPolicyAsync_UsesApiKeyWithoutOAuthAndParsesPolicy()
    {
        var service = CreateService(new StubHttpClientFactory(new StubHttpMessageHandler(request =>
        {
            Assert.Equal(HttpMethod.Get, request.Method);
            Assert.Equal("https://openapi.etsy.com/v3/application/shops/123/policies/return/8", request.RequestUri?.ToString());
            AssertHeaders(request, false);
            return Task.FromResult(JsonResponse(HttpStatusCode.OK, "{\"return_policy_id\":8,\"shop_id\":123,\"accepts_returns\":false,\"accepts_exchanges\":true,\"return_deadline\":14}"));
        })));

        var result = await service.GetShopReturnPolicyAsync(123, 8);

        Assert.True(result.Success);
        Assert.False(result.Data?.AcceptsReturns);
        Assert.True(result.Data?.AcceptsExchanges);
        Assert.Equal(14, result.Data?.ReturnDeadline);
    }

    [Fact]
    public async Task UpdateShopReturnPolicyAsync_ValidRequest_SendsShopsWriteOAuthAndForm()
    {
        var service = CreateService(new StubHttpClientFactory(new StubHttpMessageHandler(async request =>
        {
            Assert.Equal(HttpMethod.Put, request.Method);
            Assert.Equal("https://openapi.etsy.com/v3/application/shops/123/policies/return/8", request.RequestUri?.ToString());
            AssertHeaders(request, true);
            Assert.Equal("accepts_returns=false&accepts_exchanges=true", await request.Content!.ReadAsStringAsync());
            return JsonResponse(HttpStatusCode.OK, "{\"return_policy_id\":8,\"accepts_returns\":false,\"accepts_exchanges\":true}");
        })));

        var result = await service.UpdateShopReturnPolicyAsync("123.access-token", 123, 8, new UpdateShopReturnPolicyRequest { AcceptsExchanges = true });

        Assert.True(result.Success);
        Assert.True(result.Data?.AcceptsExchanges);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(8)]
    public async Task ReturnPolicyRequests_InvalidDeadline_ThrowsArgumentOutOfRangeException(long deadline)
    {
        var service = CreateService();

        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => service.CreateShopReturnPolicyAsync("token", 123, new CreateShopReturnPolicyRequest { ReturnDeadline = deadline }));
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => service.UpdateShopReturnPolicyAsync("token", 123, 8, new UpdateShopReturnPolicyRequest { ReturnDeadline = deadline }));
    }

    [Fact]
    public async Task ReturnPolicyOperations_InvalidIdsOrAccessToken_Throw()
    {
        var service = CreateService();

        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => service.GetShopReturnPoliciesAsync(0));
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => service.GetShopReturnPolicyAsync(123, 0));
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => service.DeleteShopReturnPolicyAsync("token", 123, 0));
        await Assert.ThrowsAsync<ArgumentException>(() => service.CreateShopReturnPolicyAsync(" ", 123, new CreateShopReturnPolicyRequest()));
        await Assert.ThrowsAsync<ArgumentNullException>(() => service.CreateShopReturnPolicyAsync("token", 123, null!));
        await Assert.ThrowsAsync<ArgumentNullException>(() => service.UpdateShopReturnPolicyAsync("token", 123, 8, null!));
        await Assert.ThrowsAsync<ArgumentNullException>(() => service.ConsolidateShopReturnPoliciesAsync("token", 123, null!));
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => service.ConsolidateShopReturnPoliciesAsync("token", 123, new ConsolidateShopReturnPoliciesRequest { SourceReturnPolicyId = 0, DestinationReturnPolicyId = 8 }));
    }

    [Fact]
    public async Task GetShopReturnPoliciesAsync_CancelledRequest_PropagatesCancellation()
    {
        var service = CreateService(new StubHttpClientFactory(new StubHttpMessageHandler(async (_, cancellationToken) =>
        {
            await Task.Delay(Timeout.InfiniteTimeSpan, cancellationToken);
            throw new InvalidOperationException("The cancelled request unexpectedly completed.");
        })));
        using var cancellationSource = new CancellationTokenSource();
        cancellationSource.Cancel();

        await Assert.ThrowsAnyAsync<OperationCanceledException>(() => service.GetShopReturnPoliciesAsync(123, cancellationSource.Token));
    }

    [Fact]
    public void ShopPolicyService_RegisteredAtEachLifetime_ResolvesFromDependencyInjection()
    {
        var services = new ServiceCollection();
        services.AddEtsyShopPolicyManagementServiceScoped("client-id", "shared-secret");
        services.AddEtsyShopPolicyManagementServiceTransient("client-id", "shared-secret");
        services.AddEtsyShopPolicyManagementServiceSingleton("client-id", "shared-secret");
        using var provider = services.BuildServiceProvider();

        var service = provider.GetRequiredService<IEtsyShopPolicyManagementService>();

        Assert.IsType<EtsyShopPolicyManagementService>(service);
    }

    private static void AssertHeaders(HttpRequestMessage request, bool expectsOAuth)
    {
        Assert.Equal("client-id:shared-secret", request.Headers.GetValues("x-api-key").Single());
        if (expectsOAuth)
            Assert.Equal("Bearer 123.access-token", request.Headers.Authorization?.ToString());
        else
            Assert.Null(request.Headers.Authorization);
    }

    private static EtsyShopPolicyManagementService CreateService(IHttpClientFactory? factory = null) => new(
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

        public StubHttpMessageHandler(Func<HttpRequestMessage, Task<HttpResponseMessage>> sendAsync)
            : this((request, _) => sendAsync(request))
        {
        }

        public StubHttpMessageHandler(Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>> sendAsync) => this.sendAsync = sendAsync;

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken) => sendAsync(request, cancellationToken);
    }
}
