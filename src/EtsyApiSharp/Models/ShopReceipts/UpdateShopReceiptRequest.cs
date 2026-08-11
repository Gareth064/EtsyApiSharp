using System.Text.Json.Serialization;

namespace EtsyApiSharp.Models;

public class UpdateShopReceiptRequest
{
    [JsonPropertyName("was_shipped")]
    public bool? WasShipped { get; set; }

    [JsonPropertyName("was_paid")]
    public bool? WasPaid { get; set; }
}
