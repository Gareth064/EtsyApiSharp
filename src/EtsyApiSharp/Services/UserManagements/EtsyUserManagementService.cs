using EtsyApiSharp.Helpers;
using EtsyApiSharp.Models;
using EtsyApiSharp.Models.Common;
using EtsyApiSharp.Models.Filters;
using System.Globalization;
using System.Net.Http.Headers;

namespace EtsyApiSharp.Services.UserManagements;

/// <inheritdoc />
public class EtsyUserManagementService : IEtsyUserManagementService
{
    public const string HttpClientName = "EtsyApiSharp.Users";

    private readonly string apiKey;
    private readonly IHttpClientFactory httpClientFactory;

    public EtsyUserManagementService(
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

    public Task<ApiResponse<User>> GetUserAsync(
        string accessToken,
        long userId,
        CancellationToken cancellationToken = default)
    {
        if (userId < 1)
            throw new ArgumentOutOfRangeException(nameof(userId), "The Etsy user ID must be greater than zero.");

        ValidateAccessToken(accessToken);
        return GetAsync<User>(accessToken, Url.UserUrls.GetUser(userId), cancellationToken);
    }

    public Task<ApiResponse<Self>> GetMeAsync(
        string accessToken,
        CancellationToken cancellationToken = default)
    {
        ValidateAccessToken(accessToken);
        return GetAsync<Self>(accessToken, Url.UserUrls.GetMe(), cancellationToken);
    }

    public Task<ApiResponse<EtsyListResponse<UserAddress>>> GetUserAddressesAsync(
        string accessToken,
        GetUserAddressesFilter? filter = null,
        CancellationToken cancellationToken = default)
    {
        ValidateAccessToken(accessToken);
        ValidatePagination(filter);
        return GetListAsync<UserAddress>(accessToken, Url.UserUrls.GetUserAddresses(), CreatePaginationQuery(filter), cancellationToken);
    }

    public Task<ApiResponse<UserAddress>> GetUserAddressAsync(
        string accessToken,
        long userAddressId,
        CancellationToken cancellationToken = default)
    {
        ValidateAccessToken(accessToken);
        ValidateId(userAddressId, nameof(userAddressId));
        return GetAsync<UserAddress>(accessToken, Url.UserUrls.GetUserAddress(userAddressId), cancellationToken);
    }

    public Task<ApiResponse<object>> DeleteUserAddressAsync(
        string accessToken,
        long userAddressId,
        CancellationToken cancellationToken = default)
    {
        ValidateAccessToken(accessToken);
        ValidateId(userAddressId, nameof(userAddressId));
        return SendAsync<object>(HttpMethod.Delete, accessToken, Url.UserUrls.GetUserAddress(userAddressId), null, cancellationToken);
    }

    private Task<ApiResponse<T>> GetAsync<T>(
        string accessToken,
        string relativeUrl,
        CancellationToken cancellationToken) =>
        SendAsync<T>(HttpMethod.Get, accessToken, relativeUrl, null, cancellationToken);

    private async Task<ApiResponse<T>> SendAsync<T>(
        HttpMethod method,
        string accessToken,
        string relativeUrl,
        IReadOnlyDictionary<string, string>? query,
        CancellationToken cancellationToken)
    {
        using var request = new HttpRequestMessage(
            method,
            BuildUri(relativeUrl, query));
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        request.Headers.Add("x-api-key", apiKey);

        var httpClient = httpClientFactory.CreateClient(HttpClientName);
        using var response = await httpClient.SendAsync(request, cancellationToken).ConfigureAwait(false);
        response.RequestMessage ??= request;
        return await EtsyResponseParser
            .ParseResponseOfSingle<T>(response, cancellationToken)
            .ConfigureAwait(false);
    }

    private async Task<ApiResponse<EtsyListResponse<T>>> GetListAsync<T>(
        string accessToken,
        string relativeUrl,
        IReadOnlyDictionary<string, string>? query,
        CancellationToken cancellationToken)
    {
        using var request = new HttpRequestMessage(HttpMethod.Get, BuildUri(relativeUrl, query));
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        request.Headers.Add("x-api-key", apiKey);

        var httpClient = httpClientFactory.CreateClient(HttpClientName);
        using var response = await httpClient.SendAsync(request, cancellationToken).ConfigureAwait(false);
        response.RequestMessage ??= request;
        return await EtsyResponseParser
            .ParseResponseOfList<T>(response, cancellationToken)
            .ConfigureAwait(false);
    }

    private static IReadOnlyDictionary<string, string>? CreatePaginationQuery(GetUserAddressesFilter? filter)
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

    private static Uri BuildUri(string relativeUrl, IReadOnlyDictionary<string, string>? query)
    {
        var uriBuilder = new UriBuilder($"{Url.BaseUrls.BaseApiUrl}{relativeUrl}");
        if (query is { Count: > 0 })
            uriBuilder.Query = string.Join("&", query.Select(parameter =>
                $"{Uri.EscapeDataString(parameter.Key)}={Uri.EscapeDataString(parameter.Value)}"));
        return uriBuilder.Uri;
    }

    private static void ValidatePagination(GetUserAddressesFilter? filter)
    {
        if (filter is null)
            return;

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

    private static void ValidateAccessToken(string accessToken)
    {
        if (string.IsNullOrWhiteSpace(accessToken))
            throw new ArgumentException("An Etsy OAuth access token is required.", nameof(accessToken));
    }
}
