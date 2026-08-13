using EtsyApiSharp.Helpers;
using EtsyApiSharp.Models;
using EtsyApiSharp.Models.Common;
using EtsyApiSharp.Models.ShopPolicies;
using System.Globalization;
using System.Net.Http.Headers;

namespace EtsyApiSharp.Services.ShopPolicyManagements;

public sealed class EtsyShopPolicyManagementService : IEtsyShopPolicyManagementService
{
    public const string HttpClientName = "EtsyApiSharp.ShopPolicies";

    private static readonly HashSet<long> ValidReturnDeadlines = [7, 14, 21, 30, 45, 60, 90];
    private readonly string apiKey;
    private readonly IHttpClientFactory httpClientFactory;

    public EtsyShopPolicyManagementService(IHttpClientFactory httpClientFactory, string clientId, string sharedSecret)
    {
        ArgumentNullException.ThrowIfNull(httpClientFactory);
        if (string.IsNullOrWhiteSpace(clientId))
            throw new ArgumentException("An Etsy API key keystring is required.", nameof(clientId));
        if (string.IsNullOrWhiteSpace(sharedSecret))
            throw new ArgumentException("An Etsy API shared secret is required.", nameof(sharedSecret));

        this.httpClientFactory = httpClientFactory;
        apiKey = $"{clientId}:{sharedSecret}";
    }

    public Task<ApiResponse<ShopReturnPolicy>> ConsolidateShopReturnPoliciesAsync(string accessToken, long shopId, ConsolidateShopReturnPoliciesRequest request, CancellationToken cancellationToken = default)
    {
        ValidateAccessToken(accessToken);
        ValidateId(shopId, nameof(shopId));
        ArgumentNullException.ThrowIfNull(request);
        ValidateId(request.SourceReturnPolicyId, nameof(request.SourceReturnPolicyId));
        ValidateId(request.DestinationReturnPolicyId, nameof(request.DestinationReturnPolicyId));
        return SendAsync<ShopReturnPolicy>(HttpMethod.Post, Url.ShopPolicyUrls.ConsolidateShopReturnPolicies(shopId), accessToken, CreateConsolidateContent(request), cancellationToken);
    }

    public Task<ApiResponse<ShopReturnPolicy>> CreateShopReturnPolicyAsync(string accessToken, long shopId, CreateShopReturnPolicyRequest policy, CancellationToken cancellationToken = default)
    {
        ValidateAccessToken(accessToken);
        ValidateId(shopId, nameof(shopId));
        ArgumentNullException.ThrowIfNull(policy);
        ValidateReturnDeadline(policy.ReturnDeadline, nameof(policy));
        return SendAsync<ShopReturnPolicy>(HttpMethod.Post, Url.ShopPolicyUrls.GetShopReturnPolicies(shopId), accessToken, CreateContent(policy.AcceptsReturns, policy.AcceptsExchanges, policy.ReturnDeadline), cancellationToken);
    }

    public Task<ApiResponse<EtsyListResponse<ShopReturnPolicy>>> GetShopReturnPoliciesAsync(long shopId, CancellationToken cancellationToken = default)
    {
        ValidateId(shopId, nameof(shopId));
        return SendListAsync<ShopReturnPolicy>(HttpMethod.Get, Url.ShopPolicyUrls.GetShopReturnPolicies(shopId), null, cancellationToken);
    }

    public Task<ApiResponse<object>> DeleteShopReturnPolicyAsync(string accessToken, long shopId, long returnPolicyId, CancellationToken cancellationToken = default)
    {
        ValidateWriteIds(accessToken, shopId, returnPolicyId);
        return SendAsync<object>(HttpMethod.Delete, Url.ShopPolicyUrls.GetShopReturnPolicy(shopId, returnPolicyId), accessToken, null, cancellationToken);
    }

    public Task<ApiResponse<ShopReturnPolicy>> GetShopReturnPolicyAsync(long shopId, long returnPolicyId, CancellationToken cancellationToken = default)
    {
        ValidateId(shopId, nameof(shopId));
        ValidateId(returnPolicyId, nameof(returnPolicyId));
        return SendAsync<ShopReturnPolicy>(HttpMethod.Get, Url.ShopPolicyUrls.GetShopReturnPolicy(shopId, returnPolicyId), null, null, cancellationToken);
    }

