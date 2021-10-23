using System.Text.Json.Serialization;


namespace EtsyApiSharp.Models
{
    //Represents several ShopShippingProfiles.
    public class ShopShippingProfiles
    {
        [JsonPropertyName("count")]
        public int Count { get; set; }


        [JsonPropertyName("results")]
        public List<ShopShippingProfile> Results { get; set; }


    }
}
