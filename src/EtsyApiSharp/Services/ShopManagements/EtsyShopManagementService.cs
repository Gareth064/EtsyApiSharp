using EtsyApiSharp.Helpers;
using EtsyApiSharp.Helpers.Extensions;
using EtsyApiSharp.Infrastructure;
using EtsyApiSharp.Models;
using EtsyApiSharp.Models.Common;
using EtsyApiSharp.Models.Filters;
using System.Diagnostics;
using System.Net.Http.Headers;

namespace EtsyApiSharp.Services.ShopManagements;

public class EtsyShopManagementService : IEtsyShopManagementService
{
    private static IHttpClientFactory httpClientFactory = new DefaultHttpClientFactory();
    private readonly string clientId;

    public EtsyShopManagementService(string clientId)
    {
        this.clientId=clientId;
    }

    public async Task<ApiResponse<EtsyListResponse<Shop>>> FindShopsAsync(
        string apiToken,
        string shopName,
        FindShopsByNameFilter? filter = null)
    {
        using (var httpClient = httpClientFactory.CreateClient())
        {
            try
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiToken);
                httpClient.DefaultRequestHeaders.Add("x-api-key", clientId);
                UriBuilder baseUri = new($"{Url.BaseUrls.BaseApiUrl}{Url.ShopUrls.FindShops()}");

                if (filter is not null)
                {
                    if (filter.Limit is not 25)
                        baseUri.AddQueryParam("limit", filter.Limit.ToString());

                    if (filter.Offset is not 0)
                        baseUri.AddQueryParam("offset", filter.Offset.ToString());
                }

                HttpRequestMessage request = new(HttpMethod.Get, baseUri.Uri);

                var response = httpClient.SendAsync(request).Result;
                var result = await EtsyResponseParser.ParseResponseOfList<Shop>(response);

                return result;
            }
            catch (HttpRequestException ex)
            {
                var result = new ApiResponse<EtsyListResponse<Shop>>
                {
                    ResponseCode = (int)ex.StatusCode!,
                    Success = false,
                    Data = null,
                    Message = $"{ex.Message}"
                };

                return result;
            }
        }
    }

    public async Task<ApiResponse<Shop>> GetShopAsync(
        string apiToken,
        long shopId)
    {
        using (var httpClient = httpClientFactory.CreateClient())
        {

            try
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiToken);
                httpClient.DefaultRequestHeaders.Add("x-api-key", clientId);
                UriBuilder baseUri = new($"{Url.BaseUrls.BaseApiUrl}{Url.ShopUrls.GetShop(shopId: shopId)}");
                HttpRequestMessage request = new(HttpMethod.Get, baseUri.Uri);
                var response = httpClient.SendAsync(request).Result;
                var result = await EtsyResponseParser.ParseResponseOfSingle<Shop>(response);

                return result;
            }
            catch (HttpRequestException ex)
            {
                Debug.WriteLine(ex);
                var result = new ApiResponse<Shop>
                {
                    ResponseCode = (int)ex.StatusCode!,
                    Success = false,
                    Data = null,
                    Message = $"{ex.Message}"
                };

                return result;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                var result = new ApiResponse<Shop>
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

    public async Task<ApiResponse<Shop>> GetShopByOwnerUserIdAsync(
        string apiToken,
        long userId)
    {
        try
        {
            UriBuilder baseUri = new($"{Url.BaseUrls.BaseApiUrl}{Url.ShopUrls.GetShopByOwnerUserId(userId: userId)}");
            HttpRequestMessage request = new(HttpMethod.Get, baseUri.Uri);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", apiToken);
            request.Headers.Add("x-api-key", clientId);
            var httpClient = httpClientFactory.CreateClient();
            var response = await httpClient.SendAsync(request);
            var result = await EtsyResponseParser.ParseResponseOfSingle<Shop>(response);

            return result;
        }
        catch (HttpRequestException ex)
        {
            Debug.WriteLine(ex);
            var result = new ApiResponse<Shop>
            {
                ResponseCode = (int)ex.StatusCode!,
                Success = false,
                Data = null,
                Message = $"{ex.Message}"
            };

            return result;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            var result = new ApiResponse<Shop>
            {
                ResponseCode = 500,
                Success = false,
                Data = null,
                Message = $"{ex.Message}"
            };

            return result;
        }
    }

    public async Task<ApiResponse<Shop>> UpdateShopAsync(
        string apiToken,
        long shopId,
        Shop shop)
    {
        throw new NotImplementedException();
    }
}
