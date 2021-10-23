using System.Text.Json.Serialization;


namespace EtsyApiSharp.Models
{
    //A product property definition.
    public class TaxonomyNodeProperty
    {
        [JsonPropertyName("property_id")]
        public int PropertyId { get; set; }


        [JsonPropertyName("name")]
        public string Name { get; set; }


        [JsonPropertyName("display_name")]
        public string DisplayName { get; set; }


        [JsonPropertyName("scales")]
        public List<TaxonomyPropertyScale> Scales { get; set; }


        [JsonPropertyName("is_required")]
        public bool IsRequired { get; set; }


        [JsonPropertyName("supports_attributes")]
        public bool SupportsAttributes { get; set; }


        [JsonPropertyName("supports_variations")]
        public bool SupportsVariations { get; set; }


        [JsonPropertyName("is_multivalued")]
        public bool IsMultivalued { get; set; }


        [JsonPropertyName("possible_values")]
        public List<TaxonomyPropertyValue> PossibleValues { get; set; }


        [JsonPropertyName("selected_values")]
        public List<TaxonomyPropertyValue> SelectedValues { get; set; }


    }
}
