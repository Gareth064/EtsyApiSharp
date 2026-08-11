using System.Text.Json.Serialization;

namespace EtsyApiSharp.Models;

public class UpdateListingInventoryRequest
{
    [JsonPropertyName("products")]
    public IReadOnlyCollection<ListingInventoryProduct> Products { get; set; } = Array.Empty<ListingInventoryProduct>();
    [JsonPropertyName("price_on_property")]
    public IReadOnlyCollection<long>? PriceOnProperty { get; set; }
    [JsonPropertyName("quantity_on_property")]
    public IReadOnlyCollection<long>? QuantityOnProperty { get; set; }
    [JsonPropertyName("sku_on_property")]
    public IReadOnlyCollection<long>? SkuOnProperty { get; set; }
    [JsonPropertyName("readiness_state_on_property")]
    public IReadOnlyCollection<long>? ReadinessStateOnProperty { get; set; }
}
