using EtsyApiSharp.Models.ShopShippings.Enums;

namespace EtsyApiSharp.Models;

/// <summary>Form fields used to create a manual shop shipping profile.</summary>
public sealed class CreateShopShippingProfileRequest
{
    public string? Title { get; set; }
    public string? OriginCountryIso { get; set; }
    public float PrimaryCost { get; set; }
    public float SecondaryCost { get; set; }
    public long? MinProcessingTime { get; set; }
    public long? MaxProcessingTime { get; set; }
    public ShippingProcessingTimeUnit? ProcessingTimeUnit { get; set; }
    public string? DestinationCountryIso { get; set; }
    public ShippingDestinationRegion? DestinationRegion { get; set; }
    public string? OriginPostalCode { get; set; }
    public long? ShippingCarrierId { get; set; }
    public string? MailClass { get; set; }
    public long? MinDeliveryDays { get; set; }
    public long? MaxDeliveryDays { get; set; }
}
