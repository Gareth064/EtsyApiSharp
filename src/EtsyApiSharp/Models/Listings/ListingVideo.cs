using System.Text.Json.Serialization;

namespace EtsyApiSharp.Models;

public class ListingVideo
{
    [JsonPropertyName("video_id")]
    public long VideoId { get; set; }
    [JsonPropertyName("listing_id")]
    public long? ListingId { get; set; }
    [JsonPropertyName("height")]
    public int? Height { get; set; }
    [JsonPropertyName("width")]
    public int? Width { get; set; }
    [JsonPropertyName("thumbnail_url")]
    public string? ThumbnailUrl { get; set; }
    [JsonPropertyName("video_url")]
    public string? VideoUrl { get; set; }
    [JsonPropertyName("video_state")]
    public string? VideoState { get; set; }
}
