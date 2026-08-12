namespace EtsyApiSharp.Models.Filters;

/// <summary>
/// Filters and paginates Etsy review results.
/// </summary>
public class GetReviewsFilter : EtsyFilterBase
{
    /// <summary>
    /// Gets or sets the earliest review creation time, as a Unix timestamp.
    /// </summary>
    public long? MinCreated { get; set; }

    /// <summary>
    /// Gets or sets the latest review creation time, as a Unix timestamp.
    /// </summary>
    public long? MaxCreated { get; set; }
}
