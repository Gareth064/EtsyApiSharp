using EtsyApiSharp.Models.ShopShippings.Enums;

namespace EtsyApiSharp.Models;

/// <summary>Form fields used to update a shop shipping profile.</summary>
public sealed class UpdateShopShippingProfileRequest
{
    /// <summary>
    /// Gets or sets the Title.
    /// </summary>
    public string? Title { get; set; }
    /// <summary>
    /// Gets or sets the Origin Country Iso.
    /// </summary>
    public string? OriginCountryIso { get; set; }
    /// <summary>
    /// Gets or sets the Min Processing Time.
    /// </summary>
    public long? MinProcessingTime { get; set; }
    /// <summary>
    /// Gets or sets the Max Processing Time.
    /// </summary>
    public long? MaxProcessingTime { get; set; }
    /// <summary>
    /// Gets or sets the Processing Time Unit.
    /// </summary>
    public ShippingProcessingTimeUnit? ProcessingTimeUnit { get; set; }
    /// <summary>
    /// Gets or sets the Origin Postal Code.
    /// </summary>
    public string? OriginPostalCode { get; set; }
}
