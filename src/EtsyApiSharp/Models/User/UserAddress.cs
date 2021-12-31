using System.Text.Json.Serialization;

namespace EtsyApiSharp.Models
{
    /// <summary>
    /// Represents a user's address.
    /// </summary>
    public class UserAddress
    {
        /// <summary>
        /// The numeric ID of the user's address.
        /// </summary>
        [JsonPropertyName("user_address_id")]
        public long UserAddressId { get; set; }

        /// <summary>
        /// The user's numeric ID.
        /// </summary>
        [JsonPropertyName("user_id")]
        public long UserId { get; set; }

        /// <summary>
        /// The user's name for this address.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// The first line of the user's address.
        /// </summary>
        [JsonPropertyName("first_line")]
        public string FirstLine { get; set; }

        /// <summary>
        /// The second line of the user's address.
        /// </summary>
        [JsonPropertyName("second_line")]
        public string SecondLine { get; set; }

        /// <summary>
        /// The city field of the user's address.
        /// </summary>
        [JsonPropertyName("city")]
        public string City { get; set; }

        /// <summary>
        /// The state field of the user's address.
        /// </summary>
        [JsonPropertyName("state")]
        public string State { get; set; }

        /// <summary>
        /// The zip code field of the user's address.
        /// </summary>
        [JsonPropertyName("zip")]
        public string Zip { get; set; }

        /// <summary>
        /// The ISO code of the country in this address.
        /// </summary>
        [JsonPropertyName("iso_country_code")]
        public string IsoCountryCode { get; set; }

        /// <summary>
        /// The name of the user's country.
        /// </summary>
        [JsonPropertyName("country_name")]
        public string CountryName { get; set; }

        /// <summary>
        /// Is this the user's default shipping address.
        /// </summary>
        [JsonPropertyName("is_default_shipping_address")]
        public bool IsDefaultShippingAddress { get; set; }

    }
}
