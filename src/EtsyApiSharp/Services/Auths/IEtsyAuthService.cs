using EtsyApiSharp.Models;

namespace EtsyApiSharp.Services.Auths;

public interface IEtsyAuthService
{
    public string BuildAuthorizationUrl(string codeVerifier);
    public Task<EtsyTokenResponse> GetFirstAccessTokenAsync(string authCode, string codeVerifier);
    public Task<EtsyTokenResponse> GetRefreshAccessTokenAsync(string refreshToken);
}
