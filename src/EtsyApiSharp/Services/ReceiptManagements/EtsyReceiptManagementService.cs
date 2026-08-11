using EtsyApiSharp.Helpers;
using EtsyApiSharp.Models;
using EtsyApiSharp.Models.Common;
using EtsyApiSharp.Models.Filters;
using EtsyApiSharp.Models.ShopReceipts.Enums;
using System.Globalization;
using System.Net.Http.Headers;

namespace EtsyApiSharp.Services.ReceiptManagements;

public class EtsyReceiptManagementService : IEtsyReceiptManagementService
{
    public const string HttpClientName = "EtsyApiSharp.Receipts";

    private const long MinimumEtsyTimestamp = 946684800;
    private readonly string apiKey;
    private readonly IHttpClientFactory httpClientFactory;

    public EtsyReceiptManagementService(
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

    public Task<ApiResponse<ShopReceipt>> GetShopReceiptAsync(
        string accessToken,
        long shopId,
        long receiptId,
        bool? legacy = null,
        CancellationToken cancellationToken = default)
    {
        ValidateId(shopId, nameof(shopId));
        ValidateId(receiptId, nameof(receiptId));

        return SendAsync<ShopReceipt>(
            HttpMethod.Get,
            Url.ReceiptUrls.GetShopReceipt(shopId, receiptId),
            BuildLegacyQuery(legacy),
            accessToken,
            null,
            cancellationToken);
    }

    public Task<ApiResponse<EtsyListResponse<ShopReceipt>>> GetShopReceiptsAsync(
        string accessToken,
        long shopId,
        GetShopReceiptsFilter? filter = null,
        CancellationToken cancellationToken = default)
    {
        ValidateId(shopId, nameof(shopId));
        ValidateReceiptFilter(filter);

        return SendAsync<EtsyListResponse<ShopReceipt>>(
            HttpMethod.Get,
            Url.ReceiptUrls.GetShopReceipts(shopId),
            BuildReceiptQuery(filter),
            accessToken,
            null,
            cancellationToken);
    }

    public Task<ApiResponse<ShopReceipt>> UpdateShopReceiptAsync(
        string accessToken,
        long shopId,
        long receiptId,
        UpdateShopReceiptRequest update,
        bool? legacy = null,
        CancellationToken cancellationToken = default)
    {
        ValidateId(shopId, nameof(shopId));
        ValidateId(receiptId, nameof(receiptId));
        ArgumentNullException.ThrowIfNull(update);

        if (update.WasPaid is null && update.WasShipped is null)
            throw new ArgumentException("At least one receipt status value must be supplied.", nameof(update));

        var formData = new Dictionary<string, string>();
        AddBoolean(formData, "was_paid", update.WasPaid);
        AddBoolean(formData, "was_shipped", update.WasShipped);

        return SendAsync<ShopReceipt>(
            HttpMethod.Put,
            Url.ReceiptUrls.GetShopReceipt(shopId, receiptId),
            BuildLegacyQuery(legacy),
            accessToken,
            new FormUrlEncodedContent(formData),
            cancellationToken);
    }

    public Task<ApiResponse<ShopReceipt>> CreateReceiptShipmentAsync(
        string accessToken,
        long shopId,
        long receiptId,
        CreateReceiptShipmentRequest shipment,
        bool? legacy = null,
        CancellationToken cancellationToken = default)
    {
        ValidateId(shopId, nameof(shopId));
        ValidateId(receiptId, nameof(receiptId));
        ArgumentNullException.ThrowIfNull(shipment);

        return SendAsync<ShopReceipt>(
            HttpMethod.Post,
            Url.ReceiptUrls.CreateReceiptShipment(shopId, receiptId),
            BuildLegacyQuery(legacy),
            accessToken,
            HttpContentHelper.CreateJsonContent(shipment, ignoreNullValues: true),
            cancellationToken);
    }

    public Task<ApiResponse<ShopReceiptTransaction>> GetShopReceiptTransactionAsync(
        string accessToken,
        long shopId,
        long transactionId,
        CancellationToken cancellationToken = default)
    {
        ValidateId(shopId, nameof(shopId));
        ValidateId(transactionId, nameof(transactionId));

        return SendAsync<ShopReceiptTransaction>(
            HttpMethod.Get,
            Url.ReceiptUrls.GetShopReceiptTransaction(shopId, transactionId),
            null,
            accessToken,
            null,
            cancellationToken);
    }

    public Task<ApiResponse<EtsyListResponse<ShopReceiptTransaction>>> GetShopReceiptTransactionsByListingAsync(
        string accessToken,
        long shopId,
        long listingId,
        GetShopReceiptTransactionsByListingFilter? filter = null,
        CancellationToken cancellationToken = default)
    {
        ValidateId(shopId, nameof(shopId));
        ValidateId(listingId, nameof(listingId));
        ValidatePagination(filter);

        return SendAsync<EtsyListResponse<ShopReceiptTransaction>>(
            HttpMethod.Get,
            Url.ReceiptUrls.GetShopReceiptTransactionsByListing(shopId, listingId),
            BuildPaginationQuery(filter),
            accessToken,
            null,
            cancellationToken);
    }

    public Task<ApiResponse<EtsyListResponse<ShopReceiptTransaction>>> GetShopReceiptTransactionsByReceiptAsync(
        string accessToken,
        long shopId,
        long receiptId,
        bool? legacy = null,
        CancellationToken cancellationToken = default)
    {
        ValidateId(shopId, nameof(shopId));
        ValidateId(receiptId, nameof(receiptId));

        return SendAsync<EtsyListResponse<ShopReceiptTransaction>>(
            HttpMethod.Get,
            Url.ReceiptUrls.GetShopReceiptTransactionsByReceipt(shopId, receiptId),
            BuildLegacyQuery(legacy),
            accessToken,
            null,
            cancellationToken);
    }

    public Task<ApiResponse<EtsyListResponse<ShopReceiptTransaction>>> GetShopReceiptTransactionsByShopAsync(
        string accessToken,
        long shopId,
        GetShopReceiptTransactionsByShopFilter? filter = null,
        CancellationToken cancellationToken = default)
    {
        ValidateId(shopId, nameof(shopId));
        ValidatePagination(filter);

        return SendAsync<EtsyListResponse<ShopReceiptTransaction>>(
            HttpMethod.Get,
            Url.ReceiptUrls.GetShopReceiptTransactionsByShop(shopId),
            BuildPaginationQuery(filter),
            accessToken,
            null,
            cancellationToken);
    }

    private async Task<ApiResponse<T>> SendAsync<T>(
        HttpMethod method,
        string relativeUrl,
        IReadOnlyDictionary<string, string>? query,
        string accessToken,
        HttpContent? content,
        CancellationToken cancellationToken)
    {
        using var request = new HttpRequestMessage(method, BuildUri(relativeUrl, query))
        {
            Content = content
        };

        if (string.IsNullOrWhiteSpace(accessToken))
            throw new ArgumentException("An Etsy OAuth access token is required.", nameof(accessToken));

        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        request.Headers.Add("x-api-key", apiKey);

        var httpClient = httpClientFactory.CreateClient(HttpClientName);
        using var response = await httpClient.SendAsync(request, cancellationToken).ConfigureAwait(false);
        response.RequestMessage ??= request;
        return await EtsyResponseParser
            .ParseResponseOfSingle<T>(response, cancellationToken)
            .ConfigureAwait(false);
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

    private static IReadOnlyDictionary<string, string>? BuildReceiptQuery(GetShopReceiptsFilter? filter)
    {
        if (filter is null)
            return null;

        var query = new Dictionary<string, string>();
        AddPagination(query, filter);
        AddInt64(query, "min_created", filter.MinCreated);
        AddInt64(query, "max_created", filter.MaxCreated);
        AddInt64(query, "min_last_modified", filter.MinLastModified);
        AddInt64(query, "max_last_modified", filter.MaxLastModified);

        if (filter.SortOn != ReceiptSortOn.created)
            query["sort_on"] = filter.SortOn.ToString();

        if (filter.SortOrder != ReceiptSortOrder.desc)
            query["sort_order"] = filter.SortOrder.ToString();

        AddBoolean(query, "was_paid", filter.WasPaid);
        AddBoolean(query, "was_shipped", filter.WasShipped);
        AddBoolean(query, "was_delivered", filter.WasDelivered);
        AddBoolean(query, "was_canceled", filter.WasCancelled);
        AddBoolean(query, "legacy", filter.Legacy);
        return query;
    }

    private static IReadOnlyDictionary<string, string>? BuildPaginationQuery(EtsyFilterBase? filter)
    {
        if (filter is null)
            return null;

        var query = new Dictionary<string, string>();
        AddPagination(query, filter);

        var legacy = filter switch
        {
            GetShopReceiptTransactionsByListingFilter listingFilter => listingFilter.Legacy,
            GetShopReceiptTransactionsByShopFilter shopFilter => shopFilter.Legacy,
            _ => null
        };
        AddBoolean(query, "legacy", legacy);
        return query;
    }

    private static IReadOnlyDictionary<string, string>? BuildLegacyQuery(bool? legacy)
    {
        if (legacy is null)
            return null;

        return new Dictionary<string, string>
        {
            ["legacy"] = FormatBoolean(legacy.Value)
        };
    }

    private static void AddPagination(Dictionary<string, string> query, EtsyFilterBase filter)
    {
        if (filter.Limit != 25)
            query["limit"] = filter.Limit.ToString(CultureInfo.InvariantCulture);

        if (filter.Offset != 0)
            query["offset"] = filter.Offset.ToString(CultureInfo.InvariantCulture);
    }

    private static void AddInt64(Dictionary<string, string> query, string name, long? value)
    {
        if (value.HasValue)
            query[name] = value.Value.ToString(CultureInfo.InvariantCulture);
    }

    private static void AddBoolean(Dictionary<string, string> query, string name, bool? value)
    {
        if (value.HasValue)
            query[name] = FormatBoolean(value.Value);
    }

    private static string FormatBoolean(bool value) => value ? "true" : "false";

    private static void ValidateReceiptFilter(GetShopReceiptsFilter? filter)
    {
        ValidatePagination(filter);
        if (filter is null)
            return;

        ValidateTimestamp(filter.MinCreated, nameof(filter.MinCreated));
        ValidateTimestamp(filter.MaxCreated, nameof(filter.MaxCreated));
        ValidateTimestamp(filter.MinLastModified, nameof(filter.MinLastModified));
        ValidateTimestamp(filter.MaxLastModified, nameof(filter.MaxLastModified));

        if (filter.MinCreated > filter.MaxCreated)
            throw new ArgumentException("MinCreated cannot be greater than MaxCreated.", nameof(filter));

        if (filter.MinLastModified > filter.MaxLastModified)
            throw new ArgumentException("MinLastModified cannot be greater than MaxLastModified.", nameof(filter));

        if (!Enum.IsDefined(typeof(ReceiptSortOn), filter.SortOn))
            throw new ArgumentException("The receipt sort field is invalid.", nameof(filter));

        if (!Enum.IsDefined(typeof(ReceiptSortOrder), filter.SortOrder))
            throw new ArgumentException("The receipt sort order is invalid.", nameof(filter));
    }

    private static void ValidatePagination(EtsyFilterBase? filter)
    {
        if (filter is null)
            return;

        if (filter.Limit is < 1 or > 100)
            throw new ArgumentOutOfRangeException(nameof(filter), "The limit must be between 1 and 100.");

        if (filter.Offset < 0)
            throw new ArgumentOutOfRangeException(nameof(filter), "The offset cannot be negative.");
    }

    private static void ValidateTimestamp(long? timestamp, string parameterName)
    {
        if (timestamp.HasValue && timestamp.Value < MinimumEtsyTimestamp)
            throw new ArgumentOutOfRangeException(parameterName, "Etsy timestamps must be on or after 2000-01-01 UTC.");
    }

    private static void ValidateId(long id, string parameterName)
    {
        if (id < 1)
            throw new ArgumentOutOfRangeException(parameterName, "Etsy IDs must be greater than zero.");
    }
}
