using EtsyApiSharp.Helpers;
using EtsyApiSharp.Models;
using EtsyApiSharp.Models.Common;
using EtsyApiSharp.Models.Filters;
using System.Globalization;

namespace EtsyApiSharp.Services.ReviewManagements;

/// <inheritdoc />
public class EtsyReviewManagementService : IEtsyReviewManagementService
{
    /// <summary>
    /// The Http Client Name value.
    /// </summary>
    public const string HttpClientName = "EtsyApiSharp.Reviews";

    private const long MinimumEtsyTimestamp = 946684800;
    private readonly string apiKey;
    private readonly IHttpClientFactory httpClientFactory;
    /// <summary>
    /// Initializes a new instance of the EtsyReviewManagementService class.
    /// </summary>

    public EtsyReviewManagementService(IHttpClientFactory httpClientFactory, string clientId, string sharedSecret)
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
    /// Executes the Get Reviews By Listing operation.
    /// </summary>

    public Task<ApiResponse<EtsyListResponse<ListingReview>>> GetReviewsByListingAsync(
        long listingId,
        GetReviewsFilter? filter = null,
        CancellationToken cancellationToken = default)
    {
        ValidateId(listingId, nameof(listingId));
        return SendAsync<ListingReview>(Url.ReviewUrls.GetReviewsByListing(listingId), CreateQuery(filter), cancellationToken);
    }
    /// <summary>
    /// Executes the Get Reviews By Shop operation.
    /// </summary>

    public Task<ApiResponse<EtsyListResponse<TransactionReview>>> GetReviewsByShopAsync(
        long shopId,
        GetReviewsFilter? filter = null,
        CancellationToken cancellationToken = default)
    {
        ValidateId(shopId, nameof(shopId));
        return SendAsync<TransactionReview>(Url.ReviewUrls.GetReviewsByShop(shopId), CreateQuery(filter), cancellationToken);
    }

    private async Task<ApiResponse<EtsyListResponse<T>>> SendAsync<T>(
        string relativeUrl,
        IReadOnlyDictionary<string, string>? query,
        CancellationToken cancellationToken)
    {
        using var request = new HttpRequestMessage(HttpMethod.Get, BuildUri(relativeUrl, query));
        request.Headers.Add("x-api-key", apiKey);

        var httpClient = httpClientFactory.CreateClient(HttpClientName);
        using var response = await httpClient.SendAsync(request, cancellationToken).ConfigureAwait(false);
        response.RequestMessage ??= request;
        return await EtsyResponseParser.ParseResponseOfList<T>(response, cancellationToken).ConfigureAwait(false);
    }

    private static IReadOnlyDictionary<string, string>? CreateQuery(GetReviewsFilter? filter)
    {
        if (filter is null)
            return null;

        ValidateFilter(filter);
        var query = new Dictionary<string, string>();
        if (filter.Limit != 25)
            query["limit"] = filter.Limit.ToString(CultureInfo.InvariantCulture);
        if (filter.Offset != 0)
            query["offset"] = filter.Offset.ToString(CultureInfo.InvariantCulture);
        if (filter.MinCreated is { } minCreated)
            query["min_created"] = minCreated.ToString(CultureInfo.InvariantCulture);
        if (filter.MaxCreated is { } maxCreated)
            query["max_created"] = maxCreated.ToString(CultureInfo.InvariantCulture);
        return query;
    }

    private static Uri BuildUri(string relativeUrl, IReadOnlyDictionary<string, string>? query)
    {
        var uriBuilder = new UriBuilder($"{Url.BaseUrls.BaseApiUrl}{relativeUrl}");
        if (query is { Count: > 0 })
            uriBuilder.Query = string.Join("&", query.Select(parameter =>
                $"{Uri.EscapeDataString(parameter.Key)}={Uri.EscapeDataString(parameter.Value)}"));
        return uriBuilder.Uri;
    }

    private static void ValidateFilter(GetReviewsFilter filter)
    {
        if (filter.Limit is < 1 or > 100)
            throw new ArgumentOutOfRangeException(nameof(filter), "The limit must be between 1 and 100.");
        if (filter.Offset < 0)
            throw new ArgumentOutOfRangeException(nameof(filter), "The offset cannot be negative.");
        if (filter.MinCreated is { } minCreated && minCreated < MinimumEtsyTimestamp)
            throw new ArgumentOutOfRangeException(nameof(filter), "MinCreated must be on or after 2000-01-01 UTC.");
        if (filter.MaxCreated is { } maxCreated && maxCreated < MinimumEtsyTimestamp)
            throw new ArgumentOutOfRangeException(nameof(filter), "MaxCreated must be on or after 2000-01-01 UTC.");
        if (filter.MinCreated is { } minimum && filter.MaxCreated is { } maximum && minimum > maximum)
            throw new ArgumentException("MinCreated cannot be greater than MaxCreated.", nameof(filter));
    }

    private static void ValidateId(long id, string parameterName)
    {
        if (id < 1)
            throw new ArgumentOutOfRangeException(parameterName, "Etsy IDs must be greater than zero.");
    }
}
