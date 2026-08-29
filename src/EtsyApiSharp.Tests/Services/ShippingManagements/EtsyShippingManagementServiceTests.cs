using EtsyApiSharp.Models;
using EtsyApiSharp.Models.Filters;
using EtsyApiSharp.Models.ShopShippings.Enums;
using EtsyApiSharp.Services.ShippingManagements;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Text;
using Xunit;

namespace EtsyApiSharp.Tests.Services.ShippingManagements;

public class EtsyShippingManagementServiceTests
{
    [Fact]
    public async Task GetShippingCarriersAsync_ValidRequest_UsesPublicRouteAndParsesList()
    {
        var service = CreateService(request =>
        {
            Assert.Equal(HttpMethod.Get, request.Method);
            Assert.Equal("https://openapi.etsy.com/v3/application/shipping-carriers?origin_country_iso=GB", request.RequestUri?.ToString());
            Assert.Null(request.Headers.Authorization);
            Assert.Equal("client-id:shared-secret", request.Headers.GetValues("x-api-key").Single());
            return JsonResponse(HttpStatusCode.OK, "{\"count\":1,\"results\":[{\"shipping_carrier_id\":7,\"name\":\"Royal Mail\"}]}");
        });

        var result = await service.GetShippingCarriersAsync("GB");

        Assert.True(result.Success);
        Assert.Equal("Royal Mail", result.Data?.Results.Single().Name);
    }

    [Fact]
    public async Task CreateShopShippingProfileAsync_ValidRequest_SendsShopsWriteOAuthAndForm()
    {
        var service = CreateService(async request =>
        {
            Assert.Equal(HttpMethod.Post, request.Method);
            Assert.Equal("https://openapi.etsy.com/v3/application/shops/123/shipping-profiles", request.RequestUri?.ToString());
            AssertAuth(request);
            Assert.Equal("title=Standard+%26+Tracked&origin_country_iso=GB&primary_cost=3.5&secondary_cost=1.25&min_processing_time=1&max_processing_time=3&processing_time_unit=business_days&destination_country_iso=US&min_delivery_days=5&max_delivery_days=10", await request.Content!.ReadAsStringAsync());
            return JsonResponse(HttpStatusCode.OK, "{\"shipping_profile_id\":9,\"title\":\"Standard & Tracked\"}");
        });

        var result = await service.CreateShopShippingProfileAsync(Token, 123, ProfileRequest());

        Assert.True(result.Success);
        Assert.Equal(9, result.Data?.ShippingProfileId);
    }

    [Fact]
    public async Task GetShopShippingProfilesAsync_ValidRequest_SendsShopsReadOAuthAndParsesList()
    {
        var service = CreateService(request =>
        {
            Assert.Equal(HttpMethod.Get, request.Method);
            Assert.Equal("https://openapi.etsy.com/v3/application/shops/123/shipping-profiles", request.RequestUri?.ToString());
            AssertAuth(request);
            return JsonResponse(HttpStatusCode.OK, "{\"count\":1,\"results\":[{\"shipping_profile_id\":9,\"title\":\"Standard\"}]}");
        });

        var result = await service.GetShopShippingProfilesAsync(Token, 123);

        Assert.True(result.Success);
        Assert.Equal(9, result.Data?.Results.Single().ShippingProfileId);
    }

    [Fact]
    public async Task DeleteShopShippingProfileAsync_ValidRequest_UsesDeleteRoute()
    {
        var service = CreateService(request =>
        {
            Assert.Equal(HttpMethod.Delete, request.Method);
            Assert.Equal("https://openapi.etsy.com/v3/application/shops/123/shipping-profiles/9", request.RequestUri?.ToString());
            AssertAuth(request);
            return EmptyResponse();
        });

        var result = await service.DeleteShopShippingProfileAsync(Token, 123, 9);

        Assert.True(result.Success);
        Assert.Null(result.Data);
    }

    [Fact]
    public async Task GetShopShippingProfileAsync_ValidRequest_ParsesProfile()
    {
        var service = CreateService(request =>
        {
            Assert.Equal(HttpMethod.Get, request.Method);
            Assert.Equal("https://openapi.etsy.com/v3/application/shops/123/shipping-profiles/9", request.RequestUri?.ToString());
            AssertAuth(request);
            return JsonResponse(HttpStatusCode.OK, "{\"shipping_profile_id\":9,\"origin_country_iso\":\"GB\"}");
        });

        var result = await service.GetShopShippingProfileAsync(Token, 123, 9);

        Assert.True(result.Success);
        Assert.Equal("GB", result.Data?.OriginCountryIso);
    }

