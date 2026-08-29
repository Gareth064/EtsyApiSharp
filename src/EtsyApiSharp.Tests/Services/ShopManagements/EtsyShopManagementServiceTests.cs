using EtsyApiSharp.Models;
using EtsyApiSharp.Models.Filters;
using EtsyApiSharp.Services.ShopManagements;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Text;
using Xunit;

namespace EtsyApiSharp.Tests.Services.ShopManagements;

public class EtsyShopManagementServiceTests
{
    [Fact]
    public async Task GetShopAsync_ValidRequest_UsesApiKeyWithoutOAuthAndParsesShop()
    {
        var handler = new StubHttpMessageHandler(request =>
        {
            Assert.Equal(HttpMethod.Get, request.Method);
            Assert.Equal("https://openapi.etsy.com/v3/application/shops/123", request.RequestUri?.ToString());
            Assert.Null(request.Headers.Authorization);
            Assert.Equal("client-id:shared-secret", request.Headers.GetValues("x-api-key").Single());
            return Task.FromResult(JsonResponse(
                HttpStatusCode.OK,
                "{\"shop_id\":123,\"created_timestamp\":3000000000,\"updated_timestamp\":3000000001,\"title\":null}"));
        });
        var factory = new StubHttpClientFactory(handler);
        var service = CreateService(factory);

        var result = await service.GetShopAsync(123);

        Assert.Equal(EtsyShopManagementService.HttpClientName, factory.RequestedName);
        Assert.True(result.Success);
        Assert.Equal(123, result.Data?.ShopId);
        Assert.Equal(3000000000L, result.Data?.CreatedTimestamp);
        Assert.Equal(3000000001L, result.Data?.UpdatedTimestamp);
        Assert.Null(result.Data?.Title);
        Assert.Equal("https://openapi.etsy.com/v3/application/shops/123", result.RequestedResource);
    }

    [Fact]
    public async Task GetShopByOwnerUserIdAsync_ValidRequest_UsesCurrentPublicRoute()
    {
        var handler = new StubHttpMessageHandler(request =>
        {
            Assert.Equal(HttpMethod.Get, request.Method);
            Assert.Equal("https://openapi.etsy.com/v3/application/users/456/shops", request.RequestUri?.ToString());
            Assert.Null(request.Headers.Authorization);
            Assert.Equal("client-id:shared-secret", request.Headers.GetValues("x-api-key").Single());
            return Task.FromResult(JsonResponse(HttpStatusCode.OK, "{\"shop_id\":123,\"user_id\":456}"));
        });
        var service = CreateService(new StubHttpClientFactory(handler));

        var result = await service.GetShopByOwnerUserIdAsync(456);

        Assert.True(result.Success);
        Assert.Equal(456, result.Data?.UserId);
    }

    [Fact]
    public async Task FindShopsAsync_PopulatedFilter_EncodesNameAndPagination()
    {
        var handler = new StubHttpMessageHandler(request =>
        {
            Assert.Equal(
                "https://openapi.etsy.com/v3/application/shops?shop_name=Needle%20%26%20Thread&limit=50&offset=5",
                request.RequestUri?.AbsoluteUri);
            Assert.Null(request.Headers.Authorization);
            return Task.FromResult(JsonResponse(
                HttpStatusCode.OK,
                "{\"count\":1,\"results\":[{\"shop_id\":123,\"shop_name\":\"Needle & Thread\"}]}"));
        });
        var service = CreateService(new StubHttpClientFactory(handler));

        var result = await service.FindShopsAsync(
            "Needle & Thread",
            new FindShopsByNameFilter { Limit = 50, Offset = 5 });

        Assert.True(result.Success);
        Assert.Equal(1, result.Data?.Count);
        Assert.Equal("Needle & Thread", result.Data?.Results.Single().ShopName);
    }

