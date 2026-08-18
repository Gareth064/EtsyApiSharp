using EtsyApiSharp.Models.ShopShippings.Enums;

namespace EtsyApiSharp.Models;

/// <summary>Form fields used to update a shipping profile upgrade.</summary>
public sealed class UpdateShopShippingProfileUpgradeRequest
{
    /// <summary>
    /// Gets or sets the Upgrade Name.
    /// </summary>
    public string? UpgradeName { get; set; }
    /// <summary>
    /// Gets or sets the Type.
    /// </summary>
    public ShippingProfileUpgradeType? Type { get; set; }
    /// <summary>
    /// Gets or sets the Price.
    /// </summary>
    public float? Price { get; set; }
    /// <summary>
    /// Gets or sets the Secondary Price.
    /// </summary>
    public float? SecondaryPrice { get; set; }
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
