using System.Text.Json.Serialization;

namespace EtsyApiSharp.Models;
/// <summary>
/// Represents Listing Translation.
/// </summary>

public class ListingTranslation
{
    /// <summary>
    /// The numeric ID for the Listing.
    /// </summary>
    [JsonPropertyName("listing_id")]
    public long ListingId { get; set; }

    /// <summary>
    /// The IETF language tag (e.g. 'fr') for the language of this translation.
    /// </summary>
    [JsonPropertyName("language")]
    public string Language { get; set; }

    /// <summary>
    /// The title of the Listing of this Translation.
    /// </summary>
    [JsonPropertyName("title")]
    public string Title { get; set; }

    /// <summary>
    /// The description of the Listing of this Translation.
    /// </summary>
    [JsonPropertyName("description")]
    public string Description { get; set; }

    /// <summary>
    /// The tags of the Listing of this Translation.
    /// </summary>
    [JsonPropertyName("tags")]
    public List<string> Tags { get; set; }

}
