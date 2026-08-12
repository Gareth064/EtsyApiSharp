namespace EtsyApiSharp.Models.Filters;

/// <summary>
/// Filters a shop's payment-account ledger entries by their creation timestamp.
/// </summary>
public class GetShopPaymentAccountLedgerEntriesFilter : EtsyFilterBase
{
    /// <summary>
    /// Gets or sets the earliest creation time, as a Unix timestamp. Required by Etsy.
    /// </summary>
    public long MinCreated { get; set; }

    /// <summary>
    /// Gets or sets the latest creation time, as a Unix timestamp. Required by Etsy.
    /// </summary>
    public long MaxCreated { get; set; }
}