    [Fact]
    public async Task UpdateShopShippingProfileAsync_ValidRequest_SendsForm()
    {
        var service = CreateService(async request =>
        {
            Assert.Equal(HttpMethod.Put, request.Method);
            Assert.Equal("https://openapi.etsy.com/v3/application/shops/123/shipping-profiles/9", request.RequestUri?.ToString());
            AssertAuth(request);
            Assert.Equal("title=Express&origin_postal_code=SW1A+1AA", await request.Content!.ReadAsStringAsync());
            return JsonResponse(HttpStatusCode.OK, "{\"shipping_profile_id\":9,\"title\":\"Express\"}");
        });

        var result = await service.UpdateShopShippingProfileAsync(Token, 123, 9, new UpdateShopShippingProfileRequest { Title = "Express", OriginPostalCode = "SW1A 1AA" });

        Assert.True(result.Success);
        Assert.Equal("Express", result.Data?.Title);
    }

    [Fact]
    public async Task CreateShopShippingProfileDestinationAsync_ValidRequest_SendsForm()
    {
        var service = CreateService(async request =>
        {
            Assert.Equal(HttpMethod.Post, request.Method);
            Assert.Equal("https://openapi.etsy.com/v3/application/shops/123/shipping-profiles/9/destinations", request.RequestUri?.ToString());
            AssertAuth(request);
            Assert.Equal("primary_cost=4&secondary_cost=2&destination_region=eu&min_delivery_days=3&max_delivery_days=7", await request.Content!.ReadAsStringAsync());
            return JsonResponse(HttpStatusCode.OK, "{\"shipping_profile_destination_id\":12,\"destination_region\":\"eu\"}");
        });

        var result = await service.CreateShopShippingProfileDestinationAsync(Token, 123, 9, DestinationRequest());

        Assert.True(result.Success);
        Assert.Equal(12, result.Data?.ShippingProfileDestinationId);
    }

    [Fact]
    public async Task GetShopShippingProfileDestinationsAsync_Filter_EncodesPaginationAndParsesList()
    {
        var service = CreateService(request =>
        {
            Assert.Equal(HttpMethod.Get, request.Method);
            Assert.Equal("https://openapi.etsy.com/v3/application/shops/123/shipping-profiles/9/destinations?limit=50&offset=5", request.RequestUri?.ToString());
            AssertAuth(request);
            return JsonResponse(HttpStatusCode.OK, "{\"count\":1,\"results\":[{\"shipping_profile_destination_id\":12,\"destination_country_iso\":\"US\"}]}");
        });

        var result = await service.GetShopShippingProfileDestinationsAsync(Token, 123, 9, new GetShopShippingProfileDestinationsFilter { Limit = 50, Offset = 5 });

        Assert.True(result.Success);
        Assert.Equal("US", result.Data?.Results.Single().DestinationCountryIso);
    }

    [Fact]
    public async Task DeleteShopShippingProfileDestinationAsync_ValidRequest_UsesDeleteRoute()
    {
        var service = CreateService(request =>
        {
            Assert.Equal(HttpMethod.Delete, request.Method);
            Assert.Equal("https://openapi.etsy.com/v3/application/shops/123/shipping-profiles/9/destinations/12", request.RequestUri?.ToString());
            AssertAuth(request);
            return EmptyResponse();
        });

        var result = await service.DeleteShopShippingProfileDestinationAsync(Token, 123, 9, 12);

        Assert.True(result.Success);
    }

    [Fact]
    public async Task UpdateShopShippingProfileDestinationAsync_ValidRequest_SendsForm()
    {
        var service = CreateService(async request =>
        {
            Assert.Equal(HttpMethod.Put, request.Method);
            Assert.Equal("https://openapi.etsy.com/v3/application/shops/123/shipping-profiles/9/destinations/12", request.RequestUri?.ToString());
            AssertAuth(request);
            Assert.Equal("primary_cost=5.5", await request.Content!.ReadAsStringAsync());
            return JsonResponse(HttpStatusCode.OK, "{\"shipping_profile_destination_id\":12,\"primary_cost\":{\"amount\":550,\"divisor\":100,\"currency_code\":\"GBP\"}}");
        });

        var result = await service.UpdateShopShippingProfileDestinationAsync(Token, 123, 9, 12, new UpdateShopShippingProfileDestinationRequest { PrimaryCost = 5.5f });

        Assert.True(result.Success);
        Assert.Equal(12, result.Data?.ShippingProfileDestinationId);
    }

