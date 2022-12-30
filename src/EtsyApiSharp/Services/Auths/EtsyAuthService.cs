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

    public EtsyAuthService(string clientId, string redirectUrl, List<Scope> scopes)
    {
        this.clientId = clientId;
        this.redirectUrl = redirectUrl;
        this.scopes=scopes;
    }

    public string BuildAuthorizationUrl(string codeVerifier)
    {
        StringBuilder sbUri = new(Url.BaseUrls.BaseAuthUrl);
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


        HttpRequestMessage baseRequest = new(HttpMethod.Post, Url.BaseUrls.BaseTokenUrl);
        baseRequest.Content = new FormUrlEncodedContent(formData);

        var httpClient = httpClientFactory.CreateClient();

        try
        {
            var response = await httpClient.SendAsync(baseRequest);
            string responseString = await response.Content.ReadAsStringAsync();
            token = JsonSerializer.Deserialize<EtsyTokenResponse>(responseString);

        }
        catch (Exception)
        {
            throw;
        }

        return token!;

    }

    public async Task<EtsyTokenResponse> GetRefreshAccessTokenAsync(string refreshToken)
    {
        EtsyTokenResponse? token = null;

        var formData = new List<KeyValuePair<string, string>>();
        formData.Add(new KeyValuePair<string, string>("grant_type", "refresh_token"));
        formData.Add(new KeyValuePair<string, string>("client_id", $"{clientId}"));
        formData.Add(new KeyValuePair<string, string>("refresh_token", $"{refreshToken}"));

        HttpRequestMessage baseRequest = new(HttpMethod.Post, Url.BaseUrls.BaseTokenUrl);
        baseRequest.Content = new FormUrlEncodedContent(formData);

        var httpClient = httpClientFactory.CreateClient();

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
