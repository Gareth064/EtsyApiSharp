using EtsyApiSharp.Models;
using EtsyApiSharp.Models.Common;
using EtsyApiSharp.Models.Requests;
using Newtonsoft.Json;

namespace EtsyApiSharp.Services.ReceiptManagements
{
    public class EtsyReceiptManagementService : IEtsyReceiptManagementService
    {
        private readonly HttpClient _httpClient;

        public EtsyReceiptManagementService()
        {
            _httpClient = new HttpClient { BaseAddress = new Uri(Url.baseApiUrl) };
        }
        public async Task<ApiResponse<ShopReceipt>> CreateReceiptShipmentAsync(long shopId, ShopReceiptShipment shopReceiptShipment)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResponse<ShopReceipt>> GetShopReceiptAsync(string apiToken, long shopId, long receiptid)
        {
            try
            {
                string responseBody = await _httpClient.GetStringAsync($"/v3/application/shops/{shopId}/receipts/{receiptid}");
                var reciept = JsonConvert.DeserializeObject<ShopReceipt>(responseBody);
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

        public Task<ApiListResponse<ShopReceipts>> GetShopReceiptsAsync(long shopId, GetShopReceiptsRequest queryParams)
        {
            throw new NotImplementedException();
        }

        public Task<ApiListResponse<ShopReceiptTransactions>> GetShopReceiptTransactionsByListingAsync(long shopId, long listingId)
        {
            throw new NotImplementedException();
        }
    }
}
