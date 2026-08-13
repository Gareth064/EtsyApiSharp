namespace EtsyApiSharp.Models.ShopPolicies;

/// <summary>Identifies the source and destination return policies for consolidation.</summary>
public sealed class ConsolidateShopReturnPoliciesRequest
{
    public long SourceReturnPolicyId { get; set; }

    public long DestinationReturnPolicyId { get; set; }
}
