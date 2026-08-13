using System.Text.Json.Serialization;

namespace EtsyApiSharp.Models.ShopPolicies;

/// <summary>Represents a listing-level return policy for an Etsy shop.</summary>
public sealed class ShopReturnPolicy
{
    [JsonPropertyName("return_policy_id")]
    public long ReturnPolicyId { get; set; }

    [JsonPropertyName("shop_id")]
    public long ShopId { get; set; }

    [JsonPropertyName("accepts_returns")]
    public bool AcceptsReturns { get; set; }

    [JsonPropertyName("accepts_exchanges")]
    public bool AcceptsExchanges { get; set; }

    [JsonPropertyName("return_deadline")]
    public long? ReturnDeadline { get; set; }
}
