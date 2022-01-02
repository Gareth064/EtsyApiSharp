using EtsyApiSharp.Models.Listings.Enums;

namespace EtsyApiSharp.Models.Filters
{
    public class GetListingsByShopFilter : EtsyFilterBase
    {
        public ListingState State { get; set; } = ListingState.active;
        public ListingSortOn SortOn { get; set; } = ListingSortOn.created;
        public ListingSortOrder SortOrder { get; set; } = ListingSortOrder.desc;
    }
}
