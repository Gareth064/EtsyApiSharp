using System.Text.Json.Serialization;

namespace EtsyApiSharp.Models
{
    //Represents several ListingPropertyValues.
    public class ListingPropertyValues
    {
        [JsonPropertyName("count")]
        public int Count { get; set; }


        [JsonPropertyName("results")]
        public List<ListingPropertyValue> Results { get; set; }


    }
}
