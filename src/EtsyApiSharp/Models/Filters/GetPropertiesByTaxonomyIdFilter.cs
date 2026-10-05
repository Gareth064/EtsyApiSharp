namespace EtsyApiSharp.Models.Filters;

/// <summary>
/// Represents optional capability filters for seller taxonomy properties.
/// </summary>
public class GetPropertiesByTaxonomyIdFilter
{
    /// <summary>
    /// Gets or sets whether to return properties that support variations.
    /// </summary>
    public bool? SupportsVariations { get; set; }

    /// <summary>
    /// Gets or sets whether to return properties that support attributes.
    /// </summary>
    public bool? SupportsAttributes { get; set; }
}
