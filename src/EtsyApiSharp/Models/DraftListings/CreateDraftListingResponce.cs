using System.Text.Json.Serialization;

namespace EtsyApiSharp.Models.DraftListings;
/// <summary>
/// Represents Create Draft Listing Responce.
/// </summary>

public class CreateDraftListingResponce
{
    /// <summary>
    /// Gets or sets the Listing Id.
    /// </summary>
    [JsonPropertyName("listing_id")]
    public int ListingId { get; set; }
    /// <summary>
    /// Gets or sets the User Id.
    /// </summary>

    [JsonPropertyName("user_id")]
    public int UserId { get; set; }
    /// <summary>
    /// Gets or sets the Shop Id.
    /// </summary>

    [JsonPropertyName("shop_id")]
    public int ShopId { get; set; }
    /// <summary>
    /// Gets or sets the Title.
    /// </summary>

    [JsonPropertyName("title")]
    public string Title { get; set; }
    /// <summary>
    /// Gets or sets the Description.
    /// </summary>

    [JsonPropertyName("description")]
    public string Description { get; set; }
    /// <summary>
    /// Gets or sets the State.
    /// </summary>

    [JsonPropertyName("state")]
    public string State { get; set; }
    /// <summary>
    /// Gets or sets the Creation Timestamp.
    /// </summary>

    [JsonPropertyName("creation_timestamp")]
    public int CreationTimestamp { get; set; }
    /// <summary>
    /// Gets or sets the Ending Timestamp.
    /// </summary>

    [JsonPropertyName("ending_timestamp")]
    public int EndingTimestamp { get; set; }
    /// <summary>
    /// Gets or sets the Original Creation Timestamp.
    /// </summary>

    [JsonPropertyName("original_creation_timestamp")]
    public int OriginalCreationTimestamp { get; set; }
    /// <summary>
    /// Gets or sets the Last Modified Timestamp.
    /// </summary>

    [JsonPropertyName("last_modified_timestamp")]
    public int LastModifiedTimestamp { get; set; }
    /// <summary>
    /// Gets or sets the State Timestamp.
    /// </summary>

    [JsonPropertyName("state_timestamp")]
    public int StateTimestamp { get; set; }
    /// <summary>
    /// Gets or sets the Quantity.
    /// </summary>

    [JsonPropertyName("quantity")]
    public int Quantity { get; set; }
    /// <summary>
    /// Gets or sets the Shop Section Id.
    /// </summary>

    [JsonPropertyName("shop_section_id")]
    public int ShopSectionId { get; set; }
    /// <summary>
    /// Gets or sets the Featured Rank.
    /// </summary>

    [JsonPropertyName("featured_rank")]
    public int FeaturedRank { get; set; }
    /// <summary>
    /// Gets or sets the Url.
    /// </summary>

    [JsonPropertyName("url")]
    public string Url { get; set; }
    /// <summary>
    /// Gets or sets the Num Favorers.
    /// </summary>

    [JsonPropertyName("num_favorers")]
    public int NumFavorers { get; set; }
    /// <summary>
    /// Gets or sets the Non Taxable.
    /// </summary>

    [JsonPropertyName("non_taxable")]
    public bool NonTaxable { get; set; }
    /// <summary>
    /// Gets or sets the Is Customizable.
    /// </summary>

    [JsonPropertyName("is_customizable")]
    public bool IsCustomizable { get; set; }
    /// <summary>
    /// Gets or sets the Is Personalizable.
    /// </summary>

    [JsonPropertyName("is_personalizable")]
    public bool IsPersonalizable { get; set; }
    /// <summary>
    /// Gets or sets the Personalization Is Required.
    /// </summary>

    [JsonPropertyName("personalization_is_required")]
    public bool PersonalizationIsRequired { get; set; }
    /// <summary>
    /// Gets or sets the Personalization Char Count Max.
    /// </summary>

    [JsonPropertyName("personalization_char_count_max")]
    public int PersonalizationCharCountMax { get; set; }
    /// <summary>
    /// Gets or sets the Personalization Instructions.
    /// </summary>

    [JsonPropertyName("personalization_instructions")]
    public string PersonalizationInstructions { get; set; }
    /// <summary>
    /// Gets or sets the Listing Type.
    /// </summary>

