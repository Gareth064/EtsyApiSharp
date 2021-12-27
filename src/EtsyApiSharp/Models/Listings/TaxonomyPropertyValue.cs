using System.Text.Json.Serialization;

namespace EtsyApiSharp.Models
{
    //A property value for a specific product property, which may also employ a specific scale.
    public class TaxonomyPropertyValue
    {
        [JsonPropertyName("value_id")]
        public int ValueId { get; set; }


        [JsonPropertyName("name")]
        public string Name { get; set; }


        [JsonPropertyName("scale_id")]
        public int ScaleId { get; set; }


        [JsonPropertyName("equal_to")]
        public List<long> EqualTo { get; set; }


    }
}
