using EtsyApiSharp.Helpers;
using EtsyApiSharp.Models;
using EtsyApiSharp.Models.Common;
using EtsyApiSharp.Models.Filters;
using System.Globalization;
using System.Net.Http.Headers;

namespace EtsyApiSharp.Services.PaymentManagements;

/// <inheritdoc />
public class EtsyPaymentManagementService : IEtsyPaymentManagementService
{
    public const string HttpClientName = "EtsyApiSharp.Payments";

    private const long MinimumEtsyTimestamp = 946684800;
    private readonly string apiKey;
    private readonly IHttpClientFactory httpClientFactory;

    public EtsyPaymentManagementService(IHttpClientFactory httpClientFactory, string clientId, string sharedSecret)
    {
        ArgumentNullException.ThrowIfNull(httpClientFactory);
        if (string.IsNullOrWhiteSpace(clientId))
            throw new ArgumentException("An Etsy API key keystring is required.", nameof(clientId));
        if (string.IsNullOrWhiteSpace(sharedSecret))
            throw new ArgumentException("An Etsy API shared secret is required.", nameof(sharedSecret));

        this.httpClientFactory = httpClientFactory;
        apiKey = $"{clientId}:{sharedSecret}";
    }

    public Task<ApiResponse<PaymentAccountLedgerEntry>> GetShopPaymentAccountLedgerEntryAsync(
        string accessToken, long shopId, long ledgerEntryId, CancellationToken cancellationToken = default)
    {
        ValidateId(shopId, nameof(shopId));
        ValidateId(ledgerEntryId, nameof(ledgerEntryId));
        return SendAsync<PaymentAccountLedgerEntry>(HttpMethod.Get,
            Url.PaymentUrls.GetShopPaymentAccountLedgerEntry(shopId, ledgerEntryId), null, accessToken, cancellationToken);
    }

    public Task<ApiResponse<EtsyListResponse<PaymentAccountLedgerEntry>>> GetShopPaymentAccountLedgerEntriesAsync(
        string accessToken, long shopId, GetShopPaymentAccountLedgerEntriesFilter filter, CancellationToken cancellationToken = default)
    {
        ValidateId(shopId, nameof(shopId));
        ValidateLedgerFilter(filter);
        var query = new Dictionary<string, string>
        {
            ["min_created"] = filter.MinCreated.ToString(CultureInfo.InvariantCulture),
            ["max_created"] = filter.MaxCreated.ToString(CultureInfo.InvariantCulture)
        };
        AddPagination(query, filter);
        return SendAsync<EtsyListResponse<PaymentAccountLedgerEntry>>(HttpMethod.Get,
            Url.PaymentUrls.GetShopPaymentAccountLedgerEntries(shopId), query, accessToken, cancellationToken);
    }

    public Task<ApiResponse<EtsyListResponse<Payment>>> GetPaymentAccountLedgerEntryPaymentsAsync(
        string accessToken, long shopId, IReadOnlyCollection<long> ledgerEntryIds, CancellationToken cancellationToken = default)
    {
        ValidateId(shopId, nameof(shopId));
        ValidateIds(ledgerEntryIds, nameof(ledgerEntryIds));
        return SendAsync<EtsyListResponse<Payment>>(HttpMethod.Get,
            Url.PaymentUrls.GetPaymentAccountLedgerEntryPayments(shopId), CreateIdsQuery("ledger_entry_ids", ledgerEntryIds), accessToken, cancellationToken);
    }

    public Task<ApiResponse<EtsyListResponse<Payment>>> GetShopPaymentByReceiptIdAsync(
        string accessToken, long shopId, long receiptId, CancellationToken cancellationToken = default)
    {
        ValidateId(shopId, nameof(shopId));
        ValidateId(receiptId, nameof(receiptId));
        return SendAsync<EtsyListResponse<Payment>>(HttpMethod.Get,
            Url.PaymentUrls.GetShopPaymentByReceiptId(shopId, receiptId), null, accessToken, cancellationToken);
    }

