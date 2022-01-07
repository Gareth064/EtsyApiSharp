using EtsyApiSharp.Models;
using EtsyApiSharp.Models.Common;
using EtsyApiSharp.Models.Filters;
using EtsyApiSharp.Models.Listings.Enums;

namespace EtsyApiSharp.Services.ListingManagements
{
    public interface IEtsyListingManagementService
    {
        Task<ApiResponse<EtsyListResponse<ShopListing>>> FindAllListingsActiveAsync(
            string apiToken,
            FindAllListingsActiveFilter? filter = null);

        Task<ApiResponse<EtsyListResponse<ShopListing>>> FindAllActiveListingsByShopAsync(
            string apiToken,
            long shopId,
            FindAllActiveListingsByShopFilter? filter = null);

        Task<ApiResponse<EtsyListResponse<SellerTaxonomyNode>>> GetSellerTaxonomyNodesAsync(
            string apiToken);

        Task<ApiResponse<EtsyListResponse<TaxonomyNodeProperty>>> GetPropertiesByTaxonomyIdAsync(
            string apiToken,
            long taxonomyId);

        Task<ApiResponse<EtsyListResponse<ShopListing>>> GetListingsByShopAsync(
            string apiToken,
            long shopId,
            GetListingsByShopFilter? filter = null);

        Task<ApiResponse<ShopListingWithAssociations>> GetListingAsync(
            string apiToken,
            long listingId,
            List<ListingInclude>? includes = null);

        Task<ApiResponse<EtsyListResponse<ShopListing>>> GetListingsByListingIdsAsync(
            string apiToken,
            List<long> listingIds,
            List<ListingInclude>? includes = null);

        Task<ApiResponse<EtsyListResponse<ShopListing>>> GetFeaturedListingsByShopAsync(
            string apiToken,
            long shopId,
            GetFeaturedListingsByShopFilter? filter = null);

        Task<ApiResponse<ListingPropertyValue>> GetListingPropertyAsync(
            string apiToken,
            long listingId,
            long propertyId);

        Task<ApiResponse<EtsyListResponse<ListingPropertyValue>>> GetListingPropertiesAsync(
            string apiToken,
            long shopId,
            long listingId);

        Task<ApiResponse<EtsyListResponse<ShopListing>>> GetListingsByShopReceiptAsync(
            string apiToken,
            long shopId,
            long receiptId,
            GetListingsByShopReceiptFilter? filter = null);

        Task<ApiResponse<EtsyListResponse<ShopListing>>> GetListingsByShopSectionIdAsync(
            string apiToken,
            long shopId,
            GetListingsByShopSectionIdFilter filter);
    }
}
