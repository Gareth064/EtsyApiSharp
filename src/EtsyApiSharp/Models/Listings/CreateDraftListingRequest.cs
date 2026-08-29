using EtsyApiSharp.Models.Listings.Enums;

namespace EtsyApiSharp.Models;
/// <summary>
/// Represents Create Draft Listing Request.
/// </summary>

public class CreateDraftListingRequest
{
    /// <summary>
    /// Gets or sets the Quantity.
    /// </summary>
    public long Quantity { get; set; }
    /// <summary>
    /// Gets or sets the Title.
    /// </summary>
    public string Title { get; set; } = string.Empty;
    /// <summary>
    /// Gets or sets the Description.
    /// </summary>
    public string Description { get; set; } = string.Empty;
    /// <summary>
    /// Gets or sets the Price.
    /// </summary>
    public decimal Price { get; set; }
    /// <summary>
    /// Gets or sets the Who Made.
    /// </summary>
    public ListingWhoMade WhoMade { get; set; }
    /// <summary>
    /// Gets or sets the When Made.
    /// </summary>
    public string WhenMade { get; set; } = string.Empty;
    /// <summary>
    /// Gets or sets the Taxonomy Id.
    /// </summary>
    public long TaxonomyId { get; set; }
    /// <summary>
    /// Gets or sets the Shipping Profile Id.
    /// </summary>
    public long? ShippingProfileId { get; set; }
    /// <summary>
    /// Gets or sets the Return Policy Id.
    /// </summary>
    public long? ReturnPolicyId { get; set; }
    /// <summary>
    /// Gets or sets the Materials.
    /// </summary>
    public IReadOnlyCollection<string>? Materials { get; set; }
    /// <summary>
    /// Gets or sets the Shop Section Id.
    /// </summary>
    public long? ShopSectionId { get; set; }
    /// <summary>
    /// Gets or sets the Processing Min.
    /// </summary>
    public int? ProcessingMin { get; set; }
    /// <summary>
    /// Gets or sets the Processing Max.
    /// </summary>
    public int? ProcessingMax { get; set; }
    /// <summary>
    /// Gets or sets the Readiness State Id.
    /// </summary>
    public long? ReadinessStateId { get; set; }
    /// <summary>
    /// Gets or sets the Tags.
    /// </summary>
    public IReadOnlyCollection<string>? Tags { get; set; }
    /// <summary>
    /// Gets or sets the Styles.
    /// </summary>
    public IReadOnlyCollection<string>? Styles { get; set; }
    /// <summary>
    /// Gets or sets the Item Weight.
    /// </summary>
    public decimal? ItemWeight { get; set; }
    /// <summary>
    /// Gets or sets the Item Length.
    /// </summary>
    public decimal? ItemLength { get; set; }
    /// <summary>
    /// Gets or sets the Item Width.
    /// </summary>
    public decimal? ItemWidth { get; set; }
    /// <summary>
    /// Gets or sets the Item Height.
    /// </summary>
    public decimal? ItemHeight { get; set; }
    /// <summary>
    /// Gets or sets the Item Weight Unit.
    /// </summary>
    public string? ItemWeightUnit { get; set; }
    /// <summary>
    /// Gets or sets the Item Dimensions Unit.
    /// </summary>
    public string? ItemDimensionsUnit { get; set; }
    /// <summary>
    /// Gets or sets the Is Personalizable.
    /// </summary>
    public bool? IsPersonalizable { get; set; }
    /// <summary>
    /// Gets or sets the Personalization Is Required.
    /// </summary>
    public bool? PersonalizationIsRequired { get; set; }
    /// <summary>
    /// Gets or sets the Personalization Char Count Max.
    /// </summary>
    public int? PersonalizationCharCountMax { get; set; }
    /// <summary>
    /// Gets or sets the Personalization Instructions.
    /// </summary>
    public string? PersonalizationInstructions { get; set; }
    /// <summary>
    /// Gets or sets the Production Partner Ids.
    /// </summary>
    public IReadOnlyCollection<long>? ProductionPartnerIds { get; set; }
    /// <summary>
    /// Gets or sets the Image Ids.
    /// </summary>
    public IReadOnlyCollection<long>? ImageIds { get; set; }
    /// <summary>
    /// Gets or sets the Is Supply.
    /// </summary>
    public bool? IsSupply { get; set; }
    /// <summary>
    /// Gets or sets the Is Customizable.
    /// </summary>
    public bool? IsCustomizable { get; set; }
    /// <summary>
    /// Gets or sets the Should Auto Renew.
    /// </summary>
    public bool? ShouldAutoRenew { get; set; }
    /// <summary>
    /// Gets or sets the Is Taxable.
    /// </summary>
    public bool? IsTaxable { get; set; }
    /// <summary>
    /// Gets or sets the Type.
    /// </summary>
    public ListingType? Type { get; set; }
}
