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
            _httpClient = new HttpClient { BaseAddress = new Uri(Url.AuthUrls.BaseApiUrl) };
            _httpClient.DefaultRequestHeaders.Add("x-api-key", clientId);
        }

        public async Task<ApiResponse<ShopReceipt>> CreateReceiptShipmentAsync(
            string apiToken,
            long shopId,
            ShopReceiptShipment shopReceiptShipment)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResponse<ShopReceipt>> GetShopReceiptAsync(
            string apiToken,
            long shopId,
            long receiptId)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiToken);
                string responseBody = await _httpClient.GetStringAsync(Url.ReceiptUrls.GetShopReceipt(shopId: shopId, receiptId: receiptId));
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
                    ResponseCode = (int)ex.StatusCode,
                    Success = false,
                    Data = null,
                    Message = $"{ex.Message}"
                };

                return result;
            }
        }

        public async Task<ApiResponse<EtsyListResponse<ShopReceipt>>> GetShopReceiptsAsync(
            string apiToken,
            long shopId,
            GetShopReceiptsFilter filter)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiToken);
                UriBuilder baseUri = new UriBuilder($"{_httpClient.BaseAddress}{Url.ReceiptUrls.GetShopReceipts(shopId: shopId)}");

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

                if (filter.WasDelivered is not null)
                    baseUri.AddQueryParam("was_delivered", filter.WasDelivered.ToString());

                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, baseUri.Uri);
                var response = _httpClient.SendAsync(request);
                var bodyContent = await response.Result.Content.ReadAsStringAsync();
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
                    ResponseCode = (int)ex.StatusCode,
                    Success = false,
                    Data = null,
                    Message = $"{ex.Message}"
                };

                return result;
            }
        }

        public async Task<ApiResponse<ShopReceiptTransaction>> GetShopReceiptTransactionAsync(
            string apiToken,
            long shopId,
            long transactionId)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiToken);

                string responseBody = await _httpClient.GetStringAsync(
                    Url.ReceiptUrls.GetShopReceiptTransaction(shopId: shopId, transactionId: transactionId));

                var reciept = JsonSerializer.Deserialize<ShopReceiptTransaction>(responseBody);

                var result = new ApiResponse<ShopReceiptTransaction>
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
                var result = new ApiResponse<ShopReceiptTransaction>
                {
                    ResponseCode = (int)ex.StatusCode,
                    Success = false,
                    Data = null,
                    Message = $"{ex.Message}"
                };

                return result;
            }
        }

        public async Task<ApiResponse<EtsyListResponse<ShopReceiptTransaction>>> GetShopReceiptTransactionsByListingAsync(
            string apiToken,
            long shopId,
            long listingId,
            GetShopReceiptTransactionsByListingFilter filter)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiToken);

                string responseBody = await _httpClient.GetStringAsync(
                    Url.ReceiptUrls.GetShopReceiptTransactionsByListing(shopId: shopId, listingId: listingId));

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
                    ResponseCode = (int)ex.StatusCode,
                    Success = false,
                    Data = null,
                    Message = $"{ex.Message}"
                };

                return result;
            }
        }

        public async Task<ApiResponse<EtsyListResponse<ShopReceiptTransaction>>> GetShopReceiptTransactionsByReceiptAsync(
            string apiToken,
            long shopId,
            long receiptId)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(scheme: "Bearer", parameter: apiToken);

                string responseBody = await _httpClient.GetStringAsync(
                    Url.ReceiptUrls.GetShopReceiptTransactionsByReceipt(shopId: shopId, receiptId: receiptId));

                var reciept = JsonSerializer.Deserialize<EtsyListResponse<ShopReceiptTransaction>>(json: responseBody);

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
                    ResponseCode = (int)ex.StatusCode,
                    Success = false,
                    Data = null,
                    Message = $"{ex.Message}"
                };

                return result;
            }
        }

        public async Task<ApiResponse<EtsyListResponse<ShopReceiptTransaction>>> GetShopReceiptTransactionsByShopAsync(
            string apiToken,
            long shopId,
            GetShopReceiptTransactionsByShopFilter filter)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiToken);

                UriBuilder baseUri = new UriBuilder(
                    $"{_httpClient.BaseAddress}{Url.ReceiptUrls.GetShopReceiptTransactionsByShop(shopId: shopId)}");

                if (filter.Limit is not 25)
                    baseUri.AddQueryParam("limit", filter.Limit.ToString());

                if (filter.Offset is not 0)
                    baseUri.AddQueryParam("offset", filter.Offset.ToString());

                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, baseUri.Uri);
                var response = _httpClient.SendAsync(request);
                var bodyContent = await response.Result.Content.ReadAsStringAsync();
                var transactions = JsonSerializer.Deserialize<EtsyListResponse<ShopReceiptTransaction>>(bodyContent);

                var result = new ApiResponse<EtsyListResponse<ShopReceiptTransaction>>
                {
                    ResponseCode = 200,
                    Success = true,
                    Data = transactions,
                    Message = null
                };

                return result;
            }
            catch (HttpRequestException ex)
            {
                var result = new ApiResponse<EtsyListResponse<ShopReceiptTransaction>>
                {
                    ResponseCode = (int)ex.StatusCode,
                    Success = false,
                    Data = null,
                    Message = $"{ex.Message}"
                };

                return result;
            }
        }

    }
}
