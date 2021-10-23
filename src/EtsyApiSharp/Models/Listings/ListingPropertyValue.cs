using System.Text.Json.Serialization;


namespace EtsyApiSharp.Models
{
    //A representation of structured data values.
    public class ListingPropertyValue
    {
        [JsonPropertyName("property_id")]
        public int PropertyId { get; set; }


        [JsonPropertyName("property_name")]
        public string PropertyName { get; set; }


        [JsonPropertyName("scale_id")]
        public int ScaleId { get; set; }


        [JsonPropertyName("scale_name")]
        public string ScaleName { get; set; }


        [JsonPropertyName("value_ids")]
        public List<long> ValueIds { get; set; }


        [JsonPropertyName("values")]
        public string[] Values { get; set; }


    }
}
