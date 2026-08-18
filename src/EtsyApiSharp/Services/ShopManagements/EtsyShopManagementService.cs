using EtsyApiSharp.Helpers;
using EtsyApiSharp.Models;
using EtsyApiSharp.Models.Common;
using EtsyApiSharp.Models.Filters;
using System.Globalization;
using System.Net.Http.Headers;

namespace EtsyApiSharp.Services.ShopManagements;
/// <summary>
/// Represents Etsy Shop Management Service.
/// </summary>

public class EtsyShopManagementService : IEtsyShopManagementService
{
    /// <summary>
    /// The Http Client Name value.
    /// </summary>
    public const string HttpClientName = "EtsyApiSharp.Shops";

    private readonly string apiKey;
    private readonly IHttpClientFactory httpClientFactory;
    /// <summary>
    /// Initializes a new instance of the EtsyShopManagementService class.
    /// </summary>

    public EtsyShopManagementService(
        IHttpClientFactory httpClientFactory,
        string clientId,
        string sharedSecret)
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
    /// Executes the Get Shop By Owner User Id operation.
    /// </summary>

    public Task<ApiResponse<Shop>> GetShopByOwnerUserIdAsync(
        long userId,
        CancellationToken cancellationToken = default)
    {
        ValidateId(userId, nameof(userId));

        return SendAsync<Shop>(
            HttpMethod.Get,
            Url.ShopUrls.GetShopByOwnerUserId(userId),
            null,
            null,
            null,
            cancellationToken);
    }
    /// <summary>
    /// Executes the Get Shop operation.
    /// </summary>

    public Task<ApiResponse<Shop>> GetShopAsync(
        long shopId,
        CancellationToken cancellationToken = default)
    {
        ValidateId(shopId, nameof(shopId));

        return SendAsync<Shop>(
            HttpMethod.Get,
            Url.ShopUrls.GetShop(shopId),
            null,
            null,
            null,
            cancellationToken);
    }
    /// <summary>
    /// Executes the Update Shop operation.
    /// </summary>

    public Task<ApiResponse<Shop>> UpdateShopAsync(
        string accessToken,
        long shopId,
        UpdateShopRequest update,
        CancellationToken cancellationToken = default)
    {
        ValidateId(shopId, nameof(shopId));
        ValidateAccessToken(accessToken);
        ArgumentNullException.ThrowIfNull(update);

        if (update.Title is null &&
            update.Announcement is null &&
            update.SaleMessage is null &&
            update.DigitalSaleMessage is null &&
            update.PolicyAdditional is null)
        {
            throw new ArgumentException("At least one shop value must be supplied.", nameof(update));
        }

        var formData = new Dictionary<string, string>();
        AddString(formData, "title", update.Title);
        AddString(formData, "announcement", update.Announcement);
        AddString(formData, "sale_message", update.SaleMessage);
        AddString(formData, "digital_sale_message", update.DigitalSaleMessage);
        AddString(formData, "policy_additional", update.PolicyAdditional);

        return SendAsync<Shop>(
            HttpMethod.Put,
            Url.ShopUrls.UpdateShop(shopId),
            null,
            accessToken,
            new FormUrlEncodedContent(formData),
            cancellationToken);
    }
    /// <summary>
    /// Executes the Find Shops operation.
    /// </summary>

    public Task<ApiResponse<EtsyListResponse<Shop>>> FindShopsAsync(
        string shopName,
        FindShopsByNameFilter? filter = null,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(shopName))
            throw new ArgumentException("A shop name is required.", nameof(shopName));

        ValidatePagination(filter);

        var query = new Dictionary<string, string>
        {
            ["shop_name"] = shopName
        };

        if (filter is not null)
        {
            if (filter.Limit != 25)
                query["limit"] = filter.Limit.ToString(CultureInfo.InvariantCulture);

            if (filter.Offset != 0)
                query["offset"] = filter.Offset.ToString(CultureInfo.InvariantCulture);
        }

