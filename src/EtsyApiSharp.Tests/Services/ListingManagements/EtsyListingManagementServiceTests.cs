using EtsyApiSharp.Models;
using EtsyApiSharp.Models.Filters;
using EtsyApiSharp.Models.Listings.Enums;
using EtsyApiSharp.Services.ListingManagements;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Text;
using Xunit;

namespace EtsyApiSharp.Tests.Services.ListingManagements;

public class EtsyListingManagementServiceTests
{
    [Fact]
    public async Task FindAllListingsActiveAsync_Filter_UsesPublicEndpointAndEncodesAllSupportedFilters()
    {
        var handler = new StubHttpMessageHandler(request =>
        {
            Assert.Equal(HttpMethod.Get, request.Method);
            Assert.Null(request.Headers.Authorization);
            Assert.Equal("client-id:shared-secret", request.Headers.GetValues("x-api-key").Single());
            Assert.Equal(
                "https://openapi.etsy.com/v3/application/listings/active?limit=50&offset=5&sort_on=score&sort_order=asc&keywords=needle%20%26%20thread&min_price=1.5&max_price=20.25&taxonomy_id=123&shop_location=London&is_safe=true&currency=GBP&buyer_country=GB",
                request.RequestUri?.AbsoluteUri);
            return Task.FromResult(JsonResponse(HttpStatusCode.OK, "{\"count\":1,\"results\":[{\"listing_id\":456,\"title\":\"Needle & Thread\"}]}"));
        });
        var service = CreateService(new StubHttpClientFactory(handler));

        var result = await service.FindAllListingsActiveAsync(new FindAllListingsActiveFilter
        {
            Limit = 50,
            Offset = 5,
            SortOn = ListingSortOn.score,
            SortOrder = ListingSortOrder.asc,
            Keywords = "needle & thread",
            MinPrice = 1.5,
            MaxPrice = 20.25,
            TaxonomyId = 123,
            ShopLocation = "London",
            IsSafe = true,
            Currency = "GBP",
            BuyerCountry = "GB"
        });

        Assert.True(result.Success);
        Assert.Equal(456, result.Data?.Results.Single().ListingId);
    }

    [Fact]
    public async Task GetListingAsync_Options_UsesPublicRouteAndCommaSeparatedLowercaseIncludes()
    {
        var handler = new StubHttpMessageHandler(request =>
        {
            Assert.Equal(HttpMethod.Get, request.Method);
            Assert.Null(request.Headers.Authorization);
            Assert.Equal(
                "https://openapi.etsy.com/v3/application/listings/456?includes=images%2Cinventory&language=en&allow_suggested_title=true",
                request.RequestUri?.AbsoluteUri);
            return Task.FromResult(JsonResponse(HttpStatusCode.OK, "{\"listing_id\":456}"));
        });
        var service = CreateService(new StubHttpClientFactory(handler));

        var result = await service.GetListingAsync(456, new[] { ListingInclude.Images, ListingInclude.Inventory }, "en", true);

        Assert.True(result.Success);
        Assert.Equal(456, result.Data?.ListingId);
    }

    [Fact]
    public async Task GetListingsByShopAsync_AuthenticatedRequest_SendsListingScopeBearerToken()
    {
        var handler = new StubHttpMessageHandler(request =>
        {
            Assert.Equal("Bearer", request.Headers.Authorization?.Scheme);
            Assert.Equal("123.access-token", request.Headers.Authorization?.Parameter);
            Assert.Equal(
                "https://openapi.etsy.com/v3/application/shops/123/listings?limit=10&state=draft&sort_on=updated&sort_order=asc&includes=images",
                request.RequestUri?.AbsoluteUri);
            return Task.FromResult(JsonResponse(HttpStatusCode.OK, "{\"count\":0,\"results\":[]}"));
        });
        var factory = new StubHttpClientFactory(handler);
        var service = CreateService(factory);

        var result = await service.GetListingsByShopAsync(
            "123.access-token",
            123,
            new[] { ListingInclude.Images },
            new GetListingsByShopFilter { Limit = 10, State = ListingState.draft, SortOn = ListingSortOn.updated, SortOrder = ListingSortOrder.asc });

        Assert.Equal(EtsyListingManagementService.HttpClientName, factory.RequestedName);
        Assert.True(result.Success);
    }

