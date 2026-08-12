using EtsyApiSharp.Models.ShopShippings.Enums;

namespace EtsyApiSharp.Models;

/// <summary>Form fields used to update a shipping destination.</summary>
public sealed class UpdateShopShippingProfileDestinationRequest
{
    public float? PrimaryCost { get; set; }
    public float? SecondaryCost { get; set; }
    public string? DestinationCountryIso { get; set; }
    public ShippingDestinationRegion? DestinationRegion { get; set; }
    public long? ShippingCarrierId { get; set; }
    public string? MailClass { get; set; }
    public long? MinDeliveryDays { get; set; }
    public long? MaxDeliveryDays { get; set; }
}
