using Newtonsoft.Json;

namespace EtsyApiSharp.Models.CreateDraftListings
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class Price
    {
        [JsonProperty("amount")]
        public int Amount { get; set; }

        [JsonProperty("divisor")]
        public int Divisor { get; set; }

        [JsonProperty("currency_code")]
        public string CurrencyCode { get; set; }
    }

    public class CreateDraftListingResponce
    {
        [JsonProperty("listing_id")]
        public int ListingId { get; set; }

        [JsonProperty("user_id")]
        public int UserId { get; set; }

        [JsonProperty("shop_id")]
        public int ShopId { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("creation_timestamp")]
        public int CreationTimestamp { get; set; }

        [JsonProperty("ending_timestamp")]
        public int EndingTimestamp { get; set; }

        [JsonProperty("original_creation_timestamp")]
        public int OriginalCreationTimestamp { get; set; }

        [JsonProperty("last_modified_timestamp")]
        public int LastModifiedTimestamp { get; set; }

        [JsonProperty("state_timestamp")]
        public int StateTimestamp { get; set; }

        [JsonProperty("quantity")]
        public int Quantity { get; set; }

        [JsonProperty("shop_section_id")]
        public int ShopSectionId { get; set; }

        [JsonProperty("featured_rank")]
        public int FeaturedRank { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("num_favorers")]
        public int NumFavorers { get; set; }

        [JsonProperty("non_taxable")]
        public bool NonTaxable { get; set; }

        [JsonProperty("is_customizable")]
        public bool IsCustomizable { get; set; }

        [JsonProperty("is_personalizable")]
        public bool IsPersonalizable { get; set; }

        [JsonProperty("personalization_is_required")]
        public bool PersonalizationIsRequired { get; set; }

        [JsonProperty("personalization_char_count_max")]
        public int PersonalizationCharCountMax { get; set; }

        [JsonProperty("personalization_instructions")]
        public string PersonalizationInstructions { get; set; }

        [JsonProperty("listing_type")]
        public bool ListingType { get; set; }

        [JsonProperty("tags")]
        public List<string> Tags { get; set; }

        [JsonProperty("materials")]
        public List<string> Materials { get; set; }

        [JsonProperty("shipping_profile_id")]
        public int ShippingProfileId { get; set; }

        [JsonProperty("processing_min")]
        public int ProcessingMin { get; set; }

        [JsonProperty("processing_max")]
        public int ProcessingMax { get; set; }

        [JsonProperty("who_made")]
        public string WhoMade { get; set; }

        [JsonProperty("when_made")]
        public string WhenMade { get; set; }

        [JsonProperty("is_supply")]
        public bool IsSupply { get; set; }

        [JsonProperty("item_weight")]
        public int ItemWeight { get; set; }

        [JsonProperty("item_weight_unit")]
        public string ItemWeightUnit { get; set; }

        [JsonProperty("item_length")]
        public int ItemLength { get; set; }

        [JsonProperty("item_width")]
        public int ItemWidth { get; set; }

        [JsonProperty("item_height")]
        public int ItemHeight { get; set; }

        [JsonProperty("item_dimensions_unit")]
        public string ItemDimensionsUnit { get; set; }

        [JsonProperty("is_private")]
        public bool IsPrivate { get; set; }

        [JsonProperty("style")]
        public List<string> Style { get; set; }

        [JsonProperty("file_data")]
        public string FileData { get; set; }

        [JsonProperty("has_variations")]
        public bool HasVariations { get; set; }

        [JsonProperty("should_auto_renew")]
        public bool ShouldAutoRenew { get; set; }

        [JsonProperty("language")]
        public string Language { get; set; }

        [JsonProperty("price")]
        public Price Price { get; set; }

        [JsonProperty("taxonomy_id")]
        public int TaxonomyId { get; set; }
    }



}