    [Fact]
    public async Task GetListingsByShopReceiptAsync_Filter_SendsTransactionsScopeBearerTokenAndLegacy()
    {
        var handler = new StubHttpMessageHandler(request =>
        {
            Assert.Equal("Bearer", request.Headers.Authorization?.Scheme);
            Assert.Equal(
                "https://openapi.etsy.com/v3/application/shops/123/receipts/456/listings?limit=10&offset=2&legacy=false",
                request.RequestUri?.AbsoluteUri);
            return Task.FromResult(JsonResponse(HttpStatusCode.OK, "{\"count\":0,\"results\":[]}"));
        });
        var service = CreateService(new StubHttpClientFactory(handler));

        var result = await service.GetListingsByShopReceiptAsync(
            "123.access-token",
            123,
            456,
            new GetListingsByShopReceiptFilter { Limit = 10, Offset = 2, Legacy = false });

        Assert.True(result.Success);
    }

    [Fact]
    public async Task TaxonomyMethods_ValidRequests_UseCurrentRoutesAndCorrectBuyerModels()
    {
        var urls = new Queue<string>();
        var handler = new StubHttpMessageHandler(request =>
        {
            urls.Enqueue(request.RequestUri!.ToString());
            var buyerProperties = request.RequestUri.AbsolutePath.Contains("buyer-taxonomy/nodes/456/properties", StringComparison.Ordinal);
            var body = buyerProperties
                ? "{\"count\":1,\"results\":[{\"property_id\":5,\"name\":\"Colour\"}]}"
                : "{\"count\":0,\"results\":[]}";
            return Task.FromResult(JsonResponse(HttpStatusCode.OK, body));
        });
        var service = CreateService(new StubHttpClientFactory(handler));

        await service.GetSellerTaxonomyNodesAsync();
        await service.GetPropertiesByTaxonomyIdAsync(123);
        await service.GetBuyerTaxonomyNodesAsync();
        var buyerProperties = await service.GetPropertiesByBuyerTaxonomyIdAsync(456);

        Assert.Equal("https://openapi.etsy.com/v3/application/seller-taxonomy/nodes", urls.Dequeue());
        Assert.Equal("https://openapi.etsy.com/v3/application/seller-taxonomy/nodes/123/properties", urls.Dequeue());
        Assert.Equal("https://openapi.etsy.com/v3/application/buyer-taxonomy/nodes", urls.Dequeue());
        Assert.Equal("https://openapi.etsy.com/v3/application/buyer-taxonomy/nodes/456/properties", urls.Dequeue());
        Assert.Equal("Colour", buyerProperties.Data?.Results.Single().Name);
    }

    [Fact]
    public async Task GetListingsByListingIdsAsync_Options_UsesCurrentQueryParameters()
    {
        var handler = new StubHttpMessageHandler(request =>
        {
            Assert.Equal(
                "https://openapi.etsy.com/v3/application/listings/batch?listing_ids=123%2C456&includes=images&legacy=true&currency=GBP&buyer_country=GB",
                request.RequestUri?.AbsoluteUri);
            return Task.FromResult(JsonResponse(HttpStatusCode.OK, "{\"count\":0,\"results\":[]}"));
        });
        var service = CreateService(new StubHttpClientFactory(handler));

        var result = await service.GetListingsByListingIdsAsync(new long[] { 123, 456 }, new[] { ListingInclude.Images }, true, "GBP", "GB");

        Assert.True(result.Success);
    }

