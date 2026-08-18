using System.Text.Json.Serialization;

namespace EtsyApiSharp.Models.ShopPolicies;

/// <summary>Represents a listing-level return policy for an Etsy shop.</summary>
public sealed class ShopReturnPolicy
{
    /// <summary>
    /// Gets or sets the Return Policy Id.
    /// </summary>
    [JsonPropertyName("return_policy_id")]
    public long ReturnPolicyId { get; set; }
    /// <summary>
    /// Gets or sets the Shop Id.
    /// </summary>

    [JsonPropertyName("shop_id")]
    public long ShopId { get; set; }
    /// <summary>
    /// Gets or sets the Accepts Returns.
    /// </summary>

    [JsonPropertyName("accepts_returns")]
    public bool AcceptsReturns { get; set; }
    /// <summary>
    /// Gets or sets the Accepts Exchanges.
    /// </summary>

    [JsonPropertyName("accepts_exchanges")]
    public bool AcceptsExchanges { get; set; }
    /// <summary>
    /// Gets or sets the Return Deadline.
    /// </summary>

    [JsonPropertyName("return_deadline")]
    public long? ReturnDeadline { get; set; }
}
