using EtsyApiSharp.Models.ShopShippings.Enums;
using System.Text.Json.Serialization;

namespace EtsyApiSharp.Models;

/// <summary>
/// A representation of a shipping profile upgrade option.
/// </summary>
public class ShopShippingProfileUpgrade
{
    /// <summary>
    /// The numeric ID of the base shipping profile.
    /// </summary>
    [JsonPropertyName("shipping_profile_id")]
    public long ShippingProfileId { get; set; }

    /// <summary>
    /// The numeric ID that is associated with a shipping upgrade
    /// </summary>
    [JsonPropertyName("upgrade_id")]
    public long UpgradeId { get; set; }

    /// <summary>
    /// Name for the shipping upgrade shown to shoppers at checkout, e.g. USPS Priority.
    /// </summary>
    [JsonPropertyName("upgrade_name")]
    public string UpgradeName { get; set; }

    /// <summary>
    /// The type of the shipping upgrade. Domestic (0) or international (1).
    /// </summary>
    [JsonPropertyName("type")]
    public ShippingProfileUpgradeType Type { get; set; }

    /// <summary>
    /// The positive non-zero numeric position in the images displayed in a listing, with rank 1 images appearing in the left-most position in a listing.
    /// </summary>
    [JsonPropertyName("rank")]
    public int Rank { get; set; }

    /// <summary>
    /// The IETF language tag for the language of the shipping profile. Ex: de, en, es, fr, it, ja, nl, pl, pt, ru.
    /// </summary>
    [JsonPropertyName("language")]
    public string Language { get; set; }

    /// <summary>
    /// Additional cost of adding the shipping upgrade.
    /// </summary>
    [JsonPropertyName("price")]
    public Money Price { get; set; }

    /// <summary>
    /// Additional cost of adding the shipping upgrade for each additional item.
    /// </summary>
    [JsonPropertyName("secondary_price")]
    public Money SecondaryPrice { get; set; }

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
