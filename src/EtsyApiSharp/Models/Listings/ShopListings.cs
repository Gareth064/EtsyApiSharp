using System.Text.Json.Serialization;

namespace EtsyApiSharp.Models
{
    //A set of ShopListing resources.
    public class ShopListings
    {
        [JsonPropertyName("count")]
        public int Count { get; set; }


        [JsonPropertyName("results")]
        public List<ShopListing> Results { get; set; }


    }
}
