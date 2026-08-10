using EtsyApiSharp.Helpers;
using EtsyApiSharp.Models;
using EtsyApiSharp.Models.Common;
using System.Net.Http.Headers;

namespace EtsyApiSharp.Services.UserManagements;

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

        return GetAsync<User>(accessToken, Url.UserUrls.GetUser(userId), cancellationToken);
    }

    public Task<ApiResponse<Self>> GetMeAsync(
        string accessToken,
        CancellationToken cancellationToken = default) =>
        GetAsync<Self>(accessToken, Url.UserUrls.GetMe(), cancellationToken);

    private async Task<ApiResponse<T>> GetAsync<T>(
        string accessToken,
        string relativeUrl,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(accessToken))
            throw new ArgumentException("An Etsy OAuth access token is required.", nameof(accessToken));

        using var request = new HttpRequestMessage(
            HttpMethod.Get,
            $"{Url.BaseUrls.BaseApiUrl}{relativeUrl}");
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        request.Headers.Add("x-api-key", apiKey);

        var httpClient = httpClientFactory.CreateClient(HttpClientName);
        using var response = await httpClient.SendAsync(request, cancellationToken).ConfigureAwait(false);
        response.RequestMessage ??= request;
        return await EtsyResponseParser
            .ParseResponseOfSingle<T>(response, cancellationToken)
            .ConfigureAwait(false);
    }
}
