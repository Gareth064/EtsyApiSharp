using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EtsyApiSharp.Models;
using EtsyApiSharp.Models.Common;
using EtsyApiSharp.Models.Requests;

namespace EtsyApiSharp.Services.ReceiptManagements
{
    public interface IEtsyReceiptManagementService
    {
        Task<ApiResponse<ShopReceipt>> GetShopReceiptAsync(string apiToken, long shopId, long receiptid);
        Task<ApiListResponse<ShopReceipts>> GetShopReceiptsAsync(long shopId, GetShopReceiptsRequest queryParams);
        Task<ApiResponse<ShopReceipt>> CreateReceiptShipmentAsync(long shopId, ShopReceiptShipment shopReceiptShipment);
        Task<ApiListResponse<ShopReceiptTransactions>> GetShopReceiptTransactionsByListingAsync(long shopId, long listingId);
    }
}
