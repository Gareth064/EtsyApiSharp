using System.Text.Json.Serialization;

namespace EtsyApiSharp.Models;

/// <summary>
/// Represents a shipping destination assigned to a shipping profile.
/// </summary>
public class ShopShippingProfileDestination
{
    /// <summary>
    /// The numeric ID of the shipping profile destination in the [shipping profile](/documentation/reference#tag/ShopListing-ShippingProfile) associated with the listing.
    /// </summary>
    [JsonPropertyName("shipping_profile_destination_id")]
    public long ShippingProfileDestinationId { get; set; }

    /// <summary>
    /// The numeric ID of the shipping profile.
    /// </summary>
    [JsonPropertyName("shipping_profile_id")]
    public long ShippingProfileId { get; set; }

    /// <summary>
    /// The ISO code of the country from which the listing ships.
    /// </summary>
    [JsonPropertyName("origin_country_iso")]
    public string OriginCountryIso { get; set; }

    /// <summary>
    /// The ISO code of the country to which the listing ships. If null, request sets destination to destination_region
    /// </summary>
    [JsonPropertyName("destination_country_iso")]
    public string DestinationCountryIso { get; set; }

    /// <summary>
    /// The code of the region to which the listing ships. A region represents a set of countries. Supported regions are Europe Union and Non-Europe Union (countries in Europe not in EU). If \`none\", request sets destination to destination_country_iso, or \"everywhere\" if destination_country_iso is also null
    /// </summary>
    [JsonPropertyName("destination_region")]
    public string DestinationRegion { get; set; }

    /// <summary>
    /// The cost of shipping to this country/region alone, measured in the store's default currency.
    /// </summary>
    [JsonPropertyName("primary_cost")]
    public Money PrimaryCost { get; set; }

    /// <summary>
    /// The cost of shipping to this country/region with another item, measured in the store's default currency.
    /// </summary>
    [JsonPropertyName("secondary_cost")]
    public Money SecondaryCost { get; set; }

    /// <summary>
    /// The unique ID of a supported shipping carrier, which is used to calculate an Estimated Delivery Date.
    /// </summary>
    [JsonPropertyName("shipping_carrier_id")]
    public long? ShippingCarrierId { get; set; }

    /// <summary>
    /// The unique ID string of a shipping carrier's mail class, which is used to calculate an estimated delivery date.
    /// </summary>
    [JsonPropertyName("mail_class")]
    public string MailClass { get; set; }

    /// <summary>
    /// The minimum number of business days a buyer can expect to wait to receive their purchased item once it has shipped.
    /// </summary>
    [JsonPropertyName("min_delivery_days")]
    public int? MinDeliveryDays { get; set; }

    /// <summary>
    /// The maximum number of business days a buyer can expect to wait to receive their purchased item once it has shipped.
    /// </summary>
    [JsonPropertyName("max_delivery_days")]
    public int? MaxDeliveryDays { get; set; }

}
