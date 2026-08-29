using EtsyApiSharp.Helpers;
using EtsyApiSharp.Models;
using EtsyApiSharp.Models.Common;
using EtsyApiSharp.Models.Filters;
using EtsyApiSharp.Models.ShopShippings.Enums;
using System.Globalization;
using System.Net.Http.Headers;

namespace EtsyApiSharp.Services.ShippingManagements;
/// <summary>
/// Represents Etsy Shipping Management Service.
/// </summary>

public sealed class EtsyShippingManagementService : IEtsyShippingManagementService
{
    /// <summary>
    /// The Http Client Name value.
    /// </summary>
    public const string HttpClientName = "EtsyApiSharp.Shipping";

    private readonly string apiKey;
    private readonly IHttpClientFactory httpClientFactory;
    /// <summary>
    /// Initializes a new instance of the EtsyShippingManagementService class.
    /// </summary>

    public EtsyShippingManagementService(IHttpClientFactory httpClientFactory, string clientId, string sharedSecret)
    {
        ArgumentNullException.ThrowIfNull(httpClientFactory);
        if (string.IsNullOrWhiteSpace(clientId))
            throw new ArgumentException("An Etsy API key keystring is required.", nameof(clientId));
        if (string.IsNullOrWhiteSpace(sharedSecret))
            throw new ArgumentException("An Etsy API shared secret is required.", nameof(sharedSecret));

        this.httpClientFactory = httpClientFactory;
        apiKey = $"{clientId}:{sharedSecret}";
    }
    /// <summary>
    /// Executes the Get Shipping Carriers operation.
    /// </summary>

    public Task<ApiResponse<EtsyListResponse<ShippingCarrier>>> GetShippingCarriersAsync(string originCountryIso, CancellationToken cancellationToken = default)
    {
        ValidateCountryIso(originCountryIso, nameof(originCountryIso));
        return SendListAsync<ShippingCarrier>(HttpMethod.Get, Url.ShippingUrls.GetShippingCarriers(), new Dictionary<string, string> { ["origin_country_iso"] = originCountryIso }, null, null, cancellationToken);
    }
    /// <summary>
    /// Executes the Create Shop Shipping Profile operation.
    /// </summary>

    public Task<ApiResponse<ShopShippingProfile>> CreateShopShippingProfileAsync(string accessToken, long shopId, CreateShopShippingProfileRequest profile, CancellationToken cancellationToken = default)
    {
        ValidateAccessToken(accessToken);
        ValidateId(shopId, nameof(shopId));
        ArgumentNullException.ThrowIfNull(profile);
        ValidateCreateProfile(profile);
        return SendAsync<ShopShippingProfile>(HttpMethod.Post, Url.ShippingUrls.GetShopShippingProfiles(shopId), null, accessToken, CreateProfileContent(profile), cancellationToken);
    }
    /// <summary>
    /// Executes the Get Shop Shipping Profiles operation.
    /// </summary>

    public Task<ApiResponse<EtsyListResponse<ShopShippingProfile>>> GetShopShippingProfilesAsync(string accessToken, long shopId, CancellationToken cancellationToken = default)
    {
        ValidateAccessToken(accessToken);
        ValidateId(shopId, nameof(shopId));
        return SendListAsync<ShopShippingProfile>(HttpMethod.Get, Url.ShippingUrls.GetShopShippingProfiles(shopId), null, accessToken, null, cancellationToken);
    }
    /// <summary>
    /// Executes the Delete Shop Shipping Profile operation.
    /// </summary>

    public Task<ApiResponse<object>> DeleteShopShippingProfileAsync(string accessToken, long shopId, long shippingProfileId, CancellationToken cancellationToken = default)
    {
        ValidateWriteIds(accessToken, shopId, shippingProfileId);
        return SendAsync<object>(HttpMethod.Delete, Url.ShippingUrls.GetShopShippingProfile(shopId, shippingProfileId), null, accessToken, null, cancellationToken);
    }
    /// <summary>
    /// Executes the Get Shop Shipping Profile operation.
    /// </summary>

