using EtsyApiSharp.Models.Listings.Enums;

namespace EtsyApiSharp.Models;

public class CreateDraftListingRequest
{
    public long Quantity { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public ListingWhoMade WhoMade { get; set; }
    public string WhenMade { get; set; } = string.Empty;
    public long TaxonomyId { get; set; }
    public long? ShippingProfileId { get; set; }
    public long? ReturnPolicyId { get; set; }
    public IReadOnlyCollection<string>? Materials { get; set; }
    public long? ShopSectionId { get; set; }
    public int? ProcessingMin { get; set; }
    public int? ProcessingMax { get; set; }
    public long? ReadinessStateId { get; set; }
    public IReadOnlyCollection<string>? Tags { get; set; }
    public IReadOnlyCollection<string>? Styles { get; set; }
    public decimal? ItemWeight { get; set; }
    public decimal? ItemLength { get; set; }
    public decimal? ItemWidth { get; set; }
    public decimal? ItemHeight { get; set; }
    public string? ItemWeightUnit { get; set; }
    public string? ItemDimensionsUnit { get; set; }
    public bool? IsPersonalizable { get; set; }
    public bool? PersonalizationIsRequired { get; set; }
    public int? PersonalizationCharCountMax { get; set; }
    public string? PersonalizationInstructions { get; set; }
    public IReadOnlyCollection<long>? ProductionPartnerIds { get; set; }
    public IReadOnlyCollection<long>? ImageIds { get; set; }
    public bool? IsSupply { get; set; }
    public bool? IsCustomizable { get; set; }
    public bool? ShouldAutoRenew { get; set; }
    public bool? IsTaxable { get; set; }
    public ListingType? Type { get; set; }
}