    [JsonPropertyName("listing_type")]
    public bool ListingType { get; set; }
    /// <summary>
    /// Gets or sets the Tags.
    /// </summary>

    [JsonPropertyName("tags")]
    public List<string> Tags { get; set; }
    /// <summary>
    /// Gets or sets the Materials.
    /// </summary>

    [JsonPropertyName("materials")]
    public List<string> Materials { get; set; }
    /// <summary>
    /// Gets or sets the Shipping Profile Id.
    /// </summary>

    [JsonPropertyName("shipping_profile_id")]
    public int ShippingProfileId { get; set; }
    /// <summary>
    /// Gets or sets the Processing Min.
    /// </summary>

    [JsonPropertyName("processing_min")]
    public int ProcessingMin { get; set; }
    /// <summary>
    /// Gets or sets the Processing Max.
    /// </summary>

    [JsonPropertyName("processing_max")]
    public int ProcessingMax { get; set; }
    /// <summary>
    /// Gets or sets the Who Made.
    /// </summary>

    [JsonPropertyName("who_made")]
    public string WhoMade { get; set; }
    /// <summary>
    /// Gets or sets the When Made.
    /// </summary>

    [JsonPropertyName("when_made")]
    public string WhenMade { get; set; }
    /// <summary>
    /// Gets or sets the Is Supply.
    /// </summary>

    [JsonPropertyName("is_supply")]
    public bool IsSupply { get; set; }
    /// <summary>
    /// Gets or sets the Item Weight.
    /// </summary>

    [JsonPropertyName("item_weight")]
    public int ItemWeight { get; set; }
    /// <summary>
    /// Gets or sets the Item Weight Unit.
    /// </summary>

    [JsonPropertyName("item_weight_unit")]
    public string ItemWeightUnit { get; set; }
    /// <summary>
    /// Gets or sets the Item Length.
    /// </summary>

    [JsonPropertyName("item_length")]
    public int ItemLength { get; set; }
    /// <summary>
    /// Gets or sets the Item Width.
    /// </summary>

    [JsonPropertyName("item_width")]
    public int ItemWidth { get; set; }
    /// <summary>
    /// Gets or sets the Item Height.
    /// </summary>

    [JsonPropertyName("item_height")]
    public int ItemHeight { get; set; }
    /// <summary>
    /// Gets or sets the Item Dimensions Unit.
    /// </summary>

    [JsonPropertyName("item_dimensions_unit")]
    public string ItemDimensionsUnit { get; set; }
    /// <summary>
    /// Gets or sets the Is Private.
    /// </summary>

    [JsonPropertyName("is_private")]
    public bool IsPrivate { get; set; }
    /// <summary>
    /// Gets or sets the Style.
    /// </summary>

    [JsonPropertyName("style")]
    public List<string> Style { get; set; }
    /// <summary>
    /// Gets or sets the File Data.
    /// </summary>

    [JsonPropertyName("file_data")]
    public string FileData { get; set; }
    /// <summary>
    /// Gets or sets the Has Variations.
    /// </summary>

    [JsonPropertyName("has_variations")]
    public bool HasVariations { get; set; }
    /// <summary>
    /// Gets or sets the Should Auto Renew.
    /// </summary>

    [JsonPropertyName("should_auto_renew")]
    public bool ShouldAutoRenew { get; set; }
    /// <summary>
    /// Gets or sets the Language.
    /// </summary>

    [JsonPropertyName("language")]
    public string Language { get; set; }
    /// <summary>
    /// Gets or sets the Price.
    /// </summary>

    [JsonPropertyName("price")]
    public Price Price { get; set; }
    /// <summary>
    /// Gets or sets the Taxonomy Id.
    /// </summary>

    [JsonPropertyName("taxonomy_id")]
    public int TaxonomyId { get; set; }
}
/// <summary>
/// Represents Price.
/// </summary>

public class Price
{
    /// <summary>
    /// Gets or sets the Amount.
    /// </summary>
    [JsonPropertyName("amount")]
    public int Amount { get; set; }
    /// <summary>
    /// Gets or sets the Divisor.
    /// </summary>

    [JsonPropertyName("divisor")]
    public int Divisor { get; set; }
    /// <summary>
    /// Gets or sets the Currency Code.
    /// </summary>

    [JsonPropertyName("currency_code")]
    public string CurrencyCode { get; set; }
}