    public Task<ApiResponse<ShopShippingProfile>> GetShopShippingProfileAsync(string accessToken, long shopId, long shippingProfileId, CancellationToken cancellationToken = default)
    {
        ValidateReadIds(accessToken, shopId, shippingProfileId);
        return SendAsync<ShopShippingProfile>(HttpMethod.Get, Url.ShippingUrls.GetShopShippingProfile(shopId, shippingProfileId), null, accessToken, null, cancellationToken);
    }
    /// <summary>
    /// Executes the Update Shop Shipping Profile operation.
    /// </summary>

    public Task<ApiResponse<ShopShippingProfile>> UpdateShopShippingProfileAsync(string accessToken, long shopId, long shippingProfileId, UpdateShopShippingProfileRequest profile, CancellationToken cancellationToken = default)
    {
        ValidateWriteIds(accessToken, shopId, shippingProfileId);
        ArgumentNullException.ThrowIfNull(profile);
        ValidateUpdateProfile(profile);
        return SendAsync<ShopShippingProfile>(HttpMethod.Put, Url.ShippingUrls.GetShopShippingProfile(shopId, shippingProfileId), null, accessToken, CreateProfileContent(profile), cancellationToken);
    }
    /// <summary>
    /// Executes the Create Shop Shipping Profile Destination operation.
    /// </summary>

    public Task<ApiResponse<ShopShippingProfileDestination>> CreateShopShippingProfileDestinationAsync(string accessToken, long shopId, long shippingProfileId, CreateShopShippingProfileDestinationRequest destination, CancellationToken cancellationToken = default)
    {
        ValidateWriteIds(accessToken, shopId, shippingProfileId);
        ArgumentNullException.ThrowIfNull(destination);
        ValidateCreateDestination(destination);
        return SendAsync<ShopShippingProfileDestination>(HttpMethod.Post, Url.ShippingUrls.GetShopShippingProfileDestinations(shopId, shippingProfileId), null, accessToken, CreateDestinationContent(destination), cancellationToken);
    }
    /// <summary>
    /// Executes the Get Shop Shipping Profile Destinations operation.
    /// </summary>

    public Task<ApiResponse<EtsyListResponse<ShopShippingProfileDestination>>> GetShopShippingProfileDestinationsAsync(string accessToken, long shopId, long shippingProfileId, GetShopShippingProfileDestinationsFilter? filter = null, CancellationToken cancellationToken = default)
    {
        ValidateReadIds(accessToken, shopId, shippingProfileId);
        ValidatePagination(filter);
        return SendListAsync<ShopShippingProfileDestination>(HttpMethod.Get, Url.ShippingUrls.GetShopShippingProfileDestinations(shopId, shippingProfileId), CreatePaginationQuery(filter), accessToken, null, cancellationToken);
    }
    /// <summary>
    /// Executes the Delete Shop Shipping Profile Destination operation.
    /// </summary>

    public Task<ApiResponse<object>> DeleteShopShippingProfileDestinationAsync(string accessToken, long shopId, long shippingProfileId, long shippingProfileDestinationId, CancellationToken cancellationToken = default)
    {
        ValidateWriteIds(accessToken, shopId, shippingProfileId);
        ValidateId(shippingProfileDestinationId, nameof(shippingProfileDestinationId));
        return SendAsync<object>(HttpMethod.Delete, Url.ShippingUrls.GetShopShippingProfileDestination(shopId, shippingProfileId, shippingProfileDestinationId), null, accessToken, null, cancellationToken);
    }
    /// <summary>
    /// Executes the Update Shop Shipping Profile Destination operation.
    /// </summary>

    public Task<ApiResponse<ShopShippingProfileDestination>> UpdateShopShippingProfileDestinationAsync(string accessToken, long shopId, long shippingProfileId, long shippingProfileDestinationId, UpdateShopShippingProfileDestinationRequest destination, CancellationToken cancellationToken = default)
    {
        ValidateWriteIds(accessToken, shopId, shippingProfileId);
        ValidateId(shippingProfileDestinationId, nameof(shippingProfileDestinationId));
        ArgumentNullException.ThrowIfNull(destination);
        ValidateUpdateDestination(destination);
        return SendAsync<ShopShippingProfileDestination>(HttpMethod.Put, Url.ShippingUrls.GetShopShippingProfileDestination(shopId, shippingProfileId, shippingProfileDestinationId), null, accessToken, CreateDestinationContent(destination), cancellationToken);
    }
    /// <summary>
    /// Executes the Create Shop Shipping Profile Upgrade operation.
    /// </summary>

