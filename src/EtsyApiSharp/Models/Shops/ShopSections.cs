using System.Text.Json.Serialization;

namespace EtsyApiSharp.Models
{
    //All the sections in a sprecific Shop.
    public class ShopSections
    {
        [JsonPropertyName("count")]
        public int Count { get; set; }


        [JsonPropertyName("results")]
        public List<ShopSection> Results { get; set; }


    }
}
