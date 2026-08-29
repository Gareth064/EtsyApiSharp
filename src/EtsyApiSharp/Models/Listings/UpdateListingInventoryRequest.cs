using System.Text.Json.Serialization;

namespace EtsyApiSharp.Models;
/// <summary>
/// Represents Update Listing Inventory Request.
/// </summary>

public class UpdateListingInventoryRequest
{
    /// <summary>
    /// Executes the Empty operation.
    /// </summary>
    [JsonPropertyName("products")]
    public IReadOnlyCollection<ListingInventoryProduct> Products { get; set; } = Array.Empty<ListingInventoryProduct>();
    /// <summary>
    /// Gets or sets the Price On Property.
    /// </summary>
    [JsonPropertyName("price_on_property")]
    public IReadOnlyCollection<long>? PriceOnProperty { get; set; }
    /// <summary>
    /// Gets or sets the Quantity On Property.
    /// </summary>
    [JsonPropertyName("quantity_on_property")]
    public IReadOnlyCollection<long>? QuantityOnProperty { get; set; }
    /// <summary>
    /// Gets or sets the Sku On Property.
    /// </summary>
    [JsonPropertyName("sku_on_property")]
    public IReadOnlyCollection<long>? SkuOnProperty { get; set; }
    /// <summary>
    /// Gets or sets the Readiness State On Property.
    /// </summary>
    [JsonPropertyName("readiness_state_on_property")]
    public IReadOnlyCollection<long>? ReadinessStateOnProperty { get; set; }
}
