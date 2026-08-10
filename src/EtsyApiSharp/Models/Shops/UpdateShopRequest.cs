using System.Text.Json.Serialization;

namespace EtsyApiSharp.Models;

/// <summary>
/// Values that may be changed for an Etsy shop.
/// </summary>
public class UpdateShopRequest
{
    [JsonPropertyName("title")]
    public string? Title { get; set; }

    [JsonPropertyName("announcement")]
    public string? Announcement { get; set; }

    [JsonPropertyName("sale_message")]
    public string? SaleMessage { get; set; }

    [JsonPropertyName("digital_sale_message")]
    public string? DigitalSaleMessage { get; set; }

    [JsonPropertyName("policy_additional")]
    public string? PolicyAdditional { get; set; }
}
