using System.Text.Json.Serialization;

namespace EtsyApiSharp.Models
{
    //Represents several ListingVariationImages.
    public class ListingVariationImages
    {
        [JsonPropertyName("count")]
        public int Count { get; set; }


        [JsonPropertyName("results")]
        public List<ListingVariationImage> Results { get; set; }


    }
}
