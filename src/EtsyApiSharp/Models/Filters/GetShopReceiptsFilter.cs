using EtsyApiSharp.Models.ShopReceipts.Enums;

namespace EtsyApiSharp.Models.Filters;
/// <summary>
/// Represents Get Shop Receipts Filter.
/// </summary>

public class GetShopReceiptsFilter : EtsyFilterBase
{
    /// <summary>
    /// Gets or sets the Min Created.
    /// </summary>
    public long? MinCreated { get; set; }
    /// <summary>
    /// Gets or sets the Max Created.
    /// </summary>
    public long? MaxCreated { get; set; }
    /// <summary>
    /// Gets or sets the Min Last Modified.
    /// </summary>
    public long? MinLastModified { get; set; }
    /// <summary>
    /// Gets or sets the Max Last Modified.
    /// </summary>
    public long? MaxLastModified { get; set; }
    /// <summary>
    /// Gets or sets the Sort On.
    /// </summary>
    public ReceiptSortOn SortOn { get; set; } = ReceiptSortOn.created;
    /// <summary>
    /// Gets or sets the Sort Order.
    /// </summary>
    public ReceiptSortOrder SortOrder { get; set; } = ReceiptSortOrder.desc;
    /// <summary>
    /// Gets or sets the Was Paid.
    /// </summary>
    public bool? WasPaid { get; set; }
    /// <summary>
    /// Gets or sets the Was Cancelled.
    /// </summary>
    public bool? WasCancelled { get; set; }
    /// <summary>
    /// Gets or sets the Was Shipped.
    /// </summary>
    public bool? WasShipped { get; set; }
    /// <summary>
    /// Gets or sets the Was Delivered.
    /// </summary>
    public bool? WasDelivered { get; set; }
    /// <summary>
    /// Gets or sets the Legacy.
    /// </summary>
    public bool? Legacy { get; set; }
}
