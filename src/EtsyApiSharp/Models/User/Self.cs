using System.Text.Json.Serialization;

namespace EtsyApiSharp.Models;
/// <summary>
/// Represents Self.
/// </summary>

public class Self
{
    /// <summary>
    /// Gets or sets the User Id.
    /// </summary>
    [JsonPropertyName("user_id")]
    public long UserId { get; set; }
    /// <summary>
    /// Gets or sets the Shop Id.
    /// </summary>

    [JsonPropertyName("shop_id")]
    public long ShopId { get; set; }
}
