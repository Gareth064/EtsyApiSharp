using EtsyApiSharp.Models.ShopShippings.Enums;

namespace EtsyApiSharp.Models;

/// <summary>Form fields used to update a shipping profile upgrade.</summary>
public sealed class UpdateShopShippingProfileUpgradeRequest
{
    public string? UpgradeName { get; set; }
    public ShippingProfileUpgradeType? Type { get; set; }
    public float? Price { get; set; }
    public float? SecondaryPrice { get; set; }
    public long? ShippingCarrierId { get; set; }
    public string? MailClass { get; set; }
    public long? MinDeliveryDays { get; set; }
    public long? MaxDeliveryDays { get; set; }
}
