using EtsyApiSharp.Models;
using EtsyApiSharp.Models.Filters;
using EtsyApiSharp.Models.ShopReceipts.Enums;
using EtsyApiSharp.Services.ReceiptManagements;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Text;
using System.Text.Json;
using Xunit;

namespace EtsyApiSharp.Tests.Services.ReceiptManagements;

public class EtsyReceiptManagementServiceTests
{
    [Fact]
    public async Task GetShopReceiptAsync_ValidRequest_SendsRequiredHeadersAndParsesReceipt()
    {
        var handler = new StubHttpMessageHandler(request =>
        {
            Assert.Equal(HttpMethod.Get, request.Method);
            Assert.Equal(
                "https://openapi.etsy.com/v3/application/shops/123/receipts/456?legacy=true",
                request.RequestUri?.ToString());
            Assert.Equal("Bearer", request.Headers.Authorization?.Scheme);
            Assert.Equal("123.access-token", request.Headers.Authorization?.Parameter);
            Assert.Equal("client-id:shared-secret", request.Headers.GetValues("x-api-key").Single());
            return Task.FromResult(JsonResponse(
                HttpStatusCode.OK,
                "{\"receipt_id\":456,\"created_timestamp\":3000000000,\"gift_sender\":\"Grace\",\"transactions\":[{\"variations\":[{\"question_id\":42}]}],\"refunds\":[{\"created_timestamp\":3000000000}]}"));
        });
        var factory = new StubHttpClientFactory(handler);
        var service = CreateService(factory);

        var result = await service.GetShopReceiptAsync("123.access-token", 123, 456, legacy: true);

        Assert.Equal(EtsyReceiptManagementService.HttpClientName, factory.RequestedName);
        Assert.True(result.Success);
        Assert.Equal(456, result.Data?.ReceiptId);
        Assert.Equal(3000000000L, result.Data?.CreatedTimestamp);
        Assert.Equal("Grace", result.Data?.GiftSender);
        Assert.Equal(42, result.Data?.Transactions.Single().Variations.Single().QuestionId);
        Assert.Equal(3000000000L, result.Data?.Refunds.Single().CreatedTimestamp);
        Assert.Equal(
            "https://openapi.etsy.com/v3/application/shops/123/receipts/456?legacy=true",
            result.RequestedResource);
    }

    [Fact]
    public async Task GetShopReceiptsAsync_PopulatedFilter_SendsEverySupportedQueryParameter()
    {
        var handler = new StubHttpMessageHandler(request =>
        {
            Assert.Equal(HttpMethod.Get, request.Method);
            AssertOAuthHeaders(request);
            Assert.Equal("/v3/application/shops/123/receipts", request.RequestUri?.AbsolutePath);
            var query = request.RequestUri?.Query ?? string.Empty;
            Assert.Contains("limit=50", query);
            Assert.Contains("offset=5", query);
            Assert.Contains("min_created=946684800", query);
            Assert.Contains("max_created=946684900", query);
            Assert.Contains("min_last_modified=946685000", query);
            Assert.Contains("max_last_modified=946685100", query);
            Assert.Contains("sort_on=receipt_id", query);
            Assert.Contains("sort_order=asc", query);
            Assert.Contains("was_paid=true", query);
            Assert.Contains("was_shipped=false", query);
            Assert.Contains("was_delivered=true", query);
            Assert.Contains("was_canceled=false", query);
            Assert.Contains("legacy=true", query);
            return Task.FromResult(JsonResponse(
                HttpStatusCode.OK,
                "{\"count\":1,\"results\":[{\"receipt_id\":456}]}"));
        });
        var service = CreateService(new StubHttpClientFactory(handler));
        var filter = new GetShopReceiptsFilter
        {
            Limit = 50,
            Offset = 5,
            MinCreated = 946684800,
            MaxCreated = 946684900,
            MinLastModified = 946685000,
            MaxLastModified = 946685100,
            SortOn = ReceiptSortOn.receipt_id,
            SortOrder = ReceiptSortOrder.asc,
            WasPaid = true,
            WasShipped = false,
            WasDelivered = true,
            WasCancelled = false,
            Legacy = true
        };

        var result = await service.GetShopReceiptsAsync("123.access-token", 123, filter);

        Assert.True(result.Success);
        Assert.Equal(1, result.Data?.Count);
        Assert.Equal(456, result.Data?.Results.Single().ReceiptId);
    }

