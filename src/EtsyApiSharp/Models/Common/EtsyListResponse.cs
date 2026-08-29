using System.Text.Json.Serialization;

namespace EtsyApiSharp.Models.Common;
/// <summary>
/// Represents Etsy List Response.
/// </summary>

public class EtsyListResponse<T>
{
    /// <summary>
    /// Gets or sets the Count.
    /// </summary>
    [JsonPropertyName("count")]
    public long Count { get; set; }
    /// <summary>
    /// Gets or sets the Results.
    /// </summary>

    [JsonPropertyName("results")]
    public List<T> Results { get; set; } = [];
}
