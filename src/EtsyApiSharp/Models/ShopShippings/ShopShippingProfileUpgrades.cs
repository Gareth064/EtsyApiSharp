using System.Text.Json.Serialization;


namespace EtsyApiSharp.Models
{
    //A list of shipping upgrade options.
    public class ShopShippingProfileUpgrades
    {
        [JsonPropertyName("count")]
        public int Count { get; set; }


        [JsonPropertyName("results")]
        public List<ShopShippingProfileUpgrade> Results { get; set; }


    }
}
