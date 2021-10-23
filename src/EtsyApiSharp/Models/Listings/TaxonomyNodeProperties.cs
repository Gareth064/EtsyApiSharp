using System.Text.Json.Serialization;


namespace EtsyApiSharp.Models
{
    //A list of product property definitions.
    public class TaxonomyNodeProperties
    {
        [JsonPropertyName("count")]
        public int Count { get; set; }


        [JsonPropertyName("results")]
        public List<TaxonomyNodeProperty> Results { get; set; }


    }
}
