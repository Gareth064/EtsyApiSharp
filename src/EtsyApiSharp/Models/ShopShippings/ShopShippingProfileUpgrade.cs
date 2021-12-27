using System.Text.Json.Serialization;

namespace EtsyApiSharp.Models
{
    //A representation of a shipping profile upgrade option.
    public class ShopShippingProfileUpgrade
    {
        [JsonPropertyName("shipping_profile_id")]
        public int ShippingProfileId { get; set; }


        [JsonPropertyName("upgrade_id")]
        public int UpgradeId { get; set; }


        [JsonPropertyName("upgrade_name")]
        public string UpgradeName { get; set; }


        [JsonPropertyName("type")]
        public string Type { get; set; }


        [JsonPropertyName("rank")]
        public int Rank { get; set; }


        [JsonPropertyName("language")]
        public string Language { get; set; }


        [JsonPropertyName("price")]
        public Money Price { get; set; }


        [JsonPropertyName("secondary_price")]
        public Money SecondaryPrice { get; set; }


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
