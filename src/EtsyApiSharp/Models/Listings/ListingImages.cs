using System.Text.Json.Serialization;

namespace EtsyApiSharp.Models
{
    //Represents a list of listing image resources, each of which contains the reference URLs and metadata for an image.
    public class ListingImages
    {
        [JsonPropertyName("count")]
        public int Count { get; set; }


        [JsonPropertyName("results")]
        public List<ListingImage> Results { get; set; }


    }
}
