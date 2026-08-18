using System.Text.Json.Serialization;

namespace EtsyApiSharp.Models;
/// <summary>
/// Represents Listing Video.
/// </summary>

public class ListingVideo
{
    /// <summary>
    /// Gets or sets the Video Id.
    /// </summary>
    [JsonPropertyName("video_id")]
    public long VideoId { get; set; }
    /// <summary>
    /// Gets or sets the Listing Id.
    /// </summary>
    [JsonPropertyName("listing_id")]
    public long? ListingId { get; set; }
    /// <summary>
    /// Gets or sets the Height.
    /// </summary>
    [JsonPropertyName("height")]
    public int? Height { get; set; }
    /// <summary>
    /// Gets or sets the Width.
    /// </summary>
    [JsonPropertyName("width")]
    public int? Width { get; set; }
    /// <summary>
    /// Gets or sets the Thumbnail Url.
    /// </summary>
    [JsonPropertyName("thumbnail_url")]
    public string? ThumbnailUrl { get; set; }
    /// <summary>
    /// Gets or sets the Video Url.
    /// </summary>
    [JsonPropertyName("video_url")]
    public string? VideoUrl { get; set; }
    /// <summary>
    /// Gets or sets the Video State.
    /// </summary>
    [JsonPropertyName("video_state")]
    public string? VideoState { get; set; }
}