    public Task<ApiResponse<ShopReturnPolicy>> UpdateShopReturnPolicyAsync(string accessToken, long shopId, long returnPolicyId, UpdateShopReturnPolicyRequest policy, CancellationToken cancellationToken = default)
    {
        ValidateWriteIds(accessToken, shopId, returnPolicyId);
        ArgumentNullException.ThrowIfNull(policy);
        ValidateReturnDeadline(policy.ReturnDeadline, nameof(policy));
        return SendAsync<ShopReturnPolicy>(HttpMethod.Put, Url.ShopPolicyUrls.GetShopReturnPolicy(shopId, returnPolicyId), accessToken, CreateContent(policy.AcceptsReturns, policy.AcceptsExchanges, policy.ReturnDeadline), cancellationToken);
    }

    private async Task<ApiResponse<T>> SendAsync<T>(HttpMethod method, string relativeUrl, string? accessToken, HttpContent? content, CancellationToken cancellationToken)
    {
        using var request = CreateRequest(method, relativeUrl, accessToken, content);
        using var response = await httpClientFactory.CreateClient(HttpClientName).SendAsync(request, cancellationToken).ConfigureAwait(false);
        response.RequestMessage ??= request;
        return await EtsyResponseParser.ParseResponseOfSingle<T>(response, cancellationToken).ConfigureAwait(false);
    }

    private async Task<ApiResponse<EtsyListResponse<T>>> SendListAsync<T>(HttpMethod method, string relativeUrl, string? accessToken, CancellationToken cancellationToken)
    {
        using var request = CreateRequest(method, relativeUrl, accessToken, null);
        using var response = await httpClientFactory.CreateClient(HttpClientName).SendAsync(request, cancellationToken).ConfigureAwait(false);
        response.RequestMessage ??= request;
        return await EtsyResponseParser.ParseResponseOfList<T>(response, cancellationToken).ConfigureAwait(false);
    }

    private HttpRequestMessage CreateRequest(HttpMethod method, string relativeUrl, string? accessToken, HttpContent? content)
    {
        var request = new HttpRequestMessage(method, $"{Url.BaseUrls.BaseApiUrl}{relativeUrl}") { Content = content };
        if (accessToken is not null)
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        request.Headers.Add("x-api-key", apiKey);
        return request;
    }

    private static FormUrlEncodedContent CreateConsolidateContent(ConsolidateShopReturnPoliciesRequest request) => new(
        [new("source_return_policy_id", request.SourceReturnPolicyId.ToString(CultureInfo.InvariantCulture)), new("destination_return_policy_id", request.DestinationReturnPolicyId.ToString(CultureInfo.InvariantCulture))]);

    private static FormUrlEncodedContent CreateContent(bool acceptsReturns, bool acceptsExchanges, long? returnDeadline)
    {
        var form = new Dictionary<string, string>
        {
            ["accepts_returns"] = acceptsReturns.ToString().ToLowerInvariant(),
            ["accepts_exchanges"] = acceptsExchanges.ToString().ToLowerInvariant()
        };
        if (returnDeadline.HasValue)
            form["return_deadline"] = returnDeadline.Value.ToString(CultureInfo.InvariantCulture);
        return new FormUrlEncodedContent(form);
    }

    private static void ValidateWriteIds(string accessToken, long shopId, long returnPolicyId)
    {
        ValidateAccessToken(accessToken);
        ValidateId(shopId, nameof(shopId));
        ValidateId(returnPolicyId, nameof(returnPolicyId));
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

    private static void ValidateReturnDeadline(long? returnDeadline, string parameterName)
    {
        if (returnDeadline.HasValue && !ValidReturnDeadlines.Contains(returnDeadline.Value))
            throw new ArgumentOutOfRangeException(parameterName, "The return deadline must be 7, 14, 21, 30, 45, 60, or 90 days.");
    }
}
