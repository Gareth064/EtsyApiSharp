using EtsyApiSharp.Models;
using EtsyApiSharp.Models.Common;
using EtsyApiSharp.Models.Requests;

namespace EtsyApiSharp.Services.ReceiptManagements
{
    public interface IEtsyReceiptManagementService
    {
        Task<ApiResponse<ShopReceipt>> GetShopReceiptAsync(string apiToken, long shopId, long receiptid);
        Task<ApiResponse<ShopReceipts>> GetShopReceiptsAsync(string apiToken, long shopId, GetShopReceiptsRequest queryParams);
        Task<ApiResponse<ShopReceipt>> CreateReceiptShipmentAsync(long shopId, ShopReceiptShipment shopReceiptShipment);
        Task<ApiListResponse<ShopReceiptTransactions>> GetShopReceiptTransactionsByListingAsync(long shopId, long listingId);
    }
}
