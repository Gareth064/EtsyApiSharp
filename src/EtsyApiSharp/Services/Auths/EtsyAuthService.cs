using EtsyApiSharp.Helpers;
using EtsyApiSharp.Models;
using System.Net;
using System.Text.Json;

namespace EtsyApiSharp.Services.Auths;

public class EtsyAuthService : IEtsyAuthService
{
    public const string HttpClientName = "EtsyApiSharp.Auth";

    private readonly string clientId;
    private readonly string redirectUrl;
    private readonly IReadOnlyCollection<Scope> scopes;
    private readonly IHttpClientFactory httpClientFactory;

    public EtsyAuthService(
        IHttpClientFactory httpClientFactory,
        string clientId,
        string redirectUrl,
        IEnumerable<Scope> scopes)
    {
        ArgumentNullException.ThrowIfNull(httpClientFactory);

        if (string.IsNullOrWhiteSpace(clientId))
            throw new ArgumentException("An Etsy API key keystring is required.", nameof(clientId));

        if (!redirectUrl.StartsWith("https://", StringComparison.Ordinal) ||
            !Uri.TryCreate(redirectUrl, UriKind.Absolute, out _))
        {
            throw new ArgumentException("The redirect URL must be an absolute HTTPS URL.", nameof(redirectUrl));
        }

        ArgumentNullException.ThrowIfNull(scopes);

        var requestedScopes = scopes.Distinct().ToArray();
        if (requestedScopes.Length == 0)
            throw new ArgumentException("At least one Etsy permission scope is required.", nameof(scopes));

        if (requestedScopes.Any(scope => !Enum.IsDefined(typeof(Scope), scope)))
            throw new ArgumentException("All requested Etsy permission scopes must be valid.", nameof(scopes));

        this.httpClientFactory = httpClientFactory;
        this.clientId = clientId;
        this.redirectUrl = redirectUrl;
        this.scopes = requestedScopes;
    }

    public string BuildAuthorizationUrl(string codeVerifier, string state)
    {
        ValidateCodeVerifier(codeVerifier);

        if (string.IsNullOrWhiteSpace(state))
            throw new ArgumentException("A non-empty, single-use state value is required.", nameof(state));

        var parameters = new Dictionary<string, string>
        {
            ["response_type"] = "code",
            ["client_id"] = clientId,
            ["redirect_uri"] = redirectUrl,
            ["scope"] = string.Join(" ", scopes),
            ["state"] = state,
            ["code_challenge"] = AuthHelper.CreateCodeChallenge(codeVerifier),
            ["code_challenge_method"] = "S256"
        };

        var query = string.Join(
            "&",
            parameters.Select(parameter =>
                $"{Uri.EscapeDataString(parameter.Key)}={Uri.EscapeDataString(parameter.Value)}"));

        return $"{Url.BaseUrls.BaseAuthUrl}?{query}";
    }

    public Task<EtsyTokenResponse> GetFirstAccessTokenAsync(
        string authCode,
        string codeVerifier,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(authCode))
            throw new ArgumentException("The Etsy authorization code is required.", nameof(authCode));

        ValidateCodeVerifier(codeVerifier);

        return RequestTokenAsync(
            new Dictionary<string, string>
            {
                ["grant_type"] = "authorization_code",
                ["client_id"] = clientId,
                ["redirect_uri"] = redirectUrl,
                ["code"] = authCode,
                ["code_verifier"] = codeVerifier
            },
            cancellationToken);
    }

    public Task<EtsyTokenResponse> GetRefreshAccessTokenAsync(
        string refreshToken,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(refreshToken))
            throw new ArgumentException("The Etsy refresh token is required.", nameof(refreshToken));

        return RequestTokenAsync(
            new Dictionary<string, string>
            {
                ["grant_type"] = "refresh_token",
                ["client_id"] = clientId,
                ["refresh_token"] = refreshToken
            },
            cancellationToken);
    }

    private async Task<EtsyTokenResponse> RequestTokenAsync(
        IReadOnlyDictionary<string, string> formData,
        CancellationToken cancellationToken)
    {
        using var request = new HttpRequestMessage(HttpMethod.Post, Url.BaseUrls.BaseTokenUrl)
        {
            Content = new FormUrlEncodedContent(formData)
        };

        var httpClient = httpClientFactory.CreateClient(HttpClientName);
        using var response = await httpClient.SendAsync(request, cancellationToken).ConfigureAwait(false);
        var responseBody = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);

        if (!response.IsSuccessStatusCode)
            throw CreateTokenRequestException(response.StatusCode, response.ReasonPhrase, responseBody);

        var token = JsonSerializer.Deserialize<EtsyTokenResponse>(responseBody);
        if (token is null ||
            string.IsNullOrWhiteSpace(token.AccessToken) ||
            string.IsNullOrWhiteSpace(token.TokenType) ||
            token.ExpiresIn <= 0 ||
            string.IsNullOrWhiteSpace(token.RefreshToken))
        {
            throw new JsonException("Etsy returned an incomplete OAuth token response.");
        }

        return token;
    }

    private static HttpRequestException CreateTokenRequestException(
        HttpStatusCode statusCode,
        string? reasonPhrase,
        string responseBody)
    {
        string? errorDescription = null;

        try
        {
            var error = JsonSerializer.Deserialize<ErrorResponse>(responseBody);
            if (error is not null)
            {
                errorDescription = string.IsNullOrWhiteSpace(error.ErrorDescription)
                    ? error.Error
                    : $"{error.Error}: {error.ErrorDescription}";
            }
        }
        catch (JsonException)
        {
            // A proxy may return a non-JSON response; the HTTP status is preserved below.
        }

        var message = string.IsNullOrWhiteSpace(errorDescription)
            ? $"The Etsy OAuth token request failed with status {(int)statusCode} ({reasonPhrase})."
            : $"The Etsy OAuth token request failed: {errorDescription}";

        return new HttpRequestException(message, null, statusCode);
    }

    private static void ValidateCodeVerifier(string codeVerifier)
    {
        AuthHelper.ValidateCodeVerifier(codeVerifier);
    }
}
