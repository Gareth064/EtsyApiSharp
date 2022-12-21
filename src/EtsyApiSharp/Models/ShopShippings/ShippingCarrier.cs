using System.Text.Json.Serialization;

namespace EtsyApiSharp.Models;

/// <summary>
/// A supported shipping carrier, which is used to calculate an Estimated Delivery Date.
/// </summary>
public class ShippingCarrier
{
    /// <summary>
    /// The numeric ID of this shipping carrier.
    /// </summary>
    [JsonPropertyName("shipping_carrier_id")]
    public long ShippingCarrierId { get; set; }

    /// <summary>
    /// The name of this shipping carrier.
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; }

    /// <summary>
    /// Set of domestic mail classes of this shipping carrier.
    /// </summary>
    [JsonPropertyName("domestic_classes")]
    public List<ShippingCarrierMailClass> DomesticClasses { get; set; }

    /// <summary>
    /// Set of international mail classes of this shipping carrier.
    /// </summary>
    [JsonPropertyName("international_classes")]
    public List<ShippingCarrierMailClass> InternationalClasses { get; set; }

}