    [Fact]
    public async Task UpdateShopAsync_ValidRequest_SendsOAuthAndFormEncodedFields()
    {
        var handler = new StubHttpMessageHandler(async request =>
        {
            Assert.Equal(HttpMethod.Put, request.Method);
            Assert.Equal("https://openapi.etsy.com/v3/application/shops/123", request.RequestUri?.ToString());
            Assert.Equal("Bearer", request.Headers.Authorization?.Scheme);
            Assert.Equal("123.access-token", request.Headers.Authorization?.Parameter);
            Assert.Equal("client-id:shared-secret", request.Headers.GetValues("x-api-key").Single());
            Assert.Equal("application/x-www-form-urlencoded", request.Content?.Headers.ContentType?.MediaType);
            Assert.Equal(
                "title=New+title&announcement=Hello%2C+world%21&policy_additional=",
                await request.Content!.ReadAsStringAsync());
            return JsonResponse(HttpStatusCode.OK, "{\"shop_id\":123,\"title\":\"New title\"}");
        });
        var service = CreateService(new StubHttpClientFactory(handler));
        var update = new UpdateShopRequest
        {
            Title = "New title",
            Announcement = "Hello, world!",
            PolicyAdditional = string.Empty
        };

        var result = await service.UpdateShopAsync("123.access-token", 123, update);

        Assert.True(result.Success);
        Assert.Equal("New title", result.Data?.Title);
    }

    [Fact]
    public async Task GetShopProductionPartnersAsync_ValidRequest_SendsShopsReadOAuthAndParsesList()
    {
        var handler = new StubHttpMessageHandler(request =>
        {
            Assert.Equal(HttpMethod.Get, request.Method);
            Assert.Equal("https://openapi.etsy.com/v3/application/shops/123/production-partners", request.RequestUri?.ToString());
            Assert.Equal("Bearer 123.access-token", request.Headers.Authorization?.ToString());
            Assert.Equal("client-id:shared-secret", request.Headers.GetValues("x-api-key").Single());
            return Task.FromResult(JsonResponse(HttpStatusCode.OK, "{\"count\":1,\"results\":[{\"production_partner_id\":7,\"partner_name\":\"Printer\",\"location\":\"London\"}]}"));
        });
        var service = CreateService(new StubHttpClientFactory(handler));

        var result = await service.GetShopProductionPartnersAsync("123.access-token", 123);

        Assert.True(result.Success);
        Assert.Equal(7, result.Data?.Results.Single().ProductionPartnerId);
        Assert.Equal("Printer", result.Data?.Results.Single().PartnerName);
    }

    [Fact]
    public async Task CreateShopSectionAsync_ValidRequest_SendsShopsWriteOAuthAndFormEncodedTitle()
    {
        var handler = new StubHttpMessageHandler(async request =>
        {
            Assert.Equal(HttpMethod.Post, request.Method);
            Assert.Equal("https://openapi.etsy.com/v3/application/shops/123/sections", request.RequestUri?.ToString());
            Assert.Equal("Bearer 123.access-token", request.Headers.Authorization?.ToString());
            Assert.Equal("application/x-www-form-urlencoded", request.Content?.Headers.ContentType?.MediaType);
            Assert.Equal("title=New+%26+Noteworthy", await request.Content!.ReadAsStringAsync());
            return JsonResponse(HttpStatusCode.OK, "{\"shop_section_id\":9,\"title\":\"New & Noteworthy\"}");
        });
        var service = CreateService(new StubHttpClientFactory(handler));

        var result = await service.CreateShopSectionAsync("123.access-token", 123, new CreateShopSectionRequest { Title = "New & Noteworthy" });

        Assert.True(result.Success);
        Assert.Equal(9, result.Data?.ShopSectionId);
    }

