using EtsyApiSharp.Models;

namespace EtsyApiSharp.Services
{
    internal interface IEtsyAuthService
    {
        public string BuildAuthorizationUrl(string codeVerifier, List<Scope>? scopes = null);
        public Task<EtsyTokenResponse> GetFirstAccessTokenAsync(string authCode, string codeVerifier);
        public Task<EtsyTokenResponse> GetRefreshAccessTokenAsync(string refreshToken);
    }
}