    public Task<ApiResponse<ShopShippingProfileUpgrade>> CreateShopShippingProfileUpgradeAsync(string accessToken, long shopId, long shippingProfileId, CreateShopShippingProfileUpgradeRequest upgrade, CancellationToken cancellationToken = default)
    {
        ValidateWriteIds(accessToken, shopId, shippingProfileId);
        ArgumentNullException.ThrowIfNull(upgrade);
        ValidateCreateUpgrade(upgrade);
        return SendAsync<ShopShippingProfileUpgrade>(HttpMethod.Post, Url.ShippingUrls.GetShopShippingProfileUpgrades(shopId, shippingProfileId), null, accessToken, CreateUpgradeContent(upgrade), cancellationToken);
    }
    /// <summary>
    /// Executes the Get Shop Shipping Profile Upgrades operation.
    /// </summary>

    public Task<ApiResponse<EtsyListResponse<ShopShippingProfileUpgrade>>> GetShopShippingProfileUpgradesAsync(string accessToken, long shopId, long shippingProfileId, CancellationToken cancellationToken = default)
    {
        ValidateReadIds(accessToken, shopId, shippingProfileId);
        return SendListAsync<ShopShippingProfileUpgrade>(HttpMethod.Get, Url.ShippingUrls.GetShopShippingProfileUpgrades(shopId, shippingProfileId), null, accessToken, null, cancellationToken);
    }
    /// <summary>
    /// Executes the Delete Shop Shipping Profile Upgrade operation.
    /// </summary>

    public Task<ApiResponse<object>> DeleteShopShippingProfileUpgradeAsync(string accessToken, long shopId, long shippingProfileId, long upgradeId, CancellationToken cancellationToken = default)
    {
        ValidateWriteIds(accessToken, shopId, shippingProfileId);
        ValidateId(upgradeId, nameof(upgradeId));
        return SendAsync<object>(HttpMethod.Delete, Url.ShippingUrls.GetShopShippingProfileUpgrade(shopId, shippingProfileId, upgradeId), null, accessToken, null, cancellationToken);
    }
    /// <summary>
    /// Executes the Update Shop Shipping Profile Upgrade operation.
    /// </summary>

    public Task<ApiResponse<ShopShippingProfileUpgrade>> UpdateShopShippingProfileUpgradeAsync(string accessToken, long shopId, long shippingProfileId, long upgradeId, UpdateShopShippingProfileUpgradeRequest upgrade, CancellationToken cancellationToken = default)
    {
        ValidateWriteIds(accessToken, shopId, shippingProfileId);
        ValidateId(upgradeId, nameof(upgradeId));
        ArgumentNullException.ThrowIfNull(upgrade);
        ValidateUpdateUpgrade(upgrade);
        return SendAsync<ShopShippingProfileUpgrade>(HttpMethod.Put, Url.ShippingUrls.GetShopShippingProfileUpgrade(shopId, shippingProfileId, upgradeId), null, accessToken, CreateUpgradeContent(upgrade), cancellationToken);
    }

    private async Task<ApiResponse<T>> SendAsync<T>(HttpMethod method, string relativeUrl, IReadOnlyDictionary<string, string>? query, string? accessToken, HttpContent? content, CancellationToken cancellationToken)
    {
        using var request = CreateRequest(method, relativeUrl, query, accessToken, content);
        using var response = await httpClientFactory.CreateClient(HttpClientName).SendAsync(request, cancellationToken).ConfigureAwait(false);
        response.RequestMessage ??= request;
        return await EtsyResponseParser.ParseResponseOfSingle<T>(response, cancellationToken).ConfigureAwait(false);
    }

