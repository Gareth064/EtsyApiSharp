using EtsyApiSharp.Models.Listings.Enums;

namespace EtsyApiSharp.Models.Filters;

public class FindAllActiveListingsByShopFilter : EtsyFilterBase
{
    public string? Keywords { get; set; }
    public ListingSortOn? SortOn { get; set; }
    public ListingSortOrder? SortOrder { get; set; }
}
