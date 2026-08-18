using EtsyApiSharp.Models.Listings.Enums;

namespace EtsyApiSharp.Models.Filters;
/// <summary>
/// Represents Get Listings By Shop Filter.
/// </summary>

public class GetListingsByShopFilter : EtsyFilterBase
{
    /// <summary>
    /// Gets or sets the State.
    /// </summary>
    public ListingState State { get; set; } = ListingState.active;
    /// <summary>
    /// Gets or sets the Sort On.
    /// </summary>
    public ListingSortOn SortOn { get; set; } = ListingSortOn.created;
    /// <summary>
    /// Gets or sets the Sort Order.
    /// </summary>
    public ListingSortOrder SortOrder { get; set; } = ListingSortOrder.desc;
}
