using System.Text.Json.Serialization;

namespace EtsyApiSharp.Models
{
    /// <summary>
    /// A scale defnining the assignable increments for the property values available to specific product properties.
    /// </summary>
    public class BuyerTaxonomyPropertyScale
    {
        /// <summary>
        /// The unique numeric ID of a scale.
        /// </summary>
        [JsonPropertyName("scale_id")]
        public long ScaleId { get; set; }

        /// <summary>
        /// The name string for a scale.
        /// </summary>
        [JsonPropertyName("display_name")]
        public string DisplayName { get; set; }

        /// <summary>
        /// The description string for a scale.
        /// </summary>
        [JsonPropertyName("description")]
        public string Description { get; set; }

    }
}
