namespace EtsyApiSharp.Models.ShopPolicies;

/// <summary>Values required to create an Etsy shop return policy.</summary>
public sealed class CreateShopReturnPolicyRequest
{
    public bool AcceptsReturns { get; set; }

    public bool AcceptsExchanges { get; set; }

    /// <summary>Optional return deadline in days: 7, 14, 21, 30, 45, 60, or 90.</summary>
    public long? ReturnDeadline { get; set; }
}