    [Theory]
    [InlineData(0, 0)]
    [InlineData(101, 0)]
    [InlineData(25, -1)]
    public async Task FindAllListingsActiveAsync_InvalidPagination_ThrowsArgumentOutOfRangeException(int limit, int offset)
    {
        var service = CreateService();

        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => service.FindAllListingsActiveAsync(
            new FindAllListingsActiveFilter { Limit = limit, Offset = offset }));
    }

    [Fact]
    public async Task GetListingAsync_NonPositiveListingId_ThrowsArgumentOutOfRangeException()
    {
        var service = CreateService();

        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => service.GetListingAsync(0));
    }

    [Fact]
    public async Task GetListingsByShopAsync_BlankAccessToken_ThrowsArgumentException()
    {
        var service = CreateService();

        await Assert.ThrowsAsync<ArgumentException>(() => service.GetListingsByShopAsync(" ", 123));
    }

    [Fact]
    public async Task GetListingAsync_CancelledRequest_PropagatesCancellation()
    {
        var handler = new StubHttpMessageHandler(async (_, cancellationToken) =>
        {
            await Task.Delay(Timeout.InfiniteTimeSpan, cancellationToken);
            throw new InvalidOperationException("The cancelled request unexpectedly completed.");
        });
        var service = CreateService(new StubHttpClientFactory(handler));
        using var cancellationSource = new CancellationTokenSource();
        cancellationSource.Cancel();

        await Assert.ThrowsAnyAsync<OperationCanceledException>(() => service.GetListingAsync(123, cancellationToken: cancellationSource.Token));
    }

    [Fact]
    public void AddEtsyListingManagementServiceTransient_RegisteredService_ResolvesFromDependencyInjection()
    {
        var services = new ServiceCollection();
        services.AddEtsyListingManagementServiceTransient("client-id", "shared-secret");
        using var provider = services.BuildServiceProvider();

        var service = provider.GetRequiredService<IEtsyListingManagementService>();

        Assert.IsType<EtsyListingManagementService>(service);
    }

    [Fact]
    public async Task ListingLifecycleAndPropertyMethods_ValidRequests_UseDocumentedRoutesAndContentTypes()
    {
        var requests = new Queue<HttpRequestMessage>();
        var requestBodies = new Queue<string>();
        var service = CreateService(new StubHttpClientFactory(new StubHttpMessageHandler(async request =>
        {
            requests.Enqueue(request);
            requestBodies.Enqueue(request.Content is null ? string.Empty : await request.Content.ReadAsStringAsync());
            return JsonResponse(HttpStatusCode.OK, "{}");
        })));
        var create = new CreateDraftListingRequest
        {
            Quantity = 1,
            Title = "Listing",
            Description = "Description",
            Price = 10.5M,
            WhoMade = ListingWhoMade.i_did,
            WhenMade = "made_to_order",
            TaxonomyId = 123,
            Tags = new[] { "tag one", "tag two" }
        };

        await service.CreateDraftListingAsync("123.token", 10, create);
        await service.UpdateListingAsync("123.token", 10, 20, new UpdateListingRequest { Title = "Updated" });
        await service.DeleteListingAsync("123.token", 20);
        await service.UpdateListingPropertyAsync("123.token", 10, 20, 30, new UpdateListingPropertyRequest { ValueIds = new long[] { 40 }, Values = new[] { "Blue" } });
        await service.DeleteListingPropertyAsync("123.token", 10, 20, 30);
        await service.GetListingsByShopReturnPolicyAsync("123.token", 10, 50, legacy: true);
        await service.GetListingsShippingByListingIdsAsync("123.token", new long[] { 20, 21 });

        var createRequest = requests.Dequeue();
        Assert.Equal("/v3/application/shops/10/listings", createRequest.RequestUri?.AbsolutePath);
        Assert.Equal("application/x-www-form-urlencoded", createRequest.Content?.Headers.ContentType?.MediaType);
        Assert.Contains("tags=tag+one%2Ctag+two", requestBodies.Dequeue());
        var updateRequest = requests.Dequeue();
        Assert.Equal(HttpMethod.Patch, updateRequest.Method);
        Assert.Equal("/v3/application/shops/10/listings/20", updateRequest.RequestUri?.AbsolutePath);
        Assert.Contains("title=Updated", requestBodies.Dequeue());
        Assert.Equal(HttpMethod.Delete, requests.Dequeue().Method);
        Assert.Equal(HttpMethod.Put, requests.Peek().Method);
        Assert.Equal("/v3/application/shops/10/listings/20/properties/30", requests.Dequeue().RequestUri?.AbsolutePath);
        Assert.Equal(HttpMethod.Delete, requests.Dequeue().Method);
        Assert.Equal("legacy=true", requests.Dequeue().RequestUri?.Query.TrimStart('?'));
        Assert.Equal("listing_ids=20%2C21", requests.Dequeue().RequestUri?.Query.TrimStart('?'));
    }

    [Fact]
    public async Task ListingFileAndImageMethods_ValidRequests_UseAuthenticatedRoutesAndMultipartUploads()
    {
        var requests = new Queue<HttpRequestMessage>();
        var service = CreateService(new StubHttpClientFactory(new StubHttpMessageHandler(request =>
        {
            requests.Enqueue(request);
            return Task.FromResult(JsonResponse(HttpStatusCode.OK, "{\"count\":0,\"results\":[]}"));
        })));

        await service.GetAllListingFilesAsync("123.token", 10, 20);
        await service.GetListingFileAsync("123.token", 10, 20, 30);
        await service.UploadListingFileAsync("123.token", 10, 20, new ListingFileUploadRequest { File = new MemoryStream(new byte[] { 1 }), FileName = "file.pdf", Rank = 1 });
        await service.DeleteListingFileAsync("123.token", 10, 20, 30);
        await service.UploadListingImageAsync("123.token", 10, 20, new ListingImageUploadRequest { Image = new MemoryStream(new byte[] { 1 }), FileName = "image.jpg", AltText = "Alt" });
        await service.DeleteListingImageAsync("123.token", 10, 20, 40);

        Assert.Equal("/v3/application/shops/10/listings/20/files", requests.Dequeue().RequestUri?.AbsolutePath);
        Assert.Equal("/v3/application/shops/10/listings/20/files/30", requests.Dequeue().RequestUri?.AbsolutePath);
        Assert.Equal("multipart/form-data", requests.Dequeue().Content?.Headers.ContentType?.MediaType);
        Assert.Equal(HttpMethod.Delete, requests.Dequeue().Method);
        Assert.Equal("multipart/form-data", requests.Dequeue().Content?.Headers.ContentType?.MediaType);
        Assert.Equal("/v3/application/shops/10/listings/20/images/40", requests.Dequeue().RequestUri?.AbsolutePath);
    }

    [Fact]
    public async Task InventoryAndPersonalizationMethods_ValidRequests_UseRequiredAuthenticationAndJsonBodies()
    {
        var requests = new Queue<HttpRequestMessage>();
        var service = CreateService(new StubHttpClientFactory(new StubHttpMessageHandler(request =>
        {
            requests.Enqueue(request);
            return Task.FromResult(JsonResponse(HttpStatusCode.OK, "{\"count\":0,\"results\":[]}"));
        })));
        var inventory = new UpdateListingInventoryRequest
        {
            Products = new[] { new ListingInventoryProduct { Sku = "SKU", Offerings = new List<ListingInventoryProductOffering>() } }
        };
        var personalization = new ListingPersonalizationUpdateRequest
        {
            PersonalizationQuestions = new[] { new ListingPersonalizationQuestion { QuestionText = "Name", QuestionType = "text", Required = true } }
        };

        await service.GetListingInventoryAsync("123.token", 20, showDeleted: false, includes: new[] { ListingInclude.Inventory });
        await service.GetListingsInventoryByListingIdsAsync("123.token", new long[] { 20, 21 });
        await service.UpdateListingInventoryAsync("123.token", 20, inventory, "3");
        await service.GetListingOfferingAsync(20, 30, 40, legacy: true);
        await service.GetListingProductAsync("123.token", 20, 30);
        await service.GetListingPersonalizationAsync(20);
        await service.UpdateListingPersonalizationAsync("123.token", 10, 20, personalization, true);
        await service.DeleteListingPersonalizationAsync("123.token", 10, 20);

        Assert.Equal("show_deleted=false&includes=inventory", requests.Dequeue().RequestUri?.Query.TrimStart('?'));
        Assert.Equal("/v3/application/listings/batch/inventory", requests.Dequeue().RequestUri?.AbsolutePath);
        Assert.Equal(HttpMethod.Put, requests.Peek().Method);
        Assert.Equal("application/json", requests.Dequeue().Content?.Headers.ContentType?.MediaType);
        Assert.Equal("legacy=true", requests.Dequeue().RequestUri?.Query.TrimStart('?'));
        Assert.Equal("/v3/application/listings/20/inventory/products/30", requests.Dequeue().RequestUri?.AbsolutePath);
        Assert.Null(requests.Dequeue().Headers.Authorization);
        Assert.Equal("application/json", requests.Dequeue().Content?.Headers.ContentType?.MediaType);
        Assert.Equal(HttpMethod.Delete, requests.Dequeue().Method);
    }

    [Fact]
    public async Task TranslationVariationAndVideoMethods_ValidRequests_UseCurrentRoutes()
    {
        var requests = new Queue<HttpRequestMessage>();
        var service = CreateService(new StubHttpClientFactory(new StubHttpMessageHandler(request =>
        {
            requests.Enqueue(request);
            return Task.FromResult(JsonResponse(HttpStatusCode.OK, "{\"count\":0,\"results\":[]}"));
        })));
        var translation = new ListingTranslationRequest { Title = "Title", Description = "Description" };

        await service.GetListingTranslationAsync(10, 20, "fr");
        await service.CreateListingTranslationAsync("123.token", 10, 20, "fr", translation);
        await service.UpdateListingTranslationAsync("123.token", 10, 20, "fr", translation);
        await service.GetListingVariationImagesAsync(10, 20);
        await service.UpdateVariationImagesAsync("123.token", 10, 20, new[] { new ListingVariationImage { PropertyId = 1, ValueId = 2, ImageId = 3 } });
        await service.GetListingVideoAsync(20, 30);
        await service.GetListingVideosAsync(20);
        await service.UploadListingVideoAsync("123.token", 10, 20, new ListingVideoUploadRequest { VideoId = 30 });
        await service.DeleteListingVideoAsync("123.token", 10, 20, 30);

        Assert.Null(requests.Dequeue().Headers.Authorization);
        Assert.Equal(HttpMethod.Post, requests.Dequeue().Method);
        Assert.Equal(HttpMethod.Put, requests.Dequeue().Method);
        Assert.Equal("/v3/application/shops/10/listings/20/variation-images", requests.Dequeue().RequestUri?.AbsolutePath);
        Assert.Equal("application/json", requests.Dequeue().Content?.Headers.ContentType?.MediaType);
        Assert.Equal("/v3/application/listings/20/videos/30", requests.Dequeue().RequestUri?.AbsolutePath);
        Assert.Equal("/v3/application/listings/20/videos", requests.Dequeue().RequestUri?.AbsolutePath);
        Assert.Equal("multipart/form-data", requests.Dequeue().Content?.Headers.ContentType?.MediaType);
        Assert.Equal(HttpMethod.Delete, requests.Dequeue().Method);
    }

    private static EtsyListingManagementService CreateService(IHttpClientFactory? factory = null) => new(
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

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken) =>
            sendAsync(request, cancellationToken);
    }
}
