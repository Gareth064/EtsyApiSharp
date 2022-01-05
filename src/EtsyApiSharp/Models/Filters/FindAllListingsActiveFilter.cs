using EtsyApiSharp.Models.Listings.Enums;

namespace EtsyApiSharp.Models.Filters
{
    public class FindAllListingsActiveFilter : EtsyFilterBase
    {
        public ListingSortOn? SortOn { get; set; } = ListingSortOn.created;
        public ListingSortOrder? SortOrder { get; set; } = ListingSortOrder.desc;
        public string? Keywords { get; set; }
        public double? MinPrice { get; set; }
        public double? MaxPrice { get; set; }
        public long? TaxonomyId { get; set; }
        public string? ShopLocation { get; set; }
    }
}
