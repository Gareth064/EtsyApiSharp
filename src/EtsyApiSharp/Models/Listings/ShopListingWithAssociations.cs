using System.Text.Json.Serialization;


namespace EtsyApiSharp.Models
{
    //A listing from a shop, which contains a product quantity, title, description, price, etc. and additional fields which represent associations.
    public class ShopListingWithAssociations
    {
        [JsonPropertyName("listing_id")]
        public int ListingId { get; set; }


        [JsonPropertyName("user_id")]
        public int UserId { get; set; }


        [JsonPropertyName("shop_id")]
        public int ShopId { get; set; }


        [JsonPropertyName("title")]
        public string Title { get; set; }


        [JsonPropertyName("description")]
        public string Description { get; set; }


        [JsonPropertyName("state")]
        public string State { get; set; }


        [JsonPropertyName("creation_timestamp")]
        public int CreationTimestamp { get; set; }


        [JsonPropertyName("ending_timestamp")]
        public int EndingTimestamp { get; set; }


        [JsonPropertyName("original_creation_timestamp")]
        public int OriginalCreationTimestamp { get; set; }


        [JsonPropertyName("last_modified_timestamp")]
        public int LastModifiedTimestamp { get; set; }


        [JsonPropertyName("state_timestamp")]
        public int StateTimestamp { get; set; }


        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }


        [JsonPropertyName("shop_section_id")]
        public int ShopSectionId { get; set; }


        [JsonPropertyName("featured_rank")]
        public int FeaturedRank { get; set; }


        [JsonPropertyName("url")]
        public string Url { get; set; }


        [JsonPropertyName("num_favorers")]
        public int NumFavorers { get; set; }


        [JsonPropertyName("non_taxable")]
        public bool NonTaxable { get; set; }


        [JsonPropertyName("is_customizable")]
        public bool IsCustomizable { get; set; }


        [JsonPropertyName("is_personalizable")]
        public bool IsPersonalizable { get; set; }


        [JsonPropertyName("personalization_is_required")]
        public bool PersonalizationIsRequired { get; set; }


        [JsonPropertyName("personalization_char_count_max")]
        public int PersonalizationCharCountMax { get; set; }


        [JsonPropertyName("personalization_instructions")]
        public string PersonalizationInstructions { get; set; }


        [JsonPropertyName("listing_type")]
        public bool ListingType { get; set; }


        [JsonPropertyName("tags")]
        public string[] Tags { get; set; }


        [JsonPropertyName("materials")]
        public string[] Materials { get; set; }


        [JsonPropertyName("shipping_profile_id")]
        public int ShippingProfileId { get; set; }


        [JsonPropertyName("processing_min")]
        public int ProcessingMin { get; set; }


        [JsonPropertyName("processing_max")]
        public int ProcessingMax { get; set; }


        [JsonPropertyName("who_made")]
        public string WhoMade { get; set; }


        [JsonPropertyName("when_made")]
        public string WhenMade { get; set; }


        [JsonPropertyName("is_supply")]
        public bool IsSupply { get; set; }


        [JsonPropertyName("item_weight")]
        public float ItemWeight { get; set; }


        [JsonPropertyName("item_weight_unit")]
        public string ItemWeightUnit { get; set; }


        [JsonPropertyName("item_length")]
        public float ItemLength { get; set; }


        [JsonPropertyName("item_width")]
        public float ItemWidth { get; set; }


        [JsonPropertyName("item_height")]
        public float ItemHeight { get; set; }


        [JsonPropertyName("item_dimensions_unit")]
        public string ItemDimensionsUnit { get; set; }


        [JsonPropertyName("is_private")]
        public bool IsPrivate { get; set; }


        [JsonPropertyName("style")]
        public string[] Style { get; set; }


        [JsonPropertyName("file_data")]
        public string FileData { get; set; }


        [JsonPropertyName("has_variations")]
        public bool HasVariations { get; set; }


        [JsonPropertyName("should_auto_renew")]
        public bool ShouldAutoRenew { get; set; }


        [JsonPropertyName("language")]
        public string Language { get; set; }


        [JsonPropertyName("price")]
        public Money Price { get; set; }


        [JsonPropertyName("taxonomy_id")]
        public int TaxonomyId { get; set; }


        [JsonPropertyName("user")]
        public User User { get; set; }


        [JsonPropertyName("shop")]
        public Shop Shop { get; set; }


        [JsonPropertyName("images")]
        public List<ListingImage>Images { get; set; }


        [JsonPropertyName("production_partners")]
        public List<ShopProductionPartner> ProductionPartners { get; set; }


    }
}
