using EtsyApiSharp.Models.Filters;
using EtsyApiSharp.Services.ReviewManagements;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Text;
using Xunit;

namespace EtsyApiSharp.Tests.Services.ReviewManagements;

public class EtsyReviewManagementServiceTests
{
    [Fact]
    public async Task GetReviewsByListingAsync_Filter_SendsPublicRequestAndParsesListingReviews()
    {
        var factory = new StubHttpClientFactory(new StubHttpMessageHandler(request =>
        {
            Assert.Equal(HttpMethod.Get, request.Method);
            Assert.Equal("/v3/application/listings/456/reviews", request.RequestUri?.AbsolutePath);
            Assert.Contains("limit=50", request.RequestUri?.Query);
            Assert.Contains("offset=2", request.RequestUri?.Query);
            Assert.Contains("min_created=946684800", request.RequestUri?.Query);
            Assert.Contains("max_created=3000000000", request.RequestUri?.Query);
            AssertPublicHeaders(request);
            return Task.FromResult(JsonResponse("{\"count\":1,\"results\":[{\"listing_id\":456,\"rating\":5,\"review\":\"Lovely\",\"created_timestamp\":3000000000,\"updated_timestamp\":3000000001}]}"));
        }));
        var service = CreateService(factory);

        var result = await service.GetReviewsByListingAsync(456, new GetReviewsFilter
        {
            Limit = 50,
            Offset = 2,
            MinCreated = 946684800,
            MaxCreated = 3000000000
        });

        Assert.Equal(EtsyReviewManagementService.HttpClientName, factory.RequestedName);
        Assert.True(result.Success);
        Assert.Equal(1, result.Data?.Count);
        Assert.Equal("Lovely", result.Data?.Results.Single().Review);
        Assert.Equal(3000000000L, result.Data?.Results.Single().CreatedTimestamp);
        Assert.Equal(3000000001L, result.Data?.Results.Single().UpdatedTimestamp);
    }

    [Fact]
    public async Task GetReviewsByShopAsync_NoFilter_SendsRouteAndParsesTransactionReviews()
    {
        var factory = new StubHttpClientFactory(new StubHttpMessageHandler(request =>
        {
            Assert.Equal(HttpMethod.Get, request.Method);
            Assert.Equal("https://openapi.etsy.com/v3/application/shops/123/reviews", request.RequestUri?.ToString());
            AssertPublicHeaders(request);
            return Task.FromResult(JsonResponse("{\"count\":1,\"results\":[{\"shop_id\":123,\"listing_id\":456,\"transaction_id\":789,\"buyer_user_id\":987,\"rating\":4,\"create_timestamp\":3000000000,\"created_timestamp\":3000000002,\"update_timestamp\":3000000001,\"updated_timestamp\":3000000003}]}"));
        }));
        var service = CreateService(factory);

        var result = await service.GetReviewsByShopAsync(123);

        Assert.True(result.Success);
        Assert.Equal(789, result.Data?.Results.Single().TransactionId);
        Assert.Equal(987, result.Data?.Results.Single().BuyerUserId);
        Assert.Equal(3000000000L, result.Data?.Results.Single().CreateTimestamp);
        Assert.Equal(3000000002L, result.Data?.Results.Single().CreatedTimestamp);
        Assert.Equal(3000000001L, result.Data?.Results.Single().UpdateTimestamp);
        Assert.Equal(3000000003L, result.Data?.Results.Single().UpdatedTimestamp);
    }

    [Theory]
    [InlineData(0L, 25, 0, null, null)]
    [InlineData(123L, 0, 0, null, null)]
    [InlineData(123L, 101, 0, null, null)]
    [InlineData(123L, 25, -1, null, null)]
    [InlineData(123L, 25, 0, 946684799L, null)]
    [InlineData(123L, 25, 0, null, 946684799L)]
    [InlineData(123L, 25, 0, 946684900L, 946684800L)]
    public async Task GetReviewsByListingAsync_InvalidInput_ThrowsBeforeSending(
        long listingId, int limit, int offset, long? minCreated, long? maxCreated)
    {
        var service = CreateService();

        await Assert.ThrowsAnyAsync<ArgumentException>(() => service.GetReviewsByListingAsync(listingId, new GetReviewsFilter
        {
            Limit = limit,
            Offset = offset,
            MinCreated = minCreated,
            MaxCreated = maxCreated
        }));
    }

    [Fact]
    public async Task GetReviewsByShopAsync_InvalidShopId_ThrowsBeforeSending()
    {
        var service = CreateService();

        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => service.GetReviewsByShopAsync(0));
    }

    [Fact]
    public async Task GetReviewsByShopAsync_CancelledRequest_PropagatesCancellation()
    {
        var service = CreateService(new StubHttpClientFactory(new StubHttpMessageHandler(async (_, cancellationToken) =>
        {
            await Task.Delay(Timeout.InfiniteTimeSpan, cancellationToken);
            throw new InvalidOperationException("The cancelled request unexpectedly completed.");
        })));
        using var cancellationSource = new CancellationTokenSource();
        cancellationSource.Cancel();

        await Assert.ThrowsAnyAsync<OperationCanceledException>(() => service.GetReviewsByShopAsync(123, cancellationToken: cancellationSource.Token));
    }

    [Fact]
    public void AddEtsyReviewManagementServiceTransient_RegisteredService_ResolvesFromDependencyInjection()
    {
        var services = new ServiceCollection();
        services.AddEtsyReviewManagementServiceTransient("client-id", "shared-secret");
        using var provider = services.BuildServiceProvider();

        Assert.IsType<EtsyReviewManagementService>(provider.GetRequiredService<IEtsyReviewManagementService>());
    }

    private static EtsyReviewManagementService CreateService(IHttpClientFactory? factory = null) => new(
        factory ?? new StubHttpClientFactory(new StubHttpMessageHandler(_ => throw new InvalidOperationException("No HTTP request was expected."))),
        "client-id", "shared-secret");

    private static HttpResponseMessage JsonResponse(string body) => new(HttpStatusCode.OK)
    {
        Content = new StringContent(body, Encoding.UTF8, "application/json")
    };

    private static void AssertPublicHeaders(HttpRequestMessage request)
    {
        Assert.Null(request.Headers.Authorization);
        Assert.Equal("client-id:shared-secret", request.Headers.GetValues("x-api-key").Single());
    }

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
            : this((request, _) => sendAsync(request)) { }

        public StubHttpMessageHandler(Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>> sendAsync) => this.sendAsync = sendAsync;

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken) => sendAsync(request, cancellationToken);
    }
}
