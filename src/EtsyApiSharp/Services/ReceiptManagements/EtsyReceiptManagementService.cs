using EtsyApiSharp.Helpers.Extensions;
using EtsyApiSharp.Models;
using EtsyApiSharp.Models.Common;
using EtsyApiSharp.Models.Filters;
using System.Net.Http.Headers;
using System.Text.Json;

namespace EtsyApiSharp.Services.ReceiptManagements
{
    public class EtsyReceiptManagementService : IEtsyReceiptManagementService
    {
        private readonly HttpClient _httpClient;

        public EtsyReceiptManagementService(string clientId)
        {
            _httpClient = new HttpClient { BaseAddress = new Uri(Url.baseApiUrl) };
            _httpClient.DefaultRequestHeaders.Add("x-api-key", clientId);
        }

        public async Task<ApiResponse<ShopReceipt>> CreateReceiptShipmentAsync(string apiToken, long shopId, ShopReceiptShipment shopReceiptShipment)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResponse<ShopReceipt>> GetShopReceiptAsync(string apiToken, long shopId, long receiptid)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiToken);

                string responseBody = await _httpClient.GetStringAsync($"/v3/application/shops/{shopId}/receipts/{receiptid}");
                var reciept = JsonSerializer.Deserialize<ShopReceipt>(responseBody);
                var result = new ApiResponse<ShopReceipt>
                {
                    ResponseCode = 200,
                    Success = true,
                    Data = reciept,
                    Message = null
                };
                return result;
            }
            catch (HttpRequestException ex)
            {
                var result = new ApiResponse<ShopReceipt>
                {
                    ResponseCode = 500,
                    Success = false,
                    Data = null,
                    Message = $"{ex.Message}"
                };
                return result;
            }
        }

        public async Task<ApiResponse<EtsyListResponse<ShopReceipt>>> GetShopReceiptsAsync(string apiToken, long shopId, GetShopReceiptsFilter filter)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiToken);

                UriBuilder baseUri = new UriBuilder($"{_httpClient.BaseAddress}/v3/application/shops/{shopId}/receipts");

                if (filter.Limit is not 25)
                    baseUri.AddQueryParam("limit", filter.Limit.ToString());

                if (filter.Offset is not 0)
                    baseUri.AddQueryParam("offset", filter.Offset.ToString());

                if (filter.MinCreated is not null)
                    baseUri.AddQueryParam("min_created", filter.MinCreated.ToString());

                if (filter.MaxLastModified is not null)
                    baseUri.AddQueryParam("max_created", filter.MaxCreated.ToString());

                if (filter.MinLastModified is not null)
                    baseUri.AddQueryParam("min_last_modified", filter.MinLastModified.ToString());

                if (filter.MaxLastModified is not null)
                    baseUri.AddQueryParam("max_last_modified", filter.MaxLastModified.ToString());

                if (filter.WasPaid is not null)
                    baseUri.AddQueryParam("was_paid", filter.WasPaid.ToString());

                if (filter.WasShipped is not null)
                    baseUri.AddQueryParam("was_shipped", filter.WasShipped.ToString());

                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, baseUri.Uri);

                var response = _httpClient.SendAsync(request);
                var bodyContent = await response.Result.Content.ReadAsStringAsync();
                //var reciepts = JsonConvert.DeserializeObject<EtsyListResponse<ShopReceipt>>(bodyContent);
                var reciepts = System.Text.Json.JsonSerializer.Deserialize<EtsyListResponse<ShopReceipt>>(bodyContent);

                var result = new ApiResponse<EtsyListResponse<ShopReceipt>>
                {
                    ResponseCode = 200,
                    Success = true,
                    Data = reciepts,
                    Message = null
                };
                return result;
            }
            catch (HttpRequestException ex)
            {
                var result = new ApiResponse<EtsyListResponse<ShopReceipt>>
                {
                    ResponseCode = 500,
                    Success = false,
                    Data = null,
                    Message = $"{ex.Message}"
                };
                return result;
            }
        }

        public async Task<ApiResponse<EtsyListResponse<ShopReceiptTransaction>>> GetShopReceiptTransactionsByListingAsync(string apiToken, long shopId, long listingId)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiToken);

                string responseBody = await _httpClient.GetStringAsync($"/v3/application/shops/{shopId}/listings/{listingId}/transactions");
                var reciept = JsonSerializer.Deserialize<EtsyListResponse<ShopReceiptTransaction>>(responseBody);
                var result = new ApiResponse<EtsyListResponse<ShopReceiptTransaction>>
                {
                    ResponseCode = 200,
                    Success = true,
                    Data = reciept,
                    Message = null
                };
                return result;
            }
            catch (HttpRequestException ex)
            {
                var result = new ApiResponse<EtsyListResponse<ShopReceiptTransaction>>
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
}
