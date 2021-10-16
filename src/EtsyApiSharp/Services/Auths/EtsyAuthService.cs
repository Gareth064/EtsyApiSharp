using EtsyApiSharp.Helpers;
using EtsyApiSharp.Models;
using Newtonsoft.Json;
using System.Text;

namespace EtsyApiSharp.Services
{
    public class EtsyAuthService : IEtsyAuthService
    {
        private readonly string clientId;
        private readonly string redirectUrl;
        private readonly string state = "superstate";

        public EtsyAuthService(string clientId, string redirectUrl)
        {
            this.clientId = clientId;
            this.redirectUrl = redirectUrl;
        }

        public string BuildAuthorizationUrl(string codeVerifier, List<Scope>? scopes = null)
        {
            StringBuilder sbUri = new StringBuilder(Url.baseAuthUrl);
            sbUri.Append("?response_type=" + "code");
            sbUri.Append("&redirect_uri=" + redirectUrl);
            sbUri.Append("&scope=" + PermissionsScopes(scopes));
            sbUri.Append("&client_id=" + clientId);
            sbUri.Append("&code_challenge=" + AuthHelper.CreateCodeChallenge(codeVerifier));
            sbUri.Append("&code_challenge_method=" + "S256");
            sbUri.Append("&state=" + state);
            return sbUri.ToString();
        }

        public async Task<EtsyTokenResponse> GetFirstAccessTokenAsync(string authCode, string codeVerifier)
        {
            EtsyTokenResponse? token = null;

            var formData = new List<KeyValuePair<string, string>>();
            formData.Add(new KeyValuePair<string, string>("grant_type", "authorization_code"));
            formData.Add(new KeyValuePair<string, string>("client_id", $"{clientId}"));
            formData.Add(new KeyValuePair<string, string>("redirect_uri", $"{redirectUrl}"));
            formData.Add(new KeyValuePair<string, string>("code", $"{authCode}"));
            formData.Add(new KeyValuePair<string, string>("code_verifier", $"{codeVerifier}"));

            using (var httpClient = new HttpClient())
            {
                var req = new HttpRequestMessage(HttpMethod.Post, Url.baseTokenUrl)
                {
                    Content = new FormUrlEncodedContent(formData)
                };

                using (var response = await httpClient.SendAsync(req))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string responseString = await response.Content.ReadAsStringAsync();
                        token = JsonConvert.DeserializeObject<EtsyTokenResponse>(responseString);
                    }
                }
            }
            return token;
        }

        public async Task<EtsyTokenResponse> GetRefreshAccessTokenAsync(string refreshToken)
        {
            EtsyTokenResponse? token = null;

            var formData = new List<KeyValuePair<string, string>>();
            formData.Add(new KeyValuePair<string, string>("grant_type", "refresh_token"));
            formData.Add(new KeyValuePair<string, string>("client_id", $"{clientId}"));
            formData.Add(new KeyValuePair<string, string>("refresh_token", $"{refreshToken}"));

            using (var httpClient = new HttpClient())
            {
                var req = new HttpRequestMessage(HttpMethod.Post, Url.baseTokenUrl)
                {
                    Content = new FormUrlEncodedContent(formData)
                };

                using (var response = await httpClient.SendAsync(req))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string responseString = await response.Content.ReadAsStringAsync();
                        token = JsonConvert.DeserializeObject<EtsyTokenResponse>(responseString);
                    }
                }
            }
            return token;
        }

        private string PermissionsScopes(List<Scope> scopes)
        {
            return string.Join(" ", scopes);
        }
    }
}
