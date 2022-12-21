using System.Text.Json.Serialization;

namespace EtsyApiSharp.Models;

/// <summary>
/// A representation of an offering for a listing.
/// </summary>
public class ListingInventoryProductOffering
{
    /// <summary>
    /// The ID for the ProductOffering
    /// </summary>
    [JsonPropertyName("offering_id")]
    public long OfferingId { get; set; }

    /// <summary>
    /// The quantity the ProductOffering
    /// </summary>
    [JsonPropertyName("quantity")]
    public int Quantity { get; set; }

    /// <summary>
    /// Whether or not the offering can be shown to buyers.
    /// </summary>
    [JsonPropertyName("is_enabled")]
    public bool IsEnabled { get; set; }

    /// <summary>
    /// Whether or not the offering has been deleted.
    /// </summary>
    [JsonPropertyName("is_deleted")]
    public bool IsDeleted { get; set; }

    /// <summary>
    /// Price data for this ProductOffering
    /// </summary>
    [JsonPropertyName("price")]
    public Money Price { get; set; }

}
