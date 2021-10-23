using System.Text.Json.Serialization;


namespace EtsyApiSharp.Models
{
    //A scale defnining the assignable increments for the property values available to specific product properties.
    public class TaxonomyPropertyScale
    {
        [JsonPropertyName("scale_id")]
        public int ScaleId { get; set; }


        [JsonPropertyName("display_name")]
        public string DisplayName { get; set; }


        [JsonPropertyName("description")]
        public string Description { get; set; }


    }
}
