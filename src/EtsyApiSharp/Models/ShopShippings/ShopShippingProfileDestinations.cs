using System.Text.Json.Serialization;

namespace EtsyApiSharp.Models
{
    //Represents a list of shipping destination objects.
    public class ShopShippingProfileDestinations
    {
        [JsonPropertyName("count")]
        public int Count { get; set; }


        [JsonPropertyName("results")]
        public List<ShopShippingProfileDestination> Results { get; set; }


    }
}