    private async Task<ApiResponse<EtsyListResponse<T>>> SendListAsync<T>(HttpMethod method, string relativeUrl, IReadOnlyDictionary<string, string>? query, string? accessToken, HttpContent? content, CancellationToken cancellationToken)
    {
        using var request = CreateRequest(method, relativeUrl, query, accessToken, content);
        using var response = await httpClientFactory.CreateClient(HttpClientName).SendAsync(request, cancellationToken).ConfigureAwait(false);
        response.RequestMessage ??= request;
        return await EtsyResponseParser.ParseResponseOfList<T>(response, cancellationToken).ConfigureAwait(false);
    }

    private HttpRequestMessage CreateRequest(HttpMethod method, string relativeUrl, IReadOnlyDictionary<string, string>? query, string? accessToken, HttpContent? content)
    {
        var request = new HttpRequestMessage(method, BuildUri(relativeUrl, query)) { Content = content };
        if (accessToken is not null)
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        request.Headers.Add("x-api-key", apiKey);
        return request;
    }

    private static Uri BuildUri(string relativeUrl, IReadOnlyDictionary<string, string>? query)
    {
        var uriBuilder = new UriBuilder($"{Url.BaseUrls.BaseApiUrl}{relativeUrl}");
        if (query is { Count: > 0 })
            uriBuilder.Query = string.Join("&", query.Select(pair => $"{Uri.EscapeDataString(pair.Key)}={Uri.EscapeDataString(pair.Value)}"));
        return uriBuilder.Uri;
    }

    private static IReadOnlyDictionary<string, string>? CreatePaginationQuery(GetShopShippingProfileDestinationsFilter? filter)
    {
        if (filter is null)
            return null;
        var query = new Dictionary<string, string>();
        if (filter.Limit != 25)
            query["limit"] = filter.Limit.ToString(CultureInfo.InvariantCulture);
        if (filter.Offset != 0)
            query["offset"] = filter.Offset.ToString(CultureInfo.InvariantCulture);
        return query;
    }

    private static FormUrlEncodedContent CreateProfileContent(CreateShopShippingProfileRequest profile)
    {
        var form = new Dictionary<string, string>();
        AddString(form, "title", profile.Title); AddString(form, "origin_country_iso", profile.OriginCountryIso);
        AddFloat(form, "primary_cost", profile.PrimaryCost); AddFloat(form, "secondary_cost", profile.SecondaryCost);
        AddInt64(form, "min_processing_time", profile.MinProcessingTime); AddInt64(form, "max_processing_time", profile.MaxProcessingTime);
        if (profile.ProcessingTimeUnit.HasValue) form["processing_time_unit"] = Format(profile.ProcessingTimeUnit.Value);
        AddString(form, "destination_country_iso", profile.DestinationCountryIso); if (profile.DestinationRegion.HasValue) form["destination_region"] = Format(profile.DestinationRegion.Value);
        AddString(form, "origin_postal_code", profile.OriginPostalCode); AddInt64(form, "shipping_carrier_id", profile.ShippingCarrierId); AddString(form, "mail_class", profile.MailClass);
        AddInt64(form, "min_delivery_days", profile.MinDeliveryDays); AddInt64(form, "max_delivery_days", profile.MaxDeliveryDays);
        return new FormUrlEncodedContent(form);
    }

    private static FormUrlEncodedContent CreateProfileContent(UpdateShopShippingProfileRequest profile)
    {
        var form = new Dictionary<string, string>();
        AddString(form, "title", profile.Title); AddString(form, "origin_country_iso", profile.OriginCountryIso);
        AddInt64(form, "min_processing_time", profile.MinProcessingTime); AddInt64(form, "max_processing_time", profile.MaxProcessingTime);
        if (profile.ProcessingTimeUnit.HasValue) form["processing_time_unit"] = Format(profile.ProcessingTimeUnit.Value);
        AddString(form, "origin_postal_code", profile.OriginPostalCode);
        return new FormUrlEncodedContent(form);
    }

    private static FormUrlEncodedContent CreateDestinationContent(CreateShopShippingProfileDestinationRequest destination)
    {
        var form = new Dictionary<string, string>();
        AddFloat(form, "primary_cost", destination.PrimaryCost); AddFloat(form, "secondary_cost", destination.SecondaryCost);
        AddString(form, "destination_country_iso", destination.DestinationCountryIso); if (destination.DestinationRegion.HasValue) form["destination_region"] = Format(destination.DestinationRegion.Value);
        AddInt64(form, "shipping_carrier_id", destination.ShippingCarrierId); AddString(form, "mail_class", destination.MailClass);
        AddInt64(form, "min_delivery_days", destination.MinDeliveryDays); AddInt64(form, "max_delivery_days", destination.MaxDeliveryDays);
        return new FormUrlEncodedContent(form);
    }

