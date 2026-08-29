using System.Text.Json.Serialization;

namespace EtsyApiSharp.Models;
/// <summary>
/// Represents Update Shop Receipt Request.
/// </summary>

public class UpdateShopReceiptRequest
{
    /// <summary>
    /// Gets or sets the Was Shipped.
    /// </summary>
    [JsonPropertyName("was_shipped")]
    public bool? WasShipped { get; set; }
    /// <summary>
    /// Gets or sets the Was Paid.
    /// </summary>

    [JsonPropertyName("was_paid")]
    public bool? WasPaid { get; set; }
}