    [Fact]
    public async Task GetShopSectionsAsync_ValidRequest_UsesApiKeyWithoutOAuthAndParsesList()
    {
        var handler = new StubHttpMessageHandler(request =>
        {
            Assert.Equal(HttpMethod.Get, request.Method);
            Assert.Equal("https://openapi.etsy.com/v3/application/shops/123/sections", request.RequestUri?.ToString());
            Assert.Null(request.Headers.Authorization);
            Assert.Equal("client-id:shared-secret", request.Headers.GetValues("x-api-key").Single());
            return Task.FromResult(JsonResponse(HttpStatusCode.OK, "{\"count\":1,\"results\":[{\"shop_section_id\":9,\"title\":\"Featured\",\"rank\":3000000000,\"active_listing_count\":4000000000}]}"));
        });
        var service = CreateService(new StubHttpClientFactory(handler));

        var result = await service.GetShopSectionsAsync(123);

        Assert.True(result.Success);
        Assert.Equal(3000000000L, result.Data?.Results.Single().Rank);
        Assert.Equal(4000000000L, result.Data?.Results.Single().ActiveListingCount);
    }

    [Fact]
    public async Task DeleteShopSectionAsync_ValidRequest_SendsShopsWriteOAuthAndParsesEmptyResponse()
    {
        var handler = new StubHttpMessageHandler(request =>
        {
            Assert.Equal(HttpMethod.Delete, request.Method);
            Assert.Equal("https://openapi.etsy.com/v3/application/shops/123/sections/9", request.RequestUri?.ToString());
            Assert.Equal("Bearer 123.access-token", request.Headers.Authorization?.ToString());
            return Task.FromResult(new HttpResponseMessage(HttpStatusCode.NoContent));
        });
        var service = CreateService(new StubHttpClientFactory(handler));

        var result = await service.DeleteShopSectionAsync("123.access-token", 123, 9);

        Assert.True(result.Success);
        Assert.Equal(204, result.ResponseCode);
        Assert.Null(result.Data);
    }

    [Fact]
    public async Task GetShopSectionAsync_ValidRequest_UsesApiKeyWithoutOAuthAndParsesSection()
    {
        var handler = new StubHttpMessageHandler(request =>
        {
            Assert.Equal(HttpMethod.Get, request.Method);
            Assert.Equal("https://openapi.etsy.com/v3/application/shops/123/sections/9", request.RequestUri?.ToString());
            Assert.Null(request.Headers.Authorization);
            return Task.FromResult(JsonResponse(HttpStatusCode.OK, "{\"shop_section_id\":9,\"title\":\"Featured\"}"));
        });
        var service = CreateService(new StubHttpClientFactory(handler));

        var result = await service.GetShopSectionAsync(123, 9);

        Assert.True(result.Success);
        Assert.Equal("Featured", result.Data?.Title);
    }

    [Fact]
    public async Task UpdateShopSectionAsync_ValidRequest_SendsShopsWriteOAuthAndFormEncodedTitle()
    {
        var handler = new StubHttpMessageHandler(async request =>
        {
            Assert.Equal(HttpMethod.Put, request.Method);
            Assert.Equal("https://openapi.etsy.com/v3/application/shops/123/sections/9", request.RequestUri?.ToString());
            Assert.Equal("Bearer 123.access-token", request.Headers.Authorization?.ToString());
            Assert.Equal("title=Seasonal", await request.Content!.ReadAsStringAsync());
            return JsonResponse(HttpStatusCode.OK, "{\"shop_section_id\":9,\"title\":\"Seasonal\"}");
        });
        var service = CreateService(new StubHttpClientFactory(handler));

        var result = await service.UpdateShopSectionAsync("123.access-token", 123, 9, new UpdateShopSectionRequest { Title = "Seasonal" });

        Assert.True(result.Success);
        Assert.Equal("Seasonal", result.Data?.Title);
    }

