using EtsyApiSharp.Models;
using EtsyApiSharp.Models.Common;
using EtsyApiSharp.Models.Filters;
using EtsyApiSharp.Models.Listings.Enums;

namespace EtsyApiSharp.Services.ListingManagements
{
    public interface IEtsyListingManagementService
    {
        Task<ApiResponse<EtsyListResponse<ShopListingWithAssociations>>> FindAllListingsActiveAsync(
            string apiToken,
            FindAllListingsActiveFilter? filter = null);

        Task<ApiResponse<EtsyListResponse<ShopListingWithAssociations>>> FindAllActiveListingsByShopAsync(
            string apiToken,
            long shopId,
            FindAllActiveListingsByShopFilter? filter = null);

        Task<ApiResponse<EtsyListResponse<SellerTaxonomyNode>>> GetSellerTaxonomyNodesAsync(
            string apiToken);

        Task<ApiResponse<EtsyListResponse<TaxonomyNodeProperty>>> GetPropertiesByTaxonomyIdAsync(
            string apiToken,
            long taxonomyId);

        Task<ApiResponse<EtsyListResponse<ShopListingWithAssociations>>> GetListingsByShopAsync(
            string apiToken,
            long shopId,
            List<ListingInclude> includes = null,
            GetListingsByShopFilter? filter = null);

        Task<ApiResponse<ShopListingWithAssociations>> GetListingAsync(
            string apiToken,
            long listingId,
            List<ListingInclude>? includes = null);

        Task<ApiResponse<EtsyListResponse<ShopListingWithAssociations>>> GetListingsByListingIdsAsync(
            string apiToken,
            List<long> listingIds,
            List<ListingInclude>? includes = null);

        Task<ApiResponse<EtsyListResponse<ShopListingWithAssociations>>> GetFeaturedListingsByShopAsync(
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

        Task<ApiResponse<EtsyListResponse<ShopListingWithAssociations>>> GetListingsByShopReceiptAsync(
            string apiToken,
            long shopId,
            long receiptId,
            GetListingsByShopReceiptFilter? filter = null);

        Task<ApiResponse<EtsyListResponse<ShopListingWithAssociations>>> GetListingsByShopSectionIdAsync(
            string apiToken,
            long shopId,
            List<long> sectionIds,
            GetListingsByShopSectionIdFilter? filter);

        Task<ApiResponse<EtsyListResponse<TaxonomyNodeProperty>>> GetPropertiesByBuyerTaxonomyIdAsync(
            string apiToken,
            long taxonomyId);

        public Task<ApiResponse<EtsyListResponse<SellerTaxonomyNode>>> GetBuyerTaxonomyNodesAsync(
            string apiToken);

        public Task<ApiResponse<ListingImage>> GetListingImageAsync(
            string apiToken,
            long listingId,
            long listingImageId);

        public Task<ApiResponse<EtsyListResponse<ListingImage>>> GetListingImagesAsync(
            string apiToken,
            long listingId);
    }
}
