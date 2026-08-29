using EtsyApiSharp.Models.Listings.Enums;

namespace EtsyApiSharp.Models;
/// <summary>
/// Represents Update Listing Request.
/// </summary>

public class UpdateListingRequest
{
    /// <summary>
    /// Gets or sets the Image Ids.
    /// </summary>
    public IReadOnlyCollection<long>? ImageIds { get; set; }
    /// <summary>
    /// Gets or sets the Title.
    /// </summary>
    public string? Title { get; set; }
    /// <summary>
    /// Gets or sets the Description.
    /// </summary>
    public string? Description { get; set; }
    /// <summary>
    /// Gets or sets the Materials.
    /// </summary>
    public IReadOnlyCollection<string>? Materials { get; set; }
    /// <summary>
    /// Gets or sets the Should Auto Renew.
    /// </summary>
    public bool? ShouldAutoRenew { get; set; }
    /// <summary>
    /// Gets or sets the Shipping Profile Id.
    /// </summary>
    public long? ShippingProfileId { get; set; }
    /// <summary>
    /// Gets or sets the Return Policy Id.
    /// </summary>
    public long? ReturnPolicyId { get; set; }
    /// <summary>
    /// Gets or sets the Shop Section Id.
    /// </summary>
    public long? ShopSectionId { get; set; }
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
    /// Gets or sets the Is Taxable.
    /// </summary>
    public bool? IsTaxable { get; set; }
    /// <summary>
    /// Gets or sets the Taxonomy Id.
    /// </summary>
    public long? TaxonomyId { get; set; }
    /// <summary>
    /// Gets or sets the Tags.
    /// </summary>
    public IReadOnlyCollection<string>? Tags { get; set; }
    /// <summary>
    /// Gets or sets the Who Made.
    /// </summary>
    public ListingWhoMade? WhoMade { get; set; }
    /// <summary>
    /// Gets or sets the When Made.
    /// </summary>
    public string? WhenMade { get; set; }
    /// <summary>
    /// Gets or sets the Featured Rank.
    /// </summary>
    public int? FeaturedRank { get; set; }
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
    /// Gets or sets the State.
    /// </summary>
    public ListingState? State { get; set; }
    /// <summary>
    /// Gets or sets the Is Supply.
    /// </summary>
    public bool? IsSupply { get; set; }
    /// <summary>
    /// Gets or sets the Production Partner Ids.
    /// </summary>
    public IReadOnlyCollection<long>? ProductionPartnerIds { get; set; }
    /// <summary>
    /// Gets or sets the Type.
    /// </summary>
    public ListingType? Type { get; set; }
}