    [Fact]
    public async Task GetShopAsync_EtsyError_ReturnsActualStatusAndMessage()
    {
        var handler = new StubHttpMessageHandler(_ => Task.FromResult(JsonResponse(
            HttpStatusCode.NotFound,
            "{\"error\":\"shop_not_found\",\"error_description\":\"The shop does not exist.\"}")));
        var service = CreateService(new StubHttpClientFactory(handler));

        var result = await service.GetShopAsync(123);

        Assert.False(result.Success);
        Assert.Equal(404, result.ResponseCode);
        Assert.Null(result.Data);
        Assert.Equal("shop_not_found: The shop does not exist.", result.Message);
    }

    [Theory]
    [InlineData(0, 0)]
    [InlineData(101, 0)]
    [InlineData(25, -1)]
    public async Task FindShopsAsync_InvalidPagination_ThrowsArgumentOutOfRangeException(
        int limit,
        int offset)
    {
        var service = CreateService();
        var filter = new FindShopsByNameFilter { Limit = limit, Offset = offset };

        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(
            () => service.FindShopsAsync("shop", filter));
    }

    [Fact]
    public async Task UpdateShopAsync_EmptyUpdate_ThrowsArgumentException()
    {
        var service = CreateService();

        await Assert.ThrowsAsync<ArgumentException>(
            () => service.UpdateShopAsync("123.access-token", 123, new UpdateShopRequest()));
    }

    [Fact]
    public async Task FindShopsAsync_BlankShopName_ThrowsArgumentException()
    {
        var service = CreateService();

        await Assert.ThrowsAsync<ArgumentException>(() => service.FindShopsAsync(" "));
    }

    [Fact]
    public async Task GetShopAsync_NonPositiveShopId_ThrowsArgumentOutOfRangeException()
    {
        var service = CreateService();

        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => service.GetShopAsync(0));
    }

    [Fact]
    public async Task UpdateShopAsync_BlankAccessToken_ThrowsArgumentException()
    {
        var service = CreateService();

        await Assert.ThrowsAsync<ArgumentException>(() => service.UpdateShopAsync(
            " ",
            123,
            new UpdateShopRequest { Title = "New title" }));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public async Task ShopSectionOperations_NonPositiveIds_ThrowArgumentOutOfRangeException(long id)
    {
        var service = CreateService();

        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => service.GetShopSectionsAsync(id));
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => service.GetShopSectionAsync(123, id));
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => service.DeleteShopSectionAsync("123.access-token", id, 9));
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => service.UpdateShopSectionAsync("123.access-token", 123, id, new UpdateShopSectionRequest { Title = "Title" }));
    }

    [Fact]
    public async Task ShopSectionWrites_BlankTitleOrToken_ThrowArgumentException()
    {
        var service = CreateService();

        await Assert.ThrowsAsync<ArgumentException>(() => service.CreateShopSectionAsync("123.access-token", 123, new CreateShopSectionRequest()));
        await Assert.ThrowsAsync<ArgumentException>(() => service.UpdateShopSectionAsync("123.access-token", 123, 9, new UpdateShopSectionRequest { Title = " " }));
        await Assert.ThrowsAsync<ArgumentException>(() => service.GetShopProductionPartnersAsync(" ", 123));
    }

    [Fact]
    public async Task GetShopAsync_CancelledRequest_PropagatesCancellation()
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
            () => service.GetShopAsync(123, cancellationSource.Token));
    }

    [Fact]
    public async Task GetShopSectionsAsync_CancelledRequest_PropagatesCancellation()
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
            () => service.GetShopSectionsAsync(123, cancellationSource.Token));
    }

    [Fact]
    public void AddEtsyShopManagementServiceTransient_RegisteredService_ResolvesFromDependencyInjection()
    {
        var services = new ServiceCollection();
        services.AddEtsyShopManagementServiceTransient("client-id", "shared-secret");
        using var provider = services.BuildServiceProvider();

        var service = provider.GetRequiredService<IEtsyShopManagementService>();

        Assert.IsType<EtsyShopManagementService>(service);
    }

    private static EtsyShopManagementService CreateService(IHttpClientFactory? factory = null) => new(
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
