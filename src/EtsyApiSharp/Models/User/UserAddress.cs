using System.Text.Json.Serialization;

namespace EtsyApiSharp.Models
{
    //Represents a user's address.
    public class UserAddress
    {
        [JsonPropertyName("user_address_id")]
        public int UserAddressId { get; set; }


        [JsonPropertyName("user_id")]
        public int UserId { get; set; }


        [JsonPropertyName("name")]
        public string Name { get; set; }


        [JsonPropertyName("first_line")]
        public string FirstLine { get; set; }


        [JsonPropertyName("second_line")]
        public string SecondLine { get; set; }


        [JsonPropertyName("city")]
        public string City { get; set; }


        [JsonPropertyName("state")]
        public string State { get; set; }


        [JsonPropertyName("zip")]
        public string Zip { get; set; }


        [JsonPropertyName("iso_country_code")]
        public string IsoCountryCode { get; set; }


        [JsonPropertyName("country_name")]
        public string CountryName { get; set; }


        [JsonPropertyName("is_default_shipping_address")]
        public bool IsDefaultShippingAddress { get; set; }


    }
}