    [Fact]
    public async Task GetShopReceiptTransactionsByListingAsync_Filter_SendsPaginationAndLegacy()
    {
        var handler = new StubHttpMessageHandler(request =>
        {
            Assert.Equal(HttpMethod.Get, request.Method);
            AssertOAuthHeaders(request);
            Assert.Equal(
                "https://openapi.etsy.com/v3/application/shops/123/listings/456/transactions" +
                "?limit=100&offset=25&legacy=false",
                request.RequestUri?.ToString());
            return Task.FromResult(JsonResponse(HttpStatusCode.OK, "{\"count\":1,\"results\":[{\"transaction_id\":789}]}"));
        });
        var service = CreateService(new StubHttpClientFactory(handler));
        var filter = new GetShopReceiptTransactionsByListingFilter
        {
            Limit = 100,
            Offset = 25,
            Legacy = false
        };

        var result = await service.GetShopReceiptTransactionsByListingAsync(
            "123.access-token",
            123,
            456,
            filter);

        Assert.True(result.Success);
        Assert.Equal(789, result.Data!.Results.Single().TransactionId);
    }

    [Fact]
    public async Task UpdateShopReceiptAsync_ValidRequest_SendsFormEncodedStatus()
    {
        var handler = new StubHttpMessageHandler(async request =>
        {
            Assert.Equal(HttpMethod.Put, request.Method);
            AssertOAuthHeaders(request);
            Assert.Equal(
                "https://openapi.etsy.com/v3/application/shops/123/receipts/456?legacy=false",
                request.RequestUri?.ToString());
            Assert.Equal("application/x-www-form-urlencoded", request.Content?.Headers.ContentType?.MediaType);
            Assert.Equal("was_paid=true&was_shipped=false", await request.Content!.ReadAsStringAsync());
            return JsonResponse(HttpStatusCode.OK, "{\"receipt_id\":456,\"is_paid\":true}");
        });
        var service = CreateService(new StubHttpClientFactory(handler));

        var result = await service.UpdateShopReceiptAsync(
            "123.access-token",
            123,
            456,
            new UpdateShopReceiptRequest { WasPaid = true, WasShipped = false },
            legacy: false);

        Assert.True(result.Success);
        Assert.True(result.Data?.IsPaid);
    }

    [Fact]
    public async Task CreateReceiptShipmentAsync_ValidRequest_SendsJsonAndOmitsNullProperties()
    {
        var handler = new StubHttpMessageHandler(async request =>
        {
            Assert.Equal(HttpMethod.Post, request.Method);
            AssertOAuthHeaders(request);
            Assert.Equal(
                "https://openapi.etsy.com/v3/application/shops/123/receipts/456/tracking",
                request.RequestUri?.ToString());
            Assert.Equal("application/json", request.Content?.Headers.ContentType?.MediaType);
            Assert.Equal("utf-8", request.Content?.Headers.ContentType?.CharSet);

            using var json = JsonDocument.Parse(await request.Content!.ReadAsStringAsync());
            var root = json.RootElement;
            Assert.Equal("TRACK 1", root.GetProperty("tracking_code").GetString());
            Assert.False(root.GetProperty("send_bcc").GetBoolean());
            Assert.False(root.TryGetProperty("carrier_name", out _));
            Assert.Equal(
                "1234",
                root.GetProperty("customs_data")[0].GetProperty("HS_code").GetString());
            return JsonResponse(HttpStatusCode.OK, "{\"receipt_id\":456}");
        });
        var service = CreateService(new StubHttpClientFactory(handler));
        var shipment = new CreateReceiptShipmentRequest
        {
            TrackingCode = "TRACK 1",
            SendBcc = false,
            CustomsData = new[]
            {
                new ReceiptShipmentCustomsData
                {
                    CountryOfOrigin = "GB",
                    DeclaredValue = 12.5F,
                    HsCode = "1234"
                }
            }
        };

        var result = await service.CreateReceiptShipmentAsync(
            "123.access-token",
            123,
            456,
            shipment);

        Assert.True(result.Success);
        Assert.Equal(456, result.Data?.ReceiptId);
    }

    [Fact]
    public async Task GetShopReceiptTransactionAsync_ValidRequest_SendsOAuthHeadersAndParsesTransaction()
    {
        var handler = new StubHttpMessageHandler(request =>
        {
            Assert.Equal(HttpMethod.Get, request.Method);
            Assert.Equal(
                "https://openapi.etsy.com/v3/application/shops/123/transactions/789",
                request.RequestUri?.ToString());
            AssertOAuthHeaders(request);
            return Task.FromResult(JsonResponse(HttpStatusCode.OK, "{\"transaction_id\":789}"));
        });
        var service = CreateService(new StubHttpClientFactory(handler));

        var result = await service.GetShopReceiptTransactionAsync("123.access-token", 123, 789);

        Assert.True(result.Success);
        Assert.Equal(789, result.Data?.TransactionId);
    }

