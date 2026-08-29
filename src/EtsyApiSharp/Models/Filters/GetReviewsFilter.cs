namespace EtsyApiSharp.Models.Filters;

/// <summary>
/// Filters and paginates Etsy review results.
/// </summary>
public class GetReviewsFilter : EtsyFilterBase
{
    /// <summary>
    /// Gets or sets the Min Created.
    /// </summary>
    public long? MinCreated { get; set; }
    /// <summary>
    /// Gets or sets the Max Created.
    /// </summary>

    public long? MaxCreated { get; set; }
}
