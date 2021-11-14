using EtsyApiSharp.Models;
using EtsyApiSharp.Models.Common;
using EtsyApiSharp.Models.Requests;
using Newtonsoft.Json;

namespace EtsyApiSharp.Services.ReceiptManagements
{
    public class EtsyReceiptManagementService : IEtsyReceiptManagementService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiToken;

        public EtsyReceiptManagementService(string apiToken)
        {
            _httpClient = new HttpClient { BaseAddress = new Uri(Url.baseApiUrl) };
            _apiToken = apiToken;
        }
        public async Task<ShopReceipt> CreateReceiptShipmentAsync(long shopId, ShopReceiptShipment shopReceiptShipment)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResponse> GetShopReceiptAsync(long shopId, long receiptid)
        {
            try
            {
                string responseBody = await _httpClient.GetStringAsync($"/v3/application/shops/{shopId}/receipts/{receiptid}");
                var reciept = JsonConvert.DeserializeObject<ShopReceipt>(responseBody);
                var result = new ApiResponse
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
                var result = new ApiResponse
                {
                    ResponseCode = 500,
                    Success = false,
                    Data = null,
                    Message = $"{ex.Message}"
                };

            return result;
            }
        }

        public Task<ShopReceipts> GetShopReceiptsAsync(long shopId, GetShopReceiptsRequest queryParams)
        {
            throw new NotImplementedException();
        }

        public Task<ShopReceiptTransactions> GetShopReceiptTransactionsByListingAsync(long shopId, long listingId)
        {
            throw new NotImplementedException();
        }
    }
}