    [Fact]
    public async Task GetShopReceiptTransactionsByReceiptAsync_ValidRequest_SendsOAuthHeadersAndParsesTransactions()
    {
        var handler = new StubHttpMessageHandler(request =>
        {
            Assert.Equal(HttpMethod.Get, request.Method);
            Assert.Equal(
                "https://openapi.etsy.com/v3/application/shops/123/receipts/456/transactions?legacy=true",
                request.RequestUri?.ToString());
            AssertOAuthHeaders(request);
            return Task.FromResult(JsonResponse(HttpStatusCode.OK, "{\"count\":1,\"results\":[{\"transaction_id\":789}]}"));
        });
        var service = CreateService(new StubHttpClientFactory(handler));

        var result = await service.GetShopReceiptTransactionsByReceiptAsync("123.access-token", 123, 456, legacy: true);

        Assert.True(result.Success);
        Assert.Equal(789, result.Data?.Results.Single().TransactionId);
    }

    [Fact]
    public async Task GetShopReceiptTransactionsByShopAsync_ValidRequest_SendsOAuthHeadersAndParsesTransactions()
    {
        var handler = new StubHttpMessageHandler(request =>
        {
            Assert.Equal(HttpMethod.Get, request.Method);
            Assert.Equal(
                "https://openapi.etsy.com/v3/application/shops/123/transactions?limit=10&offset=2&legacy=false",
                request.RequestUri?.ToString());
            AssertOAuthHeaders(request);
            return Task.FromResult(JsonResponse(HttpStatusCode.OK, "{\"count\":1,\"results\":[{\"transaction_id\":789}]}"));
        });
        var service = CreateService(new StubHttpClientFactory(handler));

        var result = await service.GetShopReceiptTransactionsByShopAsync(
            "123.access-token",
            123,
            new GetShopReceiptTransactionsByShopFilter { Limit = 10, Offset = 2, Legacy = false });

        Assert.True(result.Success);
        Assert.Equal(789, result.Data?.Results.Single().TransactionId);
    }

    [Theory]
    [InlineData(0, 0)]
    [InlineData(101, 0)]
    [InlineData(25, -1)]
    public async Task GetShopReceiptsAsync_InvalidPagination_ThrowsArgumentOutOfRangeException(
        int limit,
        int offset)
    {
        var service = CreateService();
        var filter = new GetShopReceiptsFilter { Limit = limit, Offset = offset };

        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(
            () => service.GetShopReceiptsAsync("123.access-token", 123, filter));
    }

    [Fact]
    public async Task UpdateShopReceiptAsync_EmptyUpdate_ThrowsArgumentException()
    {
        var service = CreateService();

        await Assert.ThrowsAsync<ArgumentException>(() => service.UpdateShopReceiptAsync(
            "123.access-token",
            123,
            456,
            new UpdateShopReceiptRequest()));
    }

    [Fact]
    public async Task GetShopReceiptAsync_CancelledRequest_PropagatesCancellation()
    {
        var handler = new StubHttpMessageHandler(async (_, cancellationToken) =>
        {
            await Task.Delay(Timeout.InfiniteTimeSpan, cancellationToken);
            throw new InvalidOperationException("The cancelled request unexpectedly completed.");
        });
        var service = CreateService(new StubHttpClientFactory(handler));
        using var cancellationSource = new CancellationTokenSource();
        cancellationSource.Cancel();

        await Assert.ThrowsAnyAsync<OperationCanceledException>(() => service.GetShopReceiptAsync(
            "123.access-token",
            123,
            456,
            cancellationToken: cancellationSource.Token));
    }

    [Fact]
    public async Task ReceiptEndpoints_InvalidIdentifiersAndMissingToken_ThrowBeforeSendingARequest()
    {
        var service = CreateService();

        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(
            () => service.GetShopReceiptAsync("123.access-token", 0, 456));
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(
            () => service.GetShopReceiptTransactionsByListingAsync("123.access-token", 123, 0));
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(
            () => service.GetShopReceiptTransactionAsync("123.access-token", 123, 0));
        await Assert.ThrowsAsync<ArgumentException>(
            () => service.GetShopReceiptsAsync(string.Empty, 123));
        await Assert.ThrowsAsync<ArgumentNullException>(
            () => service.CreateReceiptShipmentAsync("123.access-token", 123, 456, null!));
    }

    [Fact]
    public void AddEtsyReceiptManagementServiceTransient_RegisteredService_ResolvesFromDependencyInjection()
    {
        var services = new ServiceCollection();
        services.AddEtsyReceiptManagementServiceTransient("client-id", "shared-secret");
        using var provider = services.BuildServiceProvider();

        var service = provider.GetRequiredService<IEtsyReceiptManagementService>();

        Assert.IsType<EtsyReceiptManagementService>(service);
    }

    private static EtsyReceiptManagementService CreateService(IHttpClientFactory? factory = null) => new(
        factory ?? new StubHttpClientFactory(new StubHttpMessageHandler(
            _ => throw new InvalidOperationException("No HTTP request was expected."))),
        "client-id",
        "shared-secret");

    private static HttpResponseMessage JsonResponse(HttpStatusCode statusCode, string body) => new(statusCode)
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
