using System.Text.Json.Serialization;


namespace EtsyApiSharp.Models
{
    //Represents a profile used to set a listing's shipping information. Please note that it's not possible to create calculated shipping templates via the API. However, you can associate calculated shipping profiles created from Shop Manager with listings using the API.
    public class ShopShippingProfile
    {
        [JsonPropertyName("shipping_profile_id")]
        public int ShippingProfileId { get; set; }


        [JsonPropertyName("title")]
        public string Title { get; set; }


        [JsonPropertyName("user_id")]
        public int UserId { get; set; }


        [JsonPropertyName("min_processing_days")]
        public int MinProcessingDays { get; set; }


        [JsonPropertyName("max_processing_days")]
        public int MaxProcessingDays { get; set; }


        [JsonPropertyName("processing_days_display_label")]
        public string ProcessingDaysDisplayLabel { get; set; }


        [JsonPropertyName("origin_country_iso")]
        public string OriginCountryIso { get; set; }


        [JsonPropertyName("is_deleted")]
        public bool IsDeleted { get; set; }


        [JsonPropertyName("shipping_profile_destinations")]
        public List<ShopShippingProfileDestination> ShippingProfileDestinations { get; set; }


        [JsonPropertyName("shipping_profile_upgrades")]
        public List<ShopShippingProfileUpgrade> ShippingProfileUpgrades { get; set; }


        [JsonPropertyName("origin_postal_code")]
        public string OriginPostalCode { get; set; }


        [JsonPropertyName("profile_type")]
        public string ProfileType { get; set; }


    }
}
