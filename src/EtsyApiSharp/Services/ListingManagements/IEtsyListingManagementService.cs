using EtsyApiSharp.Models;
using EtsyApiSharp.Models.Common;
using EtsyApiSharp.Models.Filters;
using EtsyApiSharp.Models.Listings.Enums;

namespace EtsyApiSharp.Services
{
    public interface IEtsyListingManagementService
    {
        Task<ApiResponse<List<ShopListing>>> FindAllListingsActiveAsync(
            string apiToken,
            FindAllListingsActiveFilter? filter = null);

        Task<ApiListResponse<List<ShopListing>>> FindAllActiveListingsByShopAsync(
            string apiToken,
            long shopId,
            FindAllActiveListingsByShopFilter? filter = null);

        Task<ApiResponse<List<SellerTaxonomyNode>>> GetSellerTaxonomyNodesAsync(
            string apiToken);

        Task<ApiResponse<List<TaxonomyNodeProperty>>> GetPropertiesByTaxonomyIdAsync(
            string apiToken,
            long taxonomyId);

        Task<ApiResponse<List<ShopListing>>> GetListingsByShopAsync(
            string apiToken,
            long shopId,
            GetListingsByShopFilter? filter = null);

        Task<ApiResponse<ShopListing>> GetListingAsync(
            string apiToken,
            long listingId,
            List<ListingInclude>? includes = null);

        Task<ApiResponse<List<ShopListing>>> GetListingsByListingIdsAsync(
            string apiToken,
            List<long> listingIds,
            List<ListingInclude>? includes = null);

        Task<ApiResponse<List<ShopListing>>> GetFeaturedListingsByShopAsync(
            string apiToken,
            long shopId,
            GetFeaturedListingsByShopFilter? filter = null);

        Task<ApiResponse<ListingPropertyValue>> GetListingPropertyAsync(
            string apiToken,
            long listingId,
            long propertyId);

        Task<ApiResponse<List<ListingPropertyValue>>> GetListingPropertiesAsync(
            string apiToken,
            long shopId,
            long listingId);

        Task<ApiResponse<List<ShopListing>>> GetListingsByShopReceiptAsync(
            string apiToken,
            long shopId,
            long receiptId,
            GetListingsByShopReceiptFilter? filter = null);

        Task<ApiResponse<List<ShopListing>>> GetListingsByShopSectionIdAsync(
            string apiToken,
            long shopId,
            GetListingsByShopSectionIdFilter filter);
    }
}