    [Fact]
    public async Task CreateShopShippingProfileUpgradeAsync_ValidRequest_SendsForm()
    {
        var service = CreateService(async request =>
        {
            Assert.Equal(HttpMethod.Post, request.Method);
            Assert.Equal("https://openapi.etsy.com/v3/application/shops/123/shipping-profiles/9/upgrades", request.RequestUri?.ToString());
            AssertAuth(request);
            Assert.Equal("type=0&upgrade_name=Priority&price=2.5&secondary_price=1&min_delivery_days=1&max_delivery_days=2", await request.Content!.ReadAsStringAsync());
            return JsonResponse(HttpStatusCode.OK, "{\"shipping_profile_id\":9,\"upgrade_id\":4,\"upgrade_name\":\"Priority\",\"type\":0}");
        });

        var result = await service.CreateShopShippingProfileUpgradeAsync(Token, 123, 9, UpgradeRequest());

        Assert.True(result.Success);
        Assert.Equal(4, result.Data?.UpgradeId);
    }

    [Fact]
    public async Task GetShopShippingProfileUpgradesAsync_ValidRequest_ParsesList()
    {
        var service = CreateService(request =>
        {
            Assert.Equal(HttpMethod.Get, request.Method);
            Assert.Equal("https://openapi.etsy.com/v3/application/shops/123/shipping-profiles/9/upgrades", request.RequestUri?.ToString());
            AssertAuth(request);
            return JsonResponse(HttpStatusCode.OK, "{\"count\":1,\"results\":[{\"upgrade_id\":4,\"upgrade_name\":\"Priority\",\"type\":0}]}");
        });

        var result = await service.GetShopShippingProfileUpgradesAsync(Token, 123, 9);

        Assert.True(result.Success);
        Assert.Equal("Priority", result.Data?.Results.Single().UpgradeName);
    }

    [Fact]
    public async Task DeleteShopShippingProfileUpgradeAsync_ValidRequest_UsesDeleteRoute()
    {
        var service = CreateService(request =>
        {
            Assert.Equal(HttpMethod.Delete, request.Method);
            Assert.Equal("https://openapi.etsy.com/v3/application/shops/123/shipping-profiles/9/upgrades/4", request.RequestUri?.ToString());
            AssertAuth(request);
            return EmptyResponse();
        });

        var result = await service.DeleteShopShippingProfileUpgradeAsync(Token, 123, 9, 4);

        Assert.True(result.Success);
    }

    [Fact]
    public async Task UpdateShopShippingProfileUpgradeAsync_ValidRequest_SendsForm()
    {
        var service = CreateService(async request =>
        {
            Assert.Equal(HttpMethod.Put, request.Method);
            Assert.Equal("https://openapi.etsy.com/v3/application/shops/123/shipping-profiles/9/upgrades/4", request.RequestUri?.ToString());
            AssertAuth(request);
            Assert.Equal("upgrade_name=Next+day&price=4", await request.Content!.ReadAsStringAsync());
            return JsonResponse(HttpStatusCode.OK, "{\"shipping_profile_id\":9,\"upgrade_id\":4,\"upgrade_name\":\"Next day\",\"type\":0}");
        });

        var result = await service.UpdateShopShippingProfileUpgradeAsync(Token, 123, 9, 4, new UpdateShopShippingProfileUpgradeRequest { UpgradeName = "Next day", Price = 4 });

        Assert.True(result.Success);
        Assert.Equal("Next day", result.Data?.UpgradeName);
    }

