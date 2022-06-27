using System.Text.Json.Serialization;

namespace EtsyApiSharp.Models
{
    /// <summary>
    /// A representation of structured data values.
    /// </summary>
    public class ListingPropertyValue
    {
        /// <summary>
        /// The numeric ID of the Property.
        /// </summary>
        [JsonPropertyName("property_id")]
        public long PropertyId { get; set; }

        /// <summary>
        /// The name of the Property.
        /// </summary>
        [JsonPropertyName("property_name")]
        public string PropertyName { get; set; }

        /// <summary>
        /// The numeric ID of the scale (if any).
        /// </summary>
        [JsonPropertyName("scale_id")]
        public long? ScaleId { get; set; }

        /// <summary>
        /// The label used to describe the chosen scale (if any).
        /// </summary>
        [JsonPropertyName("scale_name")]
        public string ScaleName { get; set; }

        /// <summary>
        /// The numeric IDs of the Property values
        /// </summary>
        [JsonPropertyName("value_ids")]
        public List<long> ValueIds { get; set; }

        /// <summary>
        /// The Property values
        /// </summary>
        [JsonPropertyName("values")]
        public List<string> Values { get; set; }

    }
}
