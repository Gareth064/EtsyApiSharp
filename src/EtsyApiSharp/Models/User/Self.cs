using System.Text.Json.Serialization;

namespace EtsyApiSharp.Models;

/// <summary>
/// Represents basic information for the user making an authenticated request.
/// </summary>
public class Self
{
    [JsonPropertyName("user_id")]
    public long UserId { get; set; }

    [JsonPropertyName("shop_id")]
    public long ShopId { get; set; }
}
