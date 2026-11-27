using EtsyApiSharp.Helpers;
using EtsyApiSharp.Models;
using EtsyApiSharp.Models.Common;
using System.Net.Http.Headers;

namespace EtsyApiSharp.Services.ShopEconomicOperatorManagements;

/// <summary>
/// Implements Etsy Shop Economic Operator operations.
/// </summary>
public class EtsyShopEconomicOperatorManagementService : IEtsyShopEconomicOperatorManagementService
{
    /// <summary>The named HTTP client used by this service.</summary>
    public const string HttpClientName = "EtsyApiSharp.ShopEconomicOperators";

    private readonly string apiKey;
    private readonly IHttpClientFactory httpClientFactory;

    /// <summary>Initializes the service.</summary>
    public EtsyShopEconomicOperatorManagementService(IHttpClientFactory httpClientFactory, string clientId, string sharedSecret)
    {
        ArgumentNullException.ThrowIfNull(httpClientFactory);
        if (string.IsNullOrWhiteSpace(clientId))
            throw new ArgumentException("An Etsy API key keystring is required.", nameof(clientId));
        if (string.IsNullOrWhiteSpace(sharedSecret))
            throw new ArgumentException("An Etsy API shared secret is required.", nameof(sharedSecret));

        this.httpClientFactory = httpClientFactory;
        apiKey = $"{clientId}:{sharedSecret}";
    }

    /// <inheritdoc />
    public Task<ApiResponse<ShopEconomicOperator>> CreateShopEconomicOperatorAsync(string accessToken, long shopId, CreateShopEconomicOperatorRequest economicOperator, CancellationToken cancellationToken = default)
    {
        ValidateAccessToken(accessToken);
        ValidateId(shopId, nameof(shopId));
        ArgumentNullException.ThrowIfNull(economicOperator);
        ValidateCreate(economicOperator);
        return SendSingleAsync<ShopEconomicOperator>(HttpMethod.Post, Url.ShopEconomicOperatorUrls.GetShopEconomicOperators(shopId), accessToken, CreateFormContent(economicOperator), cancellationToken);
    }

    /// <inheritdoc />
    public Task<ApiResponse<EtsyListResponse<ShopEconomicOperator>>> GetShopEconomicOperatorsAsync(string accessToken, long shopId, CancellationToken cancellationToken = default)
    {
        ValidateAccessToken(accessToken);
        ValidateId(shopId, nameof(shopId));
        return SendListAsync<ShopEconomicOperator>(HttpMethod.Get, Url.ShopEconomicOperatorUrls.GetShopEconomicOperators(shopId), accessToken, null, cancellationToken);
    }

    /// <inheritdoc />
    public Task<ApiResponse<ShopEconomicOperator>> GetShopEconomicOperatorAsync(string accessToken, long shopId, long economicOperatorId, CancellationToken cancellationToken = default)
    {
        ValidateAccessToken(accessToken);
        ValidateId(shopId, nameof(shopId));
        ValidateId(economicOperatorId, nameof(economicOperatorId));
        return SendSingleAsync<ShopEconomicOperator>(HttpMethod.Get, Url.ShopEconomicOperatorUrls.GetShopEconomicOperator(shopId, economicOperatorId), accessToken, null, cancellationToken);
    }

    /// <inheritdoc />
    public Task<ApiResponse<ShopEconomicOperator>> UpdateShopEconomicOperatorAsync(string accessToken, long shopId, long economicOperatorId, UpdateShopEconomicOperatorRequest economicOperator, CancellationToken cancellationToken = default)
    {
        ValidateAccessToken(accessToken);
        ValidateId(shopId, nameof(shopId));
        ValidateId(economicOperatorId, nameof(economicOperatorId));
        ArgumentNullException.ThrowIfNull(economicOperator);
        if (economicOperator.GetType().GetProperties().All(property => property.GetValue(economicOperator) is null))
            throw new ArgumentException("At least one economic operator value must be supplied.", nameof(economicOperator));

        return SendSingleAsync<ShopEconomicOperator>(HttpMethod.Patch, Url.ShopEconomicOperatorUrls.GetShopEconomicOperator(shopId, economicOperatorId), accessToken, CreateFormContent(economicOperator), cancellationToken);
    }

    private async Task<ApiResponse<T>> SendSingleAsync<T>(HttpMethod method, string relativeUrl, string accessToken, HttpContent? content, CancellationToken cancellationToken)
    {
        using var request = CreateRequest(method, relativeUrl, accessToken, content);
        var httpClient = httpClientFactory.CreateClient(HttpClientName);
        using var response = await httpClient.SendAsync(request, cancellationToken).ConfigureAwait(false);
        response.RequestMessage ??= request;
        return await EtsyResponseParser.ParseResponseOfSingle<T>(response, cancellationToken).ConfigureAwait(false);
    }

    private async Task<ApiResponse<EtsyListResponse<T>>> SendListAsync<T>(HttpMethod method, string relativeUrl, string accessToken, HttpContent? content, CancellationToken cancellationToken)
    {
        using var request = CreateRequest(method, relativeUrl, accessToken, content);
        var httpClient = httpClientFactory.CreateClient(HttpClientName);
        using var response = await httpClient.SendAsync(request, cancellationToken).ConfigureAwait(false);
        response.RequestMessage ??= request;
        return await EtsyResponseParser.ParseResponseOfList<T>(response, cancellationToken).ConfigureAwait(false);
    }

    private HttpRequestMessage CreateRequest(HttpMethod method, string relativeUrl, string accessToken, HttpContent? content)
    {
        var request = new HttpRequestMessage(method, $"{Url.BaseUrls.BaseApiUrl}{relativeUrl}") { Content = content };
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        request.Headers.Add("x-api-key", apiKey);
        return request;
    }

    private static FormUrlEncodedContent CreateFormContent(object request) => new(request.GetType().GetProperties()
        .Select(property => new { property, Value = property.GetValue(request) })
        .Where(item => item.Value is not null)
        .Select(item => new KeyValuePair<string, string>(ToSnakeCase(item.property.Name), item.Value!.ToString()!)));

    private static string ToSnakeCase(string value) => string.Concat(value.Select((character, index) =>
        index > 0 && char.IsUpper(character) ? $"_{char.ToLowerInvariant(character)}" : char.ToLowerInvariant(character).ToString()));

    private static void ValidateCreate(CreateShopEconomicOperatorRequest economicOperator)
    {
        if (new[] { economicOperator.OperatorName, economicOperator.Email, economicOperator.FirstLine, economicOperator.City, economicOperator.Zip, economicOperator.CountryIso, economicOperator.Phone }.Any(string.IsNullOrWhiteSpace))
            throw new ArgumentException("Operator name, email, address, city, ZIP code, country code, and phone are required.", nameof(economicOperator));
    }

    private static void ValidateAccessToken(string accessToken)
    {
        if (string.IsNullOrWhiteSpace(accessToken))
            throw new ArgumentException("An Etsy OAuth access token is required.", nameof(accessToken));
    }

    private static void ValidateId(long id, string parameterName)
    {
        if (id < 1)
            throw new ArgumentOutOfRangeException(parameterName, "Etsy IDs must be greater than zero.");
    }
}
