using EtsyApiSharp.Models.Listings.Enums;

namespace EtsyApiSharp.Models.Filters;

public class GetListingsByShopSectionIdFilter : EtsyFilterBase
{
    public List<long> ShopSectionIds { get; set; }
    public ListingSortOn SortOn { get; set; } = ListingSortOn.created;
    public ListingSortOrder SortOrder { get; set; } = ListingSortOrder.desc;
}
