using EtsyApiSharp.Helpers;
using EtsyApiSharp.Infrastructure;
using EtsyApiSharp.Models;
using System.Text;
using System.Text.Json;

namespace EtsyApiSharp.Services.Auths;

public class EtsyAuthService : IEtsyAuthService
{
    private readonly string clientId;
    private readonly string redirectUrl;
    private readonly List<Scope> scopes;
    private readonly string state = "superstate";
    private static IHttpClientFactory httpClientFactory = new DefaultHttpClientFactory();
    private HttpClient httpClient;

    public EtsyAuthService(string clientId, string redirectUrl, List<Scope> scopes)
    {
        this.clientId = clientId;
        this.redirectUrl = redirectUrl;
        this.scopes=scopes;
        httpClient = httpClientFactory.CreateClient();
        httpClient.BaseAddress = new Uri(Url.AuthUrls.BaseTokenUrl);
    }

    public string BuildAuthorizationUrl(string codeVerifier)
    {
        StringBuilder sbUri = new(Url.AuthUrls.BaseAuthUrl);
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


        HttpRequestMessage baseRequest = new(HttpMethod.Post, Url.AuthUrls.BaseTokenUrl);
        baseRequest.Content = new FormUrlEncodedContent(formData);
        var response = httpClient.SendAsync(baseRequest).Result;
        string responseString = await response.Content.ReadAsStringAsync();
        token = JsonSerializer.Deserialize<EtsyTokenResponse>(responseString);

        return token!;
    }

    public async Task<EtsyTokenResponse> GetRefreshAccessTokenAsync(string refreshToken)
    {
        EtsyTokenResponse? token = null;

        var formData = new List<KeyValuePair<string, string>>();
        formData.Add(new KeyValuePair<string, string>("grant_type", "refresh_token"));
        formData.Add(new KeyValuePair<string, string>("client_id", $"{clientId}"));
        formData.Add(new KeyValuePair<string, string>("refresh_token", $"{refreshToken}"));

        HttpRequestMessage baseRequest = new(HttpMethod.Post, Url.AuthUrls.BaseTokenUrl);
        baseRequest.Content = new FormUrlEncodedContent(formData);
        var request = httpClient.SendAsync(baseRequest);

        using (var response = await request)
        {
            string responseString = await response.Content.ReadAsStringAsync();
            token = JsonSerializer.Deserialize<EtsyTokenResponse>(responseString);
        }

        return token!;
    }

    private static string PermissionsScopes(List<Scope> scopes)
    {
        return string.Join(" ", scopes);
    }
}
