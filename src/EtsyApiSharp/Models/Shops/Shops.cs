using System.Text.Json.Serialization;

namespace EtsyApiSharp.Models
{
    //A set of Shop records.
    public class Shops
    {
        [JsonPropertyName("count")]
        public int Count { get; set; }


        [JsonPropertyName("results")]
        public List<Shop> Results { get; set; }


    }
}
