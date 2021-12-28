using EtsyApiSharp.Models;
using EtsyApiSharp.Models.Common;
using EtsyApiSharp.Models.Filters;

namespace EtsyApiSharp.Services.ReceiptManagements
{
    public interface IEtsyReceiptManagementService
    {
        Task<ApiResponse<ShopReceipt>> GetShopReceiptAsync(string apiToken, long shopId, long receiptid);
        Task<ApiResponse<EtsyListResponse<ShopReceipt>>> GetShopReceiptsAsync(string apiToken, long shopId, GetShopReceiptsFilter queryParams);
        Task<ApiResponse<ShopReceipt>> CreateReceiptShipmentAsync(string apiToken, long shopId, ShopReceiptShipment shopReceiptShipment);
        Task<ApiResponse<EtsyListResponse<ShopReceiptTransaction>>> GetShopReceiptTransactionsByListingAsync(string apiToken, long shopId, long listingId);
        Task<ApiResponse<EtsyListResponse<ShopReceiptTransaction>>> GetShopReceiptTransactionsByReceiptAsync(string apiToken, long shopId, long receiptid);
    }
}
