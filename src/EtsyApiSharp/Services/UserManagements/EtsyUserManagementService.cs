using EtsyApiSharp.Helpers;
using EtsyApiSharp.Infrastructure;
using EtsyApiSharp.Models;
using EtsyApiSharp.Models.Common;
using System.Diagnostics;
using System.Net.Http.Headers;

namespace EtsyApiSharp.Services.UserManagements;
public class EtsyUserManagementService : IEtsyUserManagementService
{
    private static IHttpClientFactory httpClientFactory = new DefaultHttpClientFactory();
    private readonly string apiKey;
    
    public EtsyUserManagementService(string clientId, string sharedSecret)
    {
        this.apiKey = $"{clientId}:{sharedSecret}";
    }
    public async Task<ApiResponse<User>> GetUserAsync(string apiToken, long userId)
    {
        try
        {
            var httpClient = httpClientFactory.CreateClient();
            UriBuilder baseUri = new($"{Url.BaseUrls.BaseApiUrl}{Url.UserUrls.GetUser(userId: userId)}");
            HttpRequestMessage request = new(HttpMethod.Get, baseUri.Uri);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", apiToken);
            request.Headers.Add("x-api-key", apiKey);
            var response = await httpClient.SendAsync(request);
            var result = await EtsyResponseParser.ParseResponseOfSingle<User>(response);

            return result;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            var result = new ApiResponse<User>
            {
                ResponseCode = 500,
                Success = false,
                Data = null,
                Message = $"{ex.Message}"
            };

            return result;
        }
    }
}