    private static FormUrlEncodedContent CreateDestinationContent(UpdateShopShippingProfileDestinationRequest destination)
    {
        var form = new Dictionary<string, string>();
        AddFloat(form, "primary_cost", destination.PrimaryCost); AddFloat(form, "secondary_cost", destination.SecondaryCost);
        AddString(form, "destination_country_iso", destination.DestinationCountryIso); if (destination.DestinationRegion.HasValue) form["destination_region"] = Format(destination.DestinationRegion.Value);
        AddInt64(form, "shipping_carrier_id", destination.ShippingCarrierId); AddString(form, "mail_class", destination.MailClass);
        AddInt64(form, "min_delivery_days", destination.MinDeliveryDays); AddInt64(form, "max_delivery_days", destination.MaxDeliveryDays);
        return new FormUrlEncodedContent(form);
    }

    private static FormUrlEncodedContent CreateUpgradeContent(CreateShopShippingProfileUpgradeRequest upgrade)
    {
        var form = new Dictionary<string, string> { ["type"] = ((int)upgrade.Type).ToString(CultureInfo.InvariantCulture) };
        AddString(form, "upgrade_name", upgrade.UpgradeName); AddFloat(form, "price", upgrade.Price); AddFloat(form, "secondary_price", upgrade.SecondaryPrice);
        AddInt64(form, "shipping_carrier_id", upgrade.ShippingCarrierId); AddString(form, "mail_class", upgrade.MailClass); AddInt64(form, "min_delivery_days", upgrade.MinDeliveryDays); AddInt64(form, "max_delivery_days", upgrade.MaxDeliveryDays);
        return new FormUrlEncodedContent(form);
    }

    private static FormUrlEncodedContent CreateUpgradeContent(UpdateShopShippingProfileUpgradeRequest upgrade)
    {
        var form = new Dictionary<string, string>();
        AddString(form, "upgrade_name", upgrade.UpgradeName); if (upgrade.Type.HasValue) form["type"] = ((int)upgrade.Type.Value).ToString(CultureInfo.InvariantCulture);
        AddFloat(form, "price", upgrade.Price); AddFloat(form, "secondary_price", upgrade.SecondaryPrice); AddInt64(form, "shipping_carrier_id", upgrade.ShippingCarrierId);
        AddString(form, "mail_class", upgrade.MailClass); AddInt64(form, "min_delivery_days", upgrade.MinDeliveryDays); AddInt64(form, "max_delivery_days", upgrade.MaxDeliveryDays);
        return new FormUrlEncodedContent(form);
    }

    private static void AddString(Dictionary<string, string> form, string key, string? value) { if (value is not null) form[key] = value; }
    private static void AddInt64(Dictionary<string, string> form, string key, long? value) { if (value.HasValue) form[key] = value.Value.ToString(CultureInfo.InvariantCulture); }
    private static void AddFloat(Dictionary<string, string> form, string key, float value) => form[key] = value.ToString(CultureInfo.InvariantCulture);
    private static void AddFloat(Dictionary<string, string> form, string key, float? value) { if (value.HasValue) AddFloat(form, key, value.Value); }
    private static string Format(ShippingProcessingTimeUnit value) => value == ShippingProcessingTimeUnit.BusinessDays ? "business_days" : "weeks";
    private static string Format(ShippingDestinationRegion value) => value switch { ShippingDestinationRegion.Eu => "eu", ShippingDestinationRegion.NonEu => "non_eu", _ => "none" };

