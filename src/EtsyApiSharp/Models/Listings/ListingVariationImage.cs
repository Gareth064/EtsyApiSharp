using System.Text.Json.Serialization;

namespace EtsyApiSharp.Models;

/// <summary>
/// A representation of the associations of variations and images on a listing.
/// </summary>
public class ListingVariationImage
{
    /// <summary>
    /// The numeric ID of the Property.
    /// </summary>
    [JsonPropertyName("property_id")]
    public long PropertyId { get; set; }

    /// <summary>
    /// The numeric ID of the Value.
    /// </summary>
    [JsonPropertyName("value_id")]
    public long ValueId { get; set; }

    /// <summary>
    /// The string value of the property.
    /// </summary>
    [JsonPropertyName("value")]
    public string Value { get; set; }

    /// <summary>
    /// The numeric ID of the Image.
    /// </summary>
    [JsonPropertyName("image_id")]
    public long ImageId { get; set; }

}
