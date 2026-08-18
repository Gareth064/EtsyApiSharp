using EtsyApiSharp.Models.ShopShippings.Enums;

namespace EtsyApiSharp.Models;

/// <summary>Form fields used to create a shipping destination.</summary>
public sealed class CreateShopShippingProfileDestinationRequest
{
    /// <summary>
    /// Gets or sets the Primary Cost.
    /// </summary>
    public float PrimaryCost { get; set; }
    /// <summary>
    /// Gets or sets the Secondary Cost.
    /// </summary>
    public float SecondaryCost { get; set; }
    /// <summary>
    /// Gets or sets the Destination Country Iso.
    /// </summary>
    public string? DestinationCountryIso { get; set; }
    /// <summary>
    /// Gets or sets the Destination Region.
    /// </summary>
    public ShippingDestinationRegion? DestinationRegion { get; set; }
    /// <summary>
    /// Gets or sets the Shipping Carrier Id.
    /// </summary>
    public long? ShippingCarrierId { get; set; }
    /// <summary>
    /// Gets or sets the Mail Class.
    /// </summary>
    public string? MailClass { get; set; }
    /// <summary>
    /// Gets or sets the Min Delivery Days.
    /// </summary>
    public long? MinDeliveryDays { get; set; }
    /// <summary>
    /// Gets or sets the Max Delivery Days.
    /// </summary>
    public long? MaxDeliveryDays { get; set; }
}