    private static void ValidateCreateProfile(CreateShopShippingProfileRequest profile)
    {
        if (string.IsNullOrWhiteSpace(profile.Title)) throw new ArgumentException("A shipping profile title is required.", nameof(profile));
        ValidateCountryIso(profile.OriginCountryIso, nameof(profile)); ValidateCost(profile.PrimaryCost, nameof(profile)); ValidateCost(profile.SecondaryCost, nameof(profile));
        ValidateProcessingTimes(profile.MinProcessingTime, profile.MaxProcessingTime, nameof(profile)); ValidateDestination(profile.DestinationCountryIso, profile.DestinationRegion, nameof(profile));
        ValidateDelivery(profile.ShippingCarrierId, profile.MailClass, profile.MinDeliveryDays, profile.MaxDeliveryDays, true, nameof(profile));
    }

    private static void ValidateUpdateProfile(UpdateShopShippingProfileRequest profile)
    {
        if (profile.Title is null && profile.OriginCountryIso is null && profile.MinProcessingTime is null && profile.MaxProcessingTime is null && profile.ProcessingTimeUnit is null && profile.OriginPostalCode is null) throw new ArgumentException("At least one shipping profile value must be supplied.", nameof(profile));
        if (profile.OriginCountryIso is not null) ValidateCountryIso(profile.OriginCountryIso, nameof(profile));
        ValidateProcessingTimes(profile.MinProcessingTime, profile.MaxProcessingTime, nameof(profile));
    }

    private static void ValidateCreateDestination(CreateShopShippingProfileDestinationRequest destination)
    {
        ValidateCost(destination.PrimaryCost, nameof(destination)); ValidateCost(destination.SecondaryCost, nameof(destination)); ValidateDestination(destination.DestinationCountryIso, destination.DestinationRegion, nameof(destination));
        ValidateDelivery(destination.ShippingCarrierId, destination.MailClass, destination.MinDeliveryDays, destination.MaxDeliveryDays, true, nameof(destination));
    }

    private static void ValidateUpdateDestination(UpdateShopShippingProfileDestinationRequest destination)
    {
        if (destination.PrimaryCost is null && destination.SecondaryCost is null && destination.DestinationCountryIso is null && destination.DestinationRegion is null && destination.ShippingCarrierId is null && destination.MailClass is null && destination.MinDeliveryDays is null && destination.MaxDeliveryDays is null) throw new ArgumentException("At least one destination value must be supplied.", nameof(destination));
        if (destination.PrimaryCost.HasValue) ValidateCost(destination.PrimaryCost.Value, nameof(destination)); if (destination.SecondaryCost.HasValue) ValidateCost(destination.SecondaryCost.Value, nameof(destination));
        ValidateDestination(destination.DestinationCountryIso, destination.DestinationRegion, nameof(destination), required: false);
        ValidateDelivery(destination.ShippingCarrierId, destination.MailClass, destination.MinDeliveryDays, destination.MaxDeliveryDays, false, nameof(destination));
    }

    private static void ValidateCreateUpgrade(CreateShopShippingProfileUpgradeRequest upgrade)
    {
        if (!Enum.IsDefined(upgrade.Type)) throw new ArgumentException("The shipping upgrade type is invalid.", nameof(upgrade));
        if (string.IsNullOrWhiteSpace(upgrade.UpgradeName)) throw new ArgumentException("A shipping upgrade name is required.", nameof(upgrade));
        ValidateCost(upgrade.Price, nameof(upgrade)); ValidateCost(upgrade.SecondaryPrice, nameof(upgrade)); ValidateDelivery(upgrade.ShippingCarrierId, upgrade.MailClass, upgrade.MinDeliveryDays, upgrade.MaxDeliveryDays, true, nameof(upgrade));
    }

    private static void ValidateUpdateUpgrade(UpdateShopShippingProfileUpgradeRequest upgrade)
    {
        if (upgrade.UpgradeName is null && upgrade.Type is null && upgrade.Price is null && upgrade.SecondaryPrice is null && upgrade.ShippingCarrierId is null && upgrade.MailClass is null && upgrade.MinDeliveryDays is null && upgrade.MaxDeliveryDays is null) throw new ArgumentException("At least one shipping upgrade value must be supplied.", nameof(upgrade));
        if (upgrade.Type.HasValue && !Enum.IsDefined(upgrade.Type.Value)) throw new ArgumentException("The shipping upgrade type is invalid.", nameof(upgrade));
        if (upgrade.Price.HasValue) ValidateCost(upgrade.Price.Value, nameof(upgrade)); if (upgrade.SecondaryPrice.HasValue) ValidateCost(upgrade.SecondaryPrice.Value, nameof(upgrade));
        ValidateDelivery(upgrade.ShippingCarrierId, upgrade.MailClass, upgrade.MinDeliveryDays, upgrade.MaxDeliveryDays, false, nameof(upgrade));
    }

