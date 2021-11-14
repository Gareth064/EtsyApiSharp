using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EtsyApiSharp.Models;
using EtsyApiSharp.Models.Requests;

namespace EtsyApiSharp.Services.ReceiptManagements
{
    public interface IEtsyReceiptManagementService
    {
        Task<ShopReceipt> GetShopReceiptAsync(long shopId, long receiptid);
        Task<ShopReceipts> GetShopReceiptsAsync(long shopId, GetShopReceiptsRequest queryParams);
        Task<ShopReceipt> CreateReceiptShipmentAsync(long shopId, ShopReceiptShipment shopReceiptShipment);
        Task<ShopReceiptTransactions> GetShopReceiptTransactionsByListingAsync(long shopId, long listingId);
    }
}
