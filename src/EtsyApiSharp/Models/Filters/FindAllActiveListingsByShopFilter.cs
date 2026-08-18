using EtsyApiSharp.Models.Listings.Enums;

namespace EtsyApiSharp.Models.Filters;
/// <summary>
/// Represents Find All Active Listings By Shop Filter.
/// </summary>

public class FindAllActiveListingsByShopFilter : EtsyFilterBase
{
    /// <summary>
    /// Gets or sets the Keywords.
    /// </summary>
    public string? Keywords { get; set; }
    /// <summary>
    /// Gets or sets the Sort On.
    /// </summary>
    public ListingSortOn? SortOn { get; set; }
    /// <summary>
    /// Gets or sets the Sort Order.
    /// </summary>
    public ListingSortOrder? SortOrder { get; set; }
}