    private static void ValidateDestination(string? countryIso, ShippingDestinationRegion? region, string parameterName, bool required = true)
    {
        if (countryIso is not null) ValidateCountryIso(countryIso, parameterName);
        if (required && countryIso is null && region is null) throw new ArgumentException("A destination country or region is required.", parameterName);
        if (region == ShippingDestinationRegion.None && countryIso is null) throw new ArgumentException("DestinationRegion None requires a destination country.", parameterName);
    }

    private static void ValidateProcessingTimes(long? minimum, long? maximum, string parameterName)
    {
        if (minimum.HasValue != maximum.HasValue) throw new ArgumentException("Processing times must be supplied together.", parameterName);
        if (!minimum.HasValue) return;
        if (minimum is < 1 or > 10 || maximum is < 1 or > 10 || minimum > maximum) throw new ArgumentOutOfRangeException(parameterName, "Processing times must be between 1 and 10, with the minimum no greater than the maximum.");
    }

    private static void ValidateDelivery(long? carrierId, string? mailClass, long? minimumDays, long? maximumDays, bool required, string parameterName)
    {
        var hasCarrier = carrierId.HasValue; var hasMailClass = !string.IsNullOrWhiteSpace(mailClass); var hasMinimum = minimumDays.HasValue; var hasMaximum = maximumDays.HasValue;
        if (hasCarrier != hasMailClass) throw new ArgumentException("ShippingCarrierId and MailClass must be supplied together.", parameterName);
        if (hasMinimum != hasMaximum) throw new ArgumentException("Delivery-day bounds must be supplied together.", parameterName);
        if (hasCarrier && carrierId < 0) throw new ArgumentOutOfRangeException(parameterName, "Shipping carrier IDs cannot be negative.");
        if (hasMinimum && (minimumDays is < 1 or > 45 || maximumDays is < 1 or > 45 || minimumDays > maximumDays)) throw new ArgumentOutOfRangeException(parameterName, "Delivery days must be between 1 and 45, with the minimum no greater than the maximum.");
        if (required && !hasCarrier && !hasMinimum) throw new ArgumentException("A carrier and mail class or delivery-day range is required.", parameterName);
    }

    private static void ValidateCost(float value, string parameterName) { if (float.IsNaN(value) || float.IsInfinity(value) || value < 0) throw new ArgumentOutOfRangeException(parameterName, "Shipping costs must be non-negative finite values."); }
    private static void ValidateCountryIso(string? value, string parameterName) { if (string.IsNullOrWhiteSpace(value) || value.Length != 2 || !value.All(char.IsLetter)) throw new ArgumentException("A two-letter ISO country code is required.", parameterName); }
    private static void ValidatePagination(EtsyFilterBase? filter) { if (filter is null) return; if (filter.Limit is < 1 or > 100) throw new ArgumentOutOfRangeException(nameof(filter), "The limit must be between 1 and 100."); if (filter.Offset < 0) throw new ArgumentOutOfRangeException(nameof(filter), "The offset cannot be negative."); }
    private static void ValidateAccessToken(string accessToken) { if (string.IsNullOrWhiteSpace(accessToken)) throw new ArgumentException("An Etsy OAuth access token is required.", nameof(accessToken)); }
    private static void ValidateId(long id, string parameterName) { if (id < 1) throw new ArgumentOutOfRangeException(parameterName, "Etsy IDs must be greater than zero."); }
    private static void ValidateReadIds(string accessToken, long shopId, long profileId) { ValidateAccessToken(accessToken); ValidateId(shopId, nameof(shopId)); ValidateId(profileId, nameof(profileId)); }
    private static void ValidateWriteIds(string accessToken, long shopId, long profileId) => ValidateReadIds(accessToken, shopId, profileId);
}
