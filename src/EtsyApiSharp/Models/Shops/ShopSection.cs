using System.Text.Json.Serialization;

namespace EtsyApiSharp.Models;

/// <summary>
/// A section within a shop, into which a user can sort listings.
/// </summary>
public class ShopSection
{
    /// <summary>
    /// The numeric ID of a section in a specific Etsy shop.
    /// </summary>
    [JsonPropertyName("shop_section_id")]
    public long ShopSectionId { get; set; }

    /// <summary>
    /// The title string for a shop section.
    /// </summary>
    [JsonPropertyName("title")]
    public string? Title { get; set; }

    /// <summary>
    /// The positive non-zero numeric position of this section in the section display order for a shop, with rank 1 sections appearing first.
    /// </summary>
    [JsonPropertyName("rank")]
    public long Rank { get; set; }

    /// <summary>
    /// The numeric ID of the [user](/documentation/reference#tag/User) who owns this shop section.
    /// </summary>
    [JsonPropertyName("user_id")]
    public long UserId { get; set; }

    /// <summary>
    /// The number of active listings in one section of a specific Etsy shop.
    /// </summary>
    [JsonPropertyName("active_listing_count")]
    public long ActiveListingCount { get; set; }

}
