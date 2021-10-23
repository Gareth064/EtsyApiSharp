using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EtsyApiSharp.Models;

namespace EtsyApiSharp.Services.ReceiptManagements
{
    public interface IEtsyReceiptManagementService
    {
        ShopReceipt GetShopReceipt(long shopId, long receiptid);
    }
}
