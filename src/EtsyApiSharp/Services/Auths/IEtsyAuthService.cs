using EtsyApiSharp.Models;

namespace EtsyApiSharp.Services.Auths;
/// <summary>
/// Represents I Etsy Auth Service.
/// </summary>

public interface IEtsyAuthService
{
    /// <summary>
    /// Builds an Etsy OAuth authorization URL using PKCE.
    /// </summary>
    /// <param name="codeVerifier">The verifier retained for the subsequent token request.</param>
    /// <param name="state">A cryptographically random, single-use value that the caller must validate on callback.</param>
    string BuildAuthorizationUrl(string codeVerifier, string state);

    /// <summary>
    /// Exchanges an Etsy authorization code for an access token and refresh token.
    /// </summary>
    Task<EtsyTokenResponse> GetFirstAccessTokenAsync(
        string authCode,
        string codeVerifier,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Exchanges an Etsy refresh token for a fresh access token and refresh token.
    /// </summary>
    Task<EtsyTokenResponse> GetRefreshAccessTokenAsync(
        string refreshToken,
        CancellationToken cancellationToken = default);
}