    public Task<ApiResponse<EtsyListResponse<Payment>>> GetPaymentsAsync(
        string accessToken, long shopId, IReadOnlyCollection<long> paymentIds, CancellationToken cancellationToken = default)
    {
        ValidateId(shopId, nameof(shopId));
        ValidateIds(paymentIds, nameof(paymentIds));
        return SendAsync<EtsyListResponse<Payment>>(HttpMethod.Get,
            Url.PaymentUrls.GetPayments(shopId), CreateIdsQuery("payment_ids", paymentIds), accessToken, cancellationToken);
    }

    private async Task<ApiResponse<T>> SendAsync<T>(HttpMethod method, string relativeUrl,
        IReadOnlyDictionary<string, string>? query, string accessToken, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(accessToken))
            throw new ArgumentException("An Etsy OAuth access token is required.", nameof(accessToken));

        using var request = new HttpRequestMessage(method, BuildUri(relativeUrl, query));
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        request.Headers.Add("x-api-key", apiKey);

        var httpClient = httpClientFactory.CreateClient(HttpClientName);
        using var response = await httpClient.SendAsync(request, cancellationToken).ConfigureAwait(false);
        response.RequestMessage ??= request;
        return await EtsyResponseParser.ParseResponseOfSingle<T>(response, cancellationToken).ConfigureAwait(false);
    }

    private static Uri BuildUri(string relativeUrl, IReadOnlyDictionary<string, string>? query)
    {
        var uriBuilder = new UriBuilder($"{Url.BaseUrls.BaseApiUrl}{relativeUrl}");
        if (query is { Count: > 0 })
            uriBuilder.Query = string.Join("&", query.Select(parameter =>
                $"{Uri.EscapeDataString(parameter.Key)}={Uri.EscapeDataString(parameter.Value)}"));
        return uriBuilder.Uri;
    }

    private static Dictionary<string, string> CreateIdsQuery(string name, IReadOnlyCollection<long> ids) => new()
    {
        [name] = string.Join(',', ids)
    };

    private static void AddPagination(Dictionary<string, string> query, EtsyFilterBase filter)
    {
        if (filter.Limit != 25)
            query["limit"] = filter.Limit.ToString(CultureInfo.InvariantCulture);
        if (filter.Offset != 0)
            query["offset"] = filter.Offset.ToString(CultureInfo.InvariantCulture);
    }

    private static void ValidateLedgerFilter(GetShopPaymentAccountLedgerEntriesFilter? filter)
    {
        ArgumentNullException.ThrowIfNull(filter);
        if (filter.MinCreated < MinimumEtsyTimestamp || filter.MaxCreated < MinimumEtsyTimestamp)
            throw new ArgumentOutOfRangeException(nameof(filter), "Etsy timestamps must be on or after 2000-01-01 UTC.");
        if (filter.MinCreated > filter.MaxCreated)
            throw new ArgumentException("MinCreated cannot be greater than MaxCreated.", nameof(filter));
        if (filter.Limit is < 1 or > 100)
            throw new ArgumentOutOfRangeException(nameof(filter), "The limit must be between 1 and 100.");
        if (filter.Offset < 0)
            throw new ArgumentOutOfRangeException(nameof(filter), "The offset cannot be negative.");
    }

    private static void ValidateId(long id, string parameterName)
    {
        if (id < 1)
            throw new ArgumentOutOfRangeException(parameterName, "Etsy IDs must be greater than zero.");
    }

    private static void ValidateIds(IReadOnlyCollection<long>? ids, string parameterName)
    {
        ArgumentNullException.ThrowIfNull(ids);
        if (ids.Count == 0 || ids.Any(id => id < 1))
            throw new ArgumentException("At least one positive Etsy ID is required.", parameterName);
    }
}
