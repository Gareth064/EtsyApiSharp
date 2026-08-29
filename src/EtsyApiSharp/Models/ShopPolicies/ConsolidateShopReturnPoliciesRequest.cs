namespace EtsyApiSharp.Models.ShopPolicies;

/// <summary>Identifies the source and destination return policies for consolidation.</summary>
public sealed class ConsolidateShopReturnPoliciesRequest
{
    /// <summary>
    /// Gets or sets the Source Return Policy Id.
    /// </summary>
    public long SourceReturnPolicyId { get; set; }
    /// <summary>
    /// Gets or sets the Destination Return Policy Id.
    /// </summary>

    public long DestinationReturnPolicyId { get; set; }
}
