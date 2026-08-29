using EtsyApiSharp.Models.Listings.Enums;

namespace EtsyApiSharp.Models.Filters;
/// <summary>
/// Represents Find All Listings Active Filter.
/// </summary>

public class FindAllListingsActiveFilter : EtsyFilterBase
{
    /// <summary>
    /// Gets or sets the Sort On.
    /// </summary>
    public ListingSortOn? SortOn { get; set; } = ListingSortOn.created;
    /// <summary>
    /// Gets or sets the Sort Order.
    /// </summary>
    public ListingSortOrder? SortOrder { get; set; } = ListingSortOrder.desc;
    /// <summary>
    /// Gets or sets the Keywords.
    /// </summary>
    public string? Keywords { get; set; }
    /// <summary>
    /// Gets or sets the Min Price.
    /// </summary>
    public double? MinPrice { get; set; }
    /// <summary>
    /// Gets or sets the Max Price.
    /// </summary>
    public double? MaxPrice { get; set; }
    /// <summary>
    /// Gets or sets the Taxonomy Id.
    /// </summary>
    public long? TaxonomyId { get; set; }
    /// <summary>
    /// Gets or sets the Shop Location.
    /// </summary>
    public string? ShopLocation { get; set; }
    /// <summary>
    /// Gets or sets the Is Safe.
    /// </summary>
    public bool? IsSafe { get; set; }
    /// <summary>
    /// Gets or sets the Currency.
    /// </summary>
    public string? Currency { get; set; }
    /// <summary>
    /// Gets or sets the Buyer Country.
    /// </summary>
    public string? BuyerCountry { get; set; }
}
