using EtsyApiSharp.Models.Listings.Enums;
using System.Text.Json.Serialization;

namespace EtsyApiSharp.Models
{
    /// <summary>
    /// A listing from a shop, which contains a product quantity, title, description, price, etc.
    /// </summary>
    public class ShopListing
    {
        /// <summary>
        /// The numeric ID for the [listing](/documentation/reference#tag/ShopListing) associated to this transaction.
        /// </summary>
        [JsonPropertyName("listing_id")]
        public long ListingId { get; set; }

        /// <summary>
        /// The numeric ID for the [user](/documentation/reference#tag/User) posting the listing.
        /// </summary>
        [JsonPropertyName("user_id")]
        public long UserId { get; set; }

        /// <summary>
        /// The unique positive non-zero numeric ID for an Etsy Shop.
        /// </summary>
        [JsonPropertyName("shop_id")]
        public long ShopId { get; set; }

        /// <summary>
        /// The listing's title string. Valid title strings contain only letters, numbers, punctuation marks, mathematical symbols, whitespace characters, ™, ©, and ®. (regex: /[^\\p{L}\\p{Nd}\\p{P}\\p{Sm}\\p{Zs}™©®]/u) You can only use the %, :, & and + characters once each.
        /// </summary>
        [JsonPropertyName("title")]
        public string Title { get; set; }

        /// <summary>
        /// A description string of the product for sale in the listing.
        /// </summary>
        [JsonPropertyName("description")]
        public string Description { get; set; }

        /// <summary>
        /// An enumerated string from any of: active or inactive. Note: Setting a draft listing to active will also publish the listing on etsy.com. Setting a sold out listing to active will update the quantity to 1 and renew the listing on etsy.com.
        /// </summary>
        [JsonPropertyName("state")]
        public ListingState State { get; set; }

        /// <summary>
        /// The listing\'s creation time, in epoch seconds.
        /// </summary>
        [JsonPropertyName("creation_timestamp")]
        public int CreationTimestamp { get; set; }

        /// <summary>
        /// The listing\'s expiration time, in epoch seconds.
        /// </summary>
        [JsonPropertyName("ending_timestamp")]
        public int EndingTimestamp { get; set; }

        /// <summary>
        /// The listing\'s creation time, in epoch seconds.
        /// </summary>
        [JsonPropertyName("original_creation_timestamp")]
        public int OriginalCreationTimestamp { get; set; }

        /// <summary>
        /// The time of the last update to the listing, in epoch seconds.
        /// </summary>
        [JsonPropertyName("last_modified_timestamp")]
        public int LastModifiedTimestamp { get; set; }

        /// <summary>
        /// The date and time of the last state change of this listing.
        /// </summary>
        [JsonPropertyName("state_timestamp")]
        public int StateTimestamp { get; set; }

        /// <summary>
        /// The positive non-zero number of products available for purchase in the listing. Note: The listing quantity is the sum of available offering quantities. You can request the quantities for individual offerings from the ListingInventory resource using the [getListingInventory](/documentation/reference#operation/getListingInventory) endpoint.
        /// </summary>
        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }

        /// <summary>
        /// The numeric ID of a section in a specific Etsy shop.
        /// </summary>
        [JsonPropertyName("shop_section_id")]
        public long? ShopSectionId { get; set; }

        /// <summary>
        /// The positive non-zero numeric position in the featured listings of the shop, with rank 1 listings appearing in the left-most position in featured listing on a shop’s home page.
        /// </summary>
        [JsonPropertyName("featured_rank")]
        public int FeaturedRank { get; set; }

        /// <summary>
        /// The full URL to the listing's page on Etsy.
        /// </summary>
        [JsonPropertyName("url")]
        public string Url { get; set; }

        /// <summary>
        /// The number of users who marked this Listing a favorite.
        /// </summary>
        [JsonPropertyName("num_favorers")]
        public int NumFavorers { get; set; }

        /// <summary>
        /// When true, applicable [shop](/documentation/reference#tag/Shop) tax rates do not apply to this listing at checkout.
        /// </summary>
        [JsonPropertyName("non_taxable")]
        public bool NonTaxable { get; set; }

        /// <summary>
        /// When true, a buyer may contact the seller for a customized order. The default value is true when a shop accepts custom orders. Does not apply to shops that do not accept custom orders.
        /// </summary>
        [JsonPropertyName("is_customizable")]
        public bool IsCustomizable { get; set; }

        /// <summary>
        /// When true, this listing is personalizable. The default value is null.
        /// </summary>
        [JsonPropertyName("is_personalizable")]
        public bool IsPersonalizable { get; set; }

        /// <summary>
        /// When true, this listing requires personalization. The default value is null.
        /// </summary>
        [JsonPropertyName("personalization_is_required")]
        public bool PersonalizationIsRequired { get; set; }

        /// <summary>
        /// This an integer value representing the maximum length for the personalization message entered by the buyer.
        /// </summary>
        [JsonPropertyName("personalization_char_count_max")]
        public int? PersonalizationCharCountMax { get; set; }

        /// <summary>
        /// When true, this listing requires personalization. The default value is null.
        /// </summary>
        [JsonPropertyName("personalization_instructions")]
        public string PersonalizationInstructions { get; set; }

        /// <summary>
        /// An enumerated type string that indicates whether the listing is physical or a digital download.
        /// </summary>
        [JsonPropertyName("listing_type")]
        public bool ListingType { get; set; }

        /// <summary>
        /// A list of tag strings for the listing. Valid tag strings contain only letters, numbers, whitespace characters, -, ', ™, ©, and ®. (regex: /[^\\p{L}\\p{Nd}\\p{Zs}\\-'™©®]/u) Default value is null.
        /// </summary>
        [JsonPropertyName("tags")]
        public List<string> Tags { get; set; }

