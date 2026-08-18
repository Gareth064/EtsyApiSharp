using System.Text.Json.Serialization;

namespace EtsyApiSharp.Models;

/// <summary>
/// Values that may be changed for an Etsy shop.
/// </summary>
public class UpdateShopRequest
{
    /// <summary>
    /// Gets or sets the Title.
    /// </summary>
    [JsonPropertyName("title")]
    public string? Title { get; set; }
    /// <summary>
    /// Gets or sets the Announcement.
    /// </summary>

    [JsonPropertyName("announcement")]
    public string? Announcement { get; set; }
    /// <summary>
    /// Gets or sets the Sale Message.
    /// </summary>

    [JsonPropertyName("sale_message")]
    public string? SaleMessage { get; set; }
    /// <summary>
    /// Gets or sets the Digital Sale Message.
    /// </summary>

    [JsonPropertyName("digital_sale_message")]
    public string? DigitalSaleMessage { get; set; }
    /// <summary>
    /// Gets or sets the Policy Additional.
    /// </summary>

    [JsonPropertyName("policy_additional")]
    public string? PolicyAdditional { get; set; }
}
