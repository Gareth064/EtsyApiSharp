using EtsyApiSharp.Models.ShopReceipts.Enums;

namespace EtsyApiSharp.Models.Filters;

public class GetShopReceiptsFilter : EtsyFilterBase
{
    public long? MinCreated { get; set; }
    public long? MaxCreated { get; set; }
    public long? MinLastModified { get; set; }
    public long? MaxLastModified { get; set; }
    public ReceiptSortOn SortOn { get; set; } = ReceiptSortOn.created;
    public ReceiptSortOrder SortOrder { get; set; } = ReceiptSortOrder.desc;
    public bool? WasPaid { get; set; }
    public bool? WasCancelled { get; set; }
    public bool? WasShipped { get; set; }
    public bool? WasDelivered { get; set; }
    public bool? Legacy { get; set; }
}
