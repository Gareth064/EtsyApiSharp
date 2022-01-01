using System.Text.Json.Serialization;

namespace EtsyApiSharp.Models
{
    /// <summary>
    /// A property value for a specific product property, which may also employ a specific scale.
    /// </summary>
    public class TaxonomyPropertyValue
    {
        /// <summary>
        /// The numeric ID of this property value.
        /// </summary>
        [JsonPropertyName("value_id")]
        public long? ValueId { get; set; }

        /// <summary>
        /// The name string of this property value.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// The numeric scale ID of the scale to which this property value belongs.
        /// </summary>
        [JsonPropertyName("scale_id")]
        public long? ScaleId { get; set; }

        /// <summary>
        /// A list of numeric property value IDs this property value is equal to (if any).
        /// </summary>
        [JsonPropertyName("equal_to")]
        public List<int> EqualTo { get; set; }

    }
}
