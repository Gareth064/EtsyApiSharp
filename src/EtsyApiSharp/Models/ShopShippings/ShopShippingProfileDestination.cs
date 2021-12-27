using System.Text.Json.Serialization;

namespace EtsyApiSharp.Models
{
    //Represents a shipping destination assigned to a shipping profile.
    public class ShopShippingProfileDestination
    {
        [JsonPropertyName("shipping_profile_destination_id")]
        public int ShippingProfileDestinationId { get; set; }


        [JsonPropertyName("shipping_profile_id")]
        public int ShippingProfileId { get; set; }


        [JsonPropertyName("origin_country_iso")]
        public string OriginCountryIso { get; set; }


        [JsonPropertyName("destination_country_iso")]
        public string DestinationCountryIso { get; set; }


        [JsonPropertyName("destination_region")]
        public string DestinationRegion { get; set; }


        [JsonPropertyName("primary_cost")]
        public Money PrimaryCost { get; set; }


        [JsonPropertyName("secondary_cost")]
        public Money SecondaryCost { get; set; }


        [JsonPropertyName("shipping_carrier_id")]
        public int ShippingCarrierId { get; set; }


        [JsonPropertyName("mail_class")]
        public string MailClass { get; set; }


        [JsonPropertyName("min_delivery_days")]
        public int MinDeliveryDays { get; set; }


        [JsonPropertyName("max_delivery_days")]
        public int MaxDeliveryDays { get; set; }


    }
}