        return SendAsync<EtsyListResponse<Shop>>(
            HttpMethod.Get,
            Url.ShopUrls.FindShops(),
            query,
            null,
            null,
            cancellationToken);
    }
    /// <summary>
    /// Executes the Get Shop Production Partners operation.
    /// </summary>

    public Task<ApiResponse<EtsyListResponse<ShopProductionPartner>>> GetShopProductionPartnersAsync(
        string accessToken,
        long shopId,
        CancellationToken cancellationToken = default)
    {
        ValidateAccessToken(accessToken);
        ValidateId(shopId, nameof(shopId));

        return SendListAsync<ShopProductionPartner>(HttpMethod.Get, Url.ShopUrls.GetShopProductionPartners(shopId), null, accessToken, null, cancellationToken);
    }
    /// <summary>
    /// Executes the Create Shop Section operation.
    /// </summary>

    public Task<ApiResponse<ShopSection>> CreateShopSectionAsync(
        string accessToken,
        long shopId,
        CreateShopSectionRequest section,
        CancellationToken cancellationToken = default)
    {
        ValidateAccessToken(accessToken);
        ValidateId(shopId, nameof(shopId));
        ArgumentNullException.ThrowIfNull(section);
        ValidateSectionTitle(section.Title, nameof(section));

        return SendAsync<ShopSection>(HttpMethod.Post, Url.ShopUrls.GetShopSections(shopId), null, accessToken, CreateSectionContent(section.Title), cancellationToken);
    }
    /// <summary>
    /// Executes the Get Shop Sections operation.
    /// </summary>

    public Task<ApiResponse<EtsyListResponse<ShopSection>>> GetShopSectionsAsync(
        long shopId,
        CancellationToken cancellationToken = default)
    {
        ValidateId(shopId, nameof(shopId));
        return SendListAsync<ShopSection>(HttpMethod.Get, Url.ShopUrls.GetShopSections(shopId), null, null, null, cancellationToken);
    }
    /// <summary>
    /// Executes the Delete Shop Section operation.
    /// </summary>

    public Task<ApiResponse<object>> DeleteShopSectionAsync(
        string accessToken,
        long shopId,
        long shopSectionId,
        CancellationToken cancellationToken = default)
    {
        ValidateAccessToken(accessToken);
        ValidateId(shopId, nameof(shopId));
        ValidateId(shopSectionId, nameof(shopSectionId));
        return SendAsync<object>(HttpMethod.Delete, Url.ShopUrls.GetShopSection(shopId, shopSectionId), null, accessToken, null, cancellationToken);
    }
    /// <summary>
    /// Executes the Get Shop Section operation.
    /// </summary>

    public Task<ApiResponse<ShopSection>> GetShopSectionAsync(
        long shopId,
        long shopSectionId,
        CancellationToken cancellationToken = default)
    {
        ValidateId(shopId, nameof(shopId));
        ValidateId(shopSectionId, nameof(shopSectionId));
        return SendAsync<ShopSection>(HttpMethod.Get, Url.ShopUrls.GetShopSection(shopId, shopSectionId), null, null, null, cancellationToken);
    }
    /// <summary>
    /// Executes the Update Shop Section operation.
    /// </summary>

    public Task<ApiResponse<ShopSection>> UpdateShopSectionAsync(
        string accessToken,
        long shopId,
        long shopSectionId,
        UpdateShopSectionRequest section,
        CancellationToken cancellationToken = default)
    {
        ValidateAccessToken(accessToken);
        ValidateId(shopId, nameof(shopId));
        ValidateId(shopSectionId, nameof(shopSectionId));
        ArgumentNullException.ThrowIfNull(section);
        ValidateSectionTitle(section.Title, nameof(section));

        return SendAsync<ShopSection>(HttpMethod.Put, Url.ShopUrls.GetShopSection(shopId, shopSectionId), null, accessToken, CreateSectionContent(section.Title), cancellationToken);
    }

    private async Task<ApiResponse<T>> SendAsync<T>(
        HttpMethod method,
        string relativeUrl,
        IReadOnlyDictionary<string, string>? query,
        string? accessToken,
        HttpContent? content,
        CancellationToken cancellationToken)
    {
        using var request = new HttpRequestMessage(method, BuildUri(relativeUrl, query))
        {
            Content = content
        };

        if (accessToken is not null)
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

        request.Headers.Add("x-api-key", apiKey);

        var httpClient = httpClientFactory.CreateClient(HttpClientName);
        using var response = await httpClient.SendAsync(request, cancellationToken).ConfigureAwait(false);
        response.RequestMessage ??= request;
        return await EtsyResponseParser
            .ParseResponseOfSingle<T>(response, cancellationToken)
            .ConfigureAwait(false);
    }

    private async Task<ApiResponse<EtsyListResponse<T>>> SendListAsync<T>(
        HttpMethod method,
        string relativeUrl,
        IReadOnlyDictionary<string, string>? query,
        string? accessToken,
        HttpContent? content,
        CancellationToken cancellationToken)
    {
        using var request = new HttpRequestMessage(method, BuildUri(relativeUrl, query))
        {
            Content = content
        };

        if (accessToken is not null)
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

        request.Headers.Add("x-api-key", apiKey);

        var httpClient = httpClientFactory.CreateClient(HttpClientName);
        using var response = await httpClient.SendAsync(request, cancellationToken).ConfigureAwait(false);
        response.RequestMessage ??= request;
        return await EtsyResponseParser.ParseResponseOfList<T>(response, cancellationToken).ConfigureAwait(false);
    }

    private static Uri BuildUri(
        string relativeUrl,
        IReadOnlyDictionary<string, string>? query)
    {
        var uriBuilder = new UriBuilder($"{Url.BaseUrls.BaseApiUrl}{relativeUrl}");
        if (query is { Count: > 0 })
        {
            uriBuilder.Query = string.Join(
                "&",
                query.Select(parameter =>
                    $"{Uri.EscapeDataString(parameter.Key)}={Uri.EscapeDataString(parameter.Value)}"));
        }

        return uriBuilder.Uri;
    }

    private static void AddString(Dictionary<string, string> formData, string name, string? value)
    {
        if (value is not null)
            formData[name] = value;
    }

    private static FormUrlEncodedContent CreateSectionContent(string? title) => new(
        [new KeyValuePair<string, string>("title", title!)]);

    private static void ValidatePagination(EtsyFilterBase? filter)
    {
        if (filter is null)
            return;

        if (filter.Limit is < 1 or > 100)
            throw new ArgumentOutOfRangeException(nameof(filter), "The limit must be between 1 and 100.");

        if (filter.Offset < 0)
            throw new ArgumentOutOfRangeException(nameof(filter), "The offset cannot be negative.");
    }

    private static void ValidateAccessToken(string accessToken)
    {
        if (string.IsNullOrWhiteSpace(accessToken))
            throw new ArgumentException("An Etsy OAuth access token is required.", nameof(accessToken));
    }

    private static void ValidateSectionTitle(string? title, string parameterName)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("A shop section title is required.", parameterName);
    }

    private static void ValidateId(long id, string parameterName)
    {
        if (id < 1)
            throw new ArgumentOutOfRangeException(parameterName, "Etsy IDs must be greater than zero.");
    }
}
