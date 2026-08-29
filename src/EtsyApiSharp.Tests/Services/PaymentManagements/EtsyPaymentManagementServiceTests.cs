using EtsyApiSharp.Models.Filters;
using EtsyApiSharp.Services.PaymentManagements;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Text;
using Xunit;

namespace EtsyApiSharp.Tests.Services.PaymentManagements;

public class EtsyPaymentManagementServiceTests
{
    [Fact]
    public async Task GetShopPaymentAccountLedgerEntryAsync_ValidRequest_SendsHeadersAndParsesEntry()
    {
        var factory = new StubHttpClientFactory(new StubHttpMessageHandler(request =>
        {
            Assert.Equal(HttpMethod.Get, request.Method);
            Assert.Equal("https://openapi.etsy.com/v3/application/shops/123/payment-account/ledger-entries/456", request.RequestUri?.ToString());
            AssertOAuthHeaders(request);
            return Task.FromResult(JsonResponse("{\"entry_id\":456,\"sequence_number\":3000000000,\"created_timestamp\":3000000001,\"payment_adjustments\":[{\"payment_adjustment_id\":4,\"payment_adjustment_items\":[{\"payment_adjustment_item_id\":5,\"amount\":6}]}]}"));
        }));
        var service = CreateService(factory);

        var result = await service.GetShopPaymentAccountLedgerEntryAsync("123.access-token", 123, 456);

        Assert.Equal(EtsyPaymentManagementService.HttpClientName, factory.RequestedName);
        Assert.True(result.Success);
        Assert.Equal(456, result.Data?.EntryId);
        Assert.Equal(3000000000L, result.Data?.SequenceNumber);
        Assert.Equal(3000000001L, result.Data?.CreatedTimestamp);
        Assert.Equal(5, result.Data?.PaymentAdjustments?.Single().PaymentAdjustmentItems?.Single().PaymentAdjustmentItemId);
    }

    [Fact]
    public async Task GetShopPaymentAccountLedgerEntriesAsync_Filter_SendsRequiredRangeAndPagination()
    {
        var service = CreateService(new StubHttpClientFactory(new StubHttpMessageHandler(request =>
        {
            Assert.Equal(HttpMethod.Get, request.Method);
            Assert.Equal("/v3/application/shops/123/payment-account/ledger-entries", request.RequestUri?.AbsolutePath);
            AssertOAuthHeaders(request);
            Assert.Contains("min_created=946684800", request.RequestUri?.Query);
            Assert.Contains("max_created=946684900", request.RequestUri?.Query);
            Assert.Contains("limit=50", request.RequestUri?.Query);
            Assert.Contains("offset=2", request.RequestUri?.Query);
            return Task.FromResult(JsonResponse("{\"count\":1,\"results\":[{\"entry_id\":456,\"amount\":3000000000}] }"));
        })));

        var result = await service.GetShopPaymentAccountLedgerEntriesAsync("123.access-token", 123,
            new GetShopPaymentAccountLedgerEntriesFilter { MinCreated = 946684800, MaxCreated = 946684900, Limit = 50, Offset = 2 });

        Assert.True(result.Success);
        Assert.Equal(1, result.Data?.Count);
        Assert.Equal(3000000000L, result.Data?.Results.Single().Amount);
    }

    [Fact]
    public async Task GetPaymentAccountLedgerEntryPaymentsAsync_Ids_SendsEncodedCommaSeparatedQuery()
    {
        var service = CreateService(new StubHttpClientFactory(new StubHttpMessageHandler(request =>
        {
            Assert.Equal(HttpMethod.Get, request.Method);
            Assert.Equal("/v3/application/shops/123/payment-account/ledger-entries/payments", request.RequestUri?.AbsolutePath);
            Assert.Contains("ledger_entry_ids=456%2C789", request.RequestUri?.Query);
            AssertOAuthHeaders(request);
            return Task.FromResult(JsonResponse("{\"count\":1,\"results\":[{\"payment_id\":8,\"created_timestamp\":3000000000}] }"));
        })));

        var result = await service.GetPaymentAccountLedgerEntryPaymentsAsync("123.access-token", 123, new long[] { 456, 789 });

        Assert.True(result.Success);
        Assert.Equal(8, result.Data?.Results.Single().PaymentId);
        Assert.Equal(3000000000L, result.Data?.Results.Single().CreatedTimestamp);
    }

    [Fact]
    public async Task GetShopPaymentByReceiptIdAsync_ValidRequest_SendsRouteAndParsesPayments()
    {
        var service = CreateService(new StubHttpClientFactory(new StubHttpMessageHandler(request =>
        {
            Assert.Equal(HttpMethod.Get, request.Method);
            Assert.Equal("https://openapi.etsy.com/v3/application/shops/123/receipts/456/payments", request.RequestUri?.ToString());
            AssertOAuthHeaders(request);
            return Task.FromResult(JsonResponse("{\"count\":1,\"results\":[{\"payment_id\":789}] }"));
        })));

        var result = await service.GetShopPaymentByReceiptIdAsync("123.access-token", 123, 456);

        Assert.True(result.Success);
        Assert.Equal(789, result.Data?.Results.Single().PaymentId);
    }

