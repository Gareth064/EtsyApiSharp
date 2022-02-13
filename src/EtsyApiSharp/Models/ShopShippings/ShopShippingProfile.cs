using EtsyApiSharp.Helpers.Converters;
using EtsyApiSharp.Models.ShopShippings.Enums;
using System.Text.Json.Serialization;

namespace EtsyApiSharp.Models
{
    /// <summary>
    /// Represents a profile used to set a listing's shipping information. Please note that it's not possible to create calculated shipping templates via the API. However, you can associate calculated shipping profiles created from Shop Manager with listings using the API.
    /// </summary>
    public class ShopShippingProfile
    {
        /// <summary>
        /// The numeric ID of the shipping profile.
        /// </summary>
        [JsonPropertyName("shipping_profile_id")]
        public long ShippingProfileId { get; set; }

        /// <summary>
        /// The name string of this shipping profile.
        /// </summary>
        [JsonPropertyName("title")]
        public string Title { get; set; }

        /// <summary>
        /// The numeric ID for the [user](/documentation/reference#tag/User) who owns the shipping profile.
        /// </summary>
        [JsonPropertyName("user_id")]
        public long UserId { get; set; }

        /// <summary>
        /// The minimum number of days for processing the listing.
        /// </summary>
        [JsonPropertyName("min_processing_days")]
        public int? MinProcessingDays { get; set; }

        /// <summary>
        /// The maximum number of days for processing the listing.
        /// </summary>
        [JsonPropertyName("max_processing_days")]
        public int? MaxProcessingDays { get; set; }

        /// <summary>
        /// Translated display label string for processing days.
        /// </summary>
        [JsonPropertyName("processing_days_display_label")]
        public string ProcessingDaysDisplayLabel { get; set; }

        /// <summary>
        /// The ISO code of the country from which the listing ships.
        /// </summary>
        [JsonPropertyName("origin_country_iso")]
        public string OriginCountryIso { get; set; }

        /// <summary>
        /// When true, someone deleted this shipping profile.
        /// </summary>
        [JsonPropertyName("is_deleted")]
        public bool IsDeleted { get; set; }

        /// <summary>
        /// A list of [shipping profile destinations](/documentation/reference#operation/createListingShippingProfileDestination) available for this shipping profile.
        /// </summary>
        [JsonPropertyName("shipping_profile_destinations")]
        public List<ShopShippingProfileDestination> ShippingProfileDestinations { get; set; }

        /// <summary>
        /// A list of [shipping profile upgrades](/documentation/reference#operation/createListingShippingProfileUpgrade) available for this shipping profile.
        /// </summary>
        [JsonPropertyName("shipping_profile_upgrades")]
        public List<ShopShippingProfileUpgrade> ShippingProfileUpgrades { get; set; }

        /// <summary>
        /// The postal code string (not necessarily a number) for the location from which the listing ships.
        /// </summary>
        [JsonPropertyName("origin_postal_code")]
        public string OriginPostalCode { get; set; }

        [JsonConverter(typeof(JsonNullableEnumStringConverter<ShippingProfileType>))]
        [JsonPropertyName("profile_type")]
        public ShippingProfileType ProfileType { get; set; }

    }
}
