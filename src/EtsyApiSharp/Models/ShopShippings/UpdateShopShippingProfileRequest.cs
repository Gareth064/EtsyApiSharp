using EtsyApiSharp.Models.ShopShippings.Enums;

namespace EtsyApiSharp.Models;

/// <summary>Form fields used to update a shop shipping profile.</summary>
public sealed class UpdateShopShippingProfileRequest
{
    public string? Title { get; set; }
    public string? OriginCountryIso { get; set; }
    public long? MinProcessingTime { get; set; }
    public long? MaxProcessingTime { get; set; }
    public ShippingProcessingTimeUnit? ProcessingTimeUnit { get; set; }
    public string? OriginPostalCode { get; set; }
}