    [Fact]
    public async Task ShippingOperations_InvalidInputs_ThrowBeforeSendingRequest()
    {
        var service = CreateService(new Func<HttpRequestMessage, HttpResponseMessage>(_ => throw new InvalidOperationException("No request was expected.")));

        await Assert.ThrowsAsync<ArgumentException>(() => service.GetShippingCarriersAsync("GBR"));
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => service.GetShopShippingProfilesAsync(Token, 0));
        await Assert.ThrowsAsync<ArgumentException>(() => service.CreateShopShippingProfileAsync(Token, 123, new CreateShopShippingProfileRequest { Title = "Profile", OriginCountryIso = "GB" }));
        await Assert.ThrowsAsync<ArgumentException>(() => service.UpdateShopShippingProfileAsync(Token, 123, 9, new UpdateShopShippingProfileRequest()));
        await Assert.ThrowsAsync<ArgumentException>(() => service.CreateShopShippingProfileDestinationAsync(Token, 123, 9, new CreateShopShippingProfileDestinationRequest()));
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => service.GetShopShippingProfileDestinationsAsync(Token, 123, 9, new GetShopShippingProfileDestinationsFilter { Limit = 101 }));
        await Assert.ThrowsAsync<ArgumentException>(() => service.CreateShopShippingProfileUpgradeAsync(Token, 123, 9, new CreateShopShippingProfileUpgradeRequest { UpgradeName = "Upgrade", Price = 1, SecondaryPrice = 1 }));
        await Assert.ThrowsAsync<ArgumentException>(() => service.GetShopShippingProfileUpgradesAsync(" ", 123, 9));
    }

    [Fact]
    public async Task GetShippingCarriersAsync_CancelledRequest_PropagatesCancellation()
    {
        var service = CreateService(async (_, cancellationToken) =>
        {
            await Task.Delay(Timeout.InfiniteTimeSpan, cancellationToken);
            throw new InvalidOperationException("The cancelled request unexpectedly completed.");
        });
        using var cancellationSource = new CancellationTokenSource();
        cancellationSource.Cancel();

        await Assert.ThrowsAnyAsync<OperationCanceledException>(() => service.GetShippingCarriersAsync("GB", cancellationSource.Token));
    }

    [Fact]
    public void ShippingService_RegisteredAtEachLifetime_ResolvesFromDependencyInjection()
    {
        var services = new ServiceCollection();
        services.AddEtsyShippingManagementServiceScoped("client-id", "shared-secret");
        services.AddEtsyShippingManagementServiceTransient("client-id", "shared-secret");
        services.AddEtsyShippingManagementServiceSingleton("client-id", "shared-secret");
        using var provider = services.BuildServiceProvider();

        Assert.IsType<EtsyShippingManagementService>(provider.GetRequiredService<IEtsyShippingManagementService>());
    }

    private const string Token = "123.access-token";

    private static CreateShopShippingProfileRequest ProfileRequest() => new()
    {
        Title = "Standard & Tracked",
        OriginCountryIso = "GB",
        PrimaryCost = 3.5f,
        SecondaryCost = 1.25f,
        MinProcessingTime = 1,
        MaxProcessingTime = 3,
        ProcessingTimeUnit = ShippingProcessingTimeUnit.BusinessDays,
        DestinationCountryIso = "US",
        MinDeliveryDays = 5,
        MaxDeliveryDays = 10
    };

    private static CreateShopShippingProfileDestinationRequest DestinationRequest() => new()
    {
        PrimaryCost = 4,
        SecondaryCost = 2,
        DestinationRegion = ShippingDestinationRegion.Eu,
        MinDeliveryDays = 3,
        MaxDeliveryDays = 7
    };

    private static CreateShopShippingProfileUpgradeRequest UpgradeRequest() => new()
    {
        Type = ShippingProfileUpgradeType.Domestic,
        UpgradeName = "Priority",
        Price = 2.5f,
        SecondaryPrice = 1,
        MinDeliveryDays = 1,
        MaxDeliveryDays = 2
    };

    private static EtsyShippingManagementService CreateService(Func<HttpRequestMessage, HttpResponseMessage> sendAsync) => CreateService((request, _) => Task.FromResult(sendAsync(request)));
    private static EtsyShippingManagementService CreateService(Func<HttpRequestMessage, Task<HttpResponseMessage>> sendAsync) => CreateService((request, _) => sendAsync(request));
    private static EtsyShippingManagementService CreateService(Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>> sendAsync) => new(new StubHttpClientFactory(new StubHttpMessageHandler(sendAsync)), "client-id", "shared-secret");
    private static HttpResponseMessage JsonResponse(HttpStatusCode statusCode, string body) => new(statusCode) { Content = new StringContent(body, Encoding.UTF8, "application/json") };
    private static HttpResponseMessage EmptyResponse() => new(HttpStatusCode.NoContent);

    private static void AssertAuth(HttpRequestMessage request)
    {
        Assert.Equal($"Bearer {Token}", request.Headers.Authorization?.ToString());
        Assert.Equal("client-id:shared-secret", request.Headers.GetValues("x-api-key").Single());
        if (request.Content is not null)
            Assert.Equal("application/x-www-form-urlencoded", request.Content.Headers.ContentType?.MediaType);
    }

    private sealed class StubHttpClientFactory(HttpMessageHandler handler) : IHttpClientFactory
    {
        private readonly HttpClient httpClient = new(handler);
        public HttpClient CreateClient(string name) => httpClient;
    }

    private sealed class StubHttpMessageHandler(Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>> sendAsync) : HttpMessageHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken) => sendAsync(request, cancellationToken);
    }
}
