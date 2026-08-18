namespace EtsyApiSharp.Models;
/// <summary>
/// Represents Listing Translation Request.
/// </summary>

public class ListingTranslationRequest
{
    /// <summary>
    /// Gets or sets the Title.
    /// </summary>
    public string Title { get; set; } = string.Empty;
    /// <summary>
    /// Gets or sets the Description.
    /// </summary>
    public string Description { get; set; } = string.Empty;
    /// <summary>
    /// Gets or sets the Tags.
    /// </summary>
    public IReadOnlyCollection<string>? Tags { get; set; }
}
