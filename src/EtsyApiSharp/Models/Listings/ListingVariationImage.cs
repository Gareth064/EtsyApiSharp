using System.Text.Json.Serialization;

namespace EtsyApiSharp.Models
{
    //A representation of the associations of variations and images on a listing.
    public class ListingVariationImage
    {
        [JsonPropertyName("property_id")]
        public int PropertyId { get; set; }


        [JsonPropertyName("value_id")]
        public int ValueId { get; set; }


        [JsonPropertyName("image_id")]
        public int ImageId { get; set; }


    }
}
