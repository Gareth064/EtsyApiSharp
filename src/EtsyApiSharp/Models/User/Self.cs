using System.Text.Json.Serialization;

namespace EtsyApiSharp.Models;

/// <summary>
/// Identifies the Etsy user and shop associated with an OAuth access token.
/// </summary>
public class Self
{
    [JsonPropertyName("user_id")]
    public long UserId { get; set; }

    [JsonPropertyName("shop_id")]
    public long ShopId { get; set; }
}
