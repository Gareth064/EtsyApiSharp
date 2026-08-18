namespace EtsyApiSharp.Models.Filters;

/// <summary>
/// Filters a shop's payment-account ledger entries by their creation timestamp.
/// </summary>
public class GetShopPaymentAccountLedgerEntriesFilter : EtsyFilterBase
{
    /// <summary>
    /// Gets or sets the Min Created.
    /// </summary>
    public long MinCreated { get; set; }
    /// <summary>
    /// Gets or sets the Max Created.
    /// </summary>

    public long MaxCreated { get; set; }
}