        /// <summary>
        /// A list of material strings for materials used in the product. Valid materials strings contain only letters, numbers, and whitespace characters. (regex: /[^\\p{L}\\p{Nd}\\p{Zs}]/u) Default value is null.
        /// </summary>
        [JsonPropertyName("materials")]
        public List<string> Materials { get; set; }

        /// <summary>
        /// The numeric ID of the [shipping profile](/documentation/reference#tag/ShopListing-ShippingProfile) associated with the listing. Required when listing type=physical.
        /// </summary>
        [JsonPropertyName("shipping_profile_id")]
        public long? ShippingProfileId { get; set; }

        /// <summary>
        /// The minimum number of days required to process this listing. Default value is null.
        /// </summary>
        [JsonPropertyName("processing_min")]
        public int? ProcessingMin { get; set; }

        /// <summary>
        /// The maximum number of days required to process this listing. Default value is null.
        /// </summary>
        [JsonPropertyName("processing_max")]
        public int? ProcessingMax { get; set; }

        /// <summary>
        /// An enumerated string inidcated who made the product. Helps buyers locate the listing under the Handmade heading. Requires 'is_supply' and 'when_made'.
        /// </summary>
        [JsonPropertyName("who_made")]
        public string WhoMade { get; set; }

        /// <summary>
        /// An enumerated string for the era in which the maker made the product in this listing. Helps buyers locate the listing under the Vintage heading. Requires 'is_supply' and 'who_made'.
        /// </summary>
        [JsonPropertyName("when_made")]
        public string WhenMade { get; set; }

        /// <summary>
        /// When true, tags the listing as a supply product, else indicates that it's a finished product. Helps buyers locate the listing under the Supplies heading. Requires 'who_made' and 'when_made'.
        /// </summary>
        [JsonPropertyName("is_supply")]
        public bool? IsSupply { get; set; }

        /// <summary>
        /// The numeric weight of the product measured in units set in \'item_weight_unit\'. Default value is null. If set, the value must be greater than 0.
        /// </summary>
        [JsonPropertyName("item_weight")]
        public float? ItemWeight { get; set; }

        /// <summary>
        /// A string defining the units used to measure the weight of the product. Default value is null.
        /// </summary>
        [JsonPropertyName("item_weight_unit")]
        public string ItemWeightUnit { get; set; }

        /// <summary>
        /// The numeric length of the product measured in units set in \'item_dimensions_unit\'. Default value is null. If set, the value must be greater than 0.
        /// </summary>
        [JsonPropertyName("item_length")]
        public float? ItemLength { get; set; }

        /// <summary>
        /// The numeric width of the product measured in units set in \'item_dimensions_unit\'. Default value is null. If set, the value must be greater than 0.
        /// </summary>
        [JsonPropertyName("item_width")]
        public float? ItemWidth { get; set; }

        /// <summary>
        /// The numeric length of the product measured in units set in \'item_dimensions_unit\'. Default value is null. If set, the value must be greater than 0.
        /// </summary>
        [JsonPropertyName("item_height")]
        public float? ItemHeight { get; set; }

        /// <summary>
        /// A string defining the units used to measure the dimensions of the product. Default value is null.
        /// </summary>
        [JsonPropertyName("item_dimensions_unit")]
        public string ItemDimensionsUnit { get; set; }

        /// <summary>
        /// When true, this is a private listing intendend for a specific buyer and hidden from shop view.
        /// </summary>
        [JsonPropertyName("is_private")]
        public bool IsPrivate { get; set; }

        /// <summary>
        /// An array of style strings for this listing, each of which is free-form text string such as \"Formal\", or \"Steampunk\". A Listing may have up to two styles. Valid style strings contain only letters, numbers, and whitespace characters. (regex: /[^\\p{L}\\p{Nd}\\p{Zs}]/u) Default value is null.
        /// </summary>
        [JsonPropertyName("style")]
        public List<string> Style { get; set; }

        /// <summary>
        /// A string describing the files attached to a digital listing.
        /// </summary>
        [JsonPropertyName("file_data")]
        public string FileData { get; set; }

        /// <summary>
        /// When true, the listing has variations.
        /// </summary>
        [JsonPropertyName("has_variations")]
        public bool HasVariations { get; set; }

        /// <summary>
        /// When true, renews a listing for four months upon expriation. If set to true when previously false, etsy.com renews the listing before it expires, but the renewal period starts immidiately rather than extending the listing's expiration date. Any unused time remaining on the listing is lost. Renewals result in charges to a user's bill.
        /// </summary>
        [JsonPropertyName("should_auto_renew")]
        public bool ShouldAutoRenew { get; set; }

        /// <summary>
        /// The IETF language tag for the default language of the listing. Ex: `de`, `en`, `es`, `fr`, `it`, `ja`, `nl`, `pl`, `pt`, `ru`.
        /// </summary>
        [JsonPropertyName("language")]
        public string Language { get; set; }

        /// <summary>
        /// The positive non-zero price of the product. (Sold product listings are private) Note: The price is the minimum possible price. The getInventory method requests exact prices for available offerings.
        /// </summary>
        [JsonPropertyName("price")]
        public Money Price { get; set; }

        /// <summary>
        /// The numeric taxonomy ID of the listing. The seller manages listing taxonomy IDs for their shop.  [See SellerTaxonomy](/documentation/reference#tag/SellerTaxonomy) for more information.
        /// </summary>
        [JsonPropertyName("taxonomy_id")]
        public long? TaxonomyId { get; set; }

    }
}