    [Fact]
    public async Task GetPaymentsAsync_Ids_SendsEncodedCommaSeparatedQueryAndParsesPayments()
    {
        var service = CreateService(new StubHttpClientFactory(new StubHttpMessageHandler(request =>
        {
            Assert.Equal(HttpMethod.Get, request.Method);
            Assert.Equal("/v3/application/shops/123/payments", request.RequestUri?.AbsolutePath);
            Assert.Contains("payment_ids=456%2C789", request.RequestUri?.Query);
            AssertOAuthHeaders(request);
            return Task.FromResult(JsonResponse("{\"count\":1,\"results\":[{\"payment_id\":456,\"payment_adjustments\":[{\"updated_timestamp\":3000000000}]}] }"));
        })));

        var result = await service.GetPaymentsAsync("123.access-token", 123, new long[] { 456, 789 });

        Assert.True(result.Success);
        Assert.Equal(456, result.Data?.Results.Single().PaymentId);
        Assert.Equal(3000000000L, result.Data?.Results.Single().PaymentAdjustments?.Single().UpdatedTimestamp);
    }

    [Theory]
    [InlineData(0, 946684800, 946684900, 25, 0)]
    [InlineData(123, 946684799, 946684900, 25, 0)]
    [InlineData(123, 946684900, 946684800, 25, 0)]
    [InlineData(123, 946684800, 946684900, 101, 0)]
    [InlineData(123, 946684800, 946684900, 25, -1)]
    public async Task GetShopPaymentAccountLedgerEntriesAsync_InvalidInput_ThrowsBeforeSending(
        long shopId, long minCreated, long maxCreated, int limit, int offset)
    {
        var service = CreateService();

        await Assert.ThrowsAnyAsync<ArgumentException>(() => service.GetShopPaymentAccountLedgerEntriesAsync("123.access-token", shopId,
            new GetShopPaymentAccountLedgerEntriesFilter { MinCreated = minCreated, MaxCreated = maxCreated, Limit = limit, Offset = offset }));
    }

    [Fact]
    public async Task PaymentEndpoints_InvalidIdsCollectionsAndToken_ThrowBeforeSending()
    {
        var service = CreateService();

        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => service.GetShopPaymentAccountLedgerEntryAsync("123.access-token", 0, 456));
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => service.GetShopPaymentByReceiptIdAsync("123.access-token", 123, 0));
        await Assert.ThrowsAsync<ArgumentException>(() => service.GetPaymentsAsync("123.access-token", 123, Array.Empty<long>()));
        await Assert.ThrowsAsync<ArgumentException>(() => service.GetPaymentAccountLedgerEntryPaymentsAsync("123.access-token", 123, new long[] { 0 }));
        await Assert.ThrowsAsync<ArgumentNullException>(() => service.GetPaymentsAsync("123.access-token", 123, null!));
        await Assert.ThrowsAsync<ArgumentException>(() => service.GetPaymentsAsync(string.Empty, 123, new long[] { 456 }));
    }

    [Fact]
    public async Task GetPaymentsAsync_CancelledRequest_PropagatesCancellation()
    {
        var service = CreateService(new StubHttpClientFactory(new StubHttpMessageHandler(async (_, cancellationToken) =>
        {
            await Task.Delay(Timeout.InfiniteTimeSpan, cancellationToken);
            throw new InvalidOperationException("The cancelled request unexpectedly completed.");
        })));
        using var cancellationSource = new CancellationTokenSource();
        cancellationSource.Cancel();

        await Assert.ThrowsAnyAsync<OperationCanceledException>(() => service.GetPaymentsAsync(
            "123.access-token", 123, new long[] { 456 }, cancellationSource.Token));
    }

    [Fact]
    public void AddEtsyPaymentManagementServiceTransient_RegisteredService_ResolvesFromDependencyInjection()
    {
        var services = new ServiceCollection();
        services.AddEtsyPaymentManagementServiceTransient("client-id", "shared-secret");
        using var provider = services.BuildServiceProvider();

        Assert.IsType<EtsyPaymentManagementService>(provider.GetRequiredService<IEtsyPaymentManagementService>());
    }

    private static EtsyPaymentManagementService CreateService(IHttpClientFactory? factory = null) => new(
        factory ?? new StubHttpClientFactory(new StubHttpMessageHandler(_ => throw new InvalidOperationException("No HTTP request was expected."))),
        "client-id", "shared-secret");

    private static HttpResponseMessage JsonResponse(string body) => new(HttpStatusCode.OK)
    {
        Content = new StringContent(body, Encoding.UTF8, "application/json")
    };

    private static void AssertOAuthHeaders(HttpRequestMessage request)
    {
        Assert.Equal("Bearer", request.Headers.Authorization?.Scheme);
        Assert.Equal("123.access-token", request.Headers.Authorization?.Parameter);
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
