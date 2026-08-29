using EtsyApiSharp.Models.Listings.Enums;

namespace EtsyApiSharp.Models.Filters;
/// <summary>
/// Represents Get Listings By Shop Section Id Filter.
/// </summary>

public class GetListingsByShopSectionIdFilter : EtsyFilterBase
{
    /// <summary>
    /// Gets or sets the Sort On.
    /// </summary>
    public ListingSortOn SortOn { get; set; } = ListingSortOn.created;
    /// <summary>
    /// Gets or sets the Sort Order.
    /// </summary>
    public ListingSortOrder SortOrder { get; set; } = ListingSortOrder.desc;
    /// <summary>
    /// Gets or sets the Legacy.
    /// </summary>
    public bool? Legacy { get; set; }
}
