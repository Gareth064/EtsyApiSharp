using System.Text.Json.Serialization;

namespace EtsyApiSharp.Models;

/// <summary>
/// A product property definition.
/// </summary>
public class BuyerTaxonomyNodeProperty
{
    /// <summary>
    /// The unique numeric ID of this product property.
    /// </summary>
    [JsonPropertyName("property_id")]
    public long PropertyId { get; set; }

    /// <summary>
    /// The name string for this taxonomy node.
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; }

    /// <summary>
    /// The human-readable product property name string.
    /// </summary>
    [JsonPropertyName("display_name")]
    public string DisplayName { get; set; }

    /// <summary>
    /// A list of available scales.
    /// </summary>
    [JsonPropertyName("scales")]
    public List<BuyerTaxonomyPropertyScale> Scales { get; set; }

    /// <summary>
    /// When true, listings assigned eligible taxonomy IDs require this property.
    /// </summary>
    [JsonPropertyName("is_required")]
    public bool IsRequired { get; set; }

    /// <summary>
    /// When true, you can use this property in listing attributes.
    /// </summary>
    [JsonPropertyName("supports_attributes")]
    public bool SupportsAttributes { get; set; }

    /// <summary>
    /// When true, you can use this property in listing variations.
    /// </summary>
    [JsonPropertyName("supports_variations")]
    public bool SupportsVariations { get; set; }

    /// <summary>
    /// When true, you can assign multiple property values to this property
    /// </summary>
    [JsonPropertyName("is_multivalued")]
    public bool IsMultivalued { get; set; }

    /// <summary>
    /// When true, you can assign multiple property values to this property
    /// </summary>
    [JsonPropertyName("max_values_allowed")]
    public int? MaxValuesAllowed { get; set; }

    /// <summary>
    /// A list of supported property value strings for this property.
    /// </summary>
    [JsonPropertyName("possible_values")]
    public List<BuyerTaxonomyPropertyValue> PossibleValues { get; set; }

    /// <summary>
    /// A list of property value strings automatically and always selected for the given property.
    /// </summary>
    [JsonPropertyName("selected_values")]
    public List<BuyerTaxonomyPropertyValue> SelectedValues { get; set; }

}
