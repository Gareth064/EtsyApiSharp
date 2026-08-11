using EtsyApiSharp.Models;
using EtsyApiSharp.Models.Common;
using EtsyApiSharp.Models.Filters;
using EtsyApiSharp.Models.Listings.Enums;

namespace EtsyApiSharp.Services.ListingManagements;

/// <summary>
/// Defines Etsy Open API v3 Listing Management operations.
/// </summary>
public interface IEtsyListingManagementService
{
    Task<ApiResponse<EtsyListResponse<ShopListingWithAssociations>>> FindAllListingsActiveAsync(FindAllListingsActiveFilter? filter = null, CancellationToken cancellationToken = default);
    Task<ApiResponse<EtsyListResponse<ShopListingWithAssociations>>> FindAllActiveListingsByShopAsync(long shopId, FindAllActiveListingsByShopFilter? filter = null, CancellationToken cancellationToken = default);
    Task<ApiResponse<EtsyListResponse<SellerTaxonomyNode>>> GetSellerTaxonomyNodesAsync(CancellationToken cancellationToken = default);
    Task<ApiResponse<EtsyListResponse<TaxonomyNodeProperty>>> GetPropertiesByTaxonomyIdAsync(long taxonomyId, CancellationToken cancellationToken = default);
    Task<ApiResponse<EtsyListResponse<ShopListingWithAssociations>>> GetListingsByShopAsync(string accessToken, long shopId, IReadOnlyCollection<ListingInclude>? includes = null, GetListingsByShopFilter? filter = null, CancellationToken cancellationToken = default);
    Task<ApiResponse<ShopListingWithAssociations>> GetListingAsync(long listingId, IReadOnlyCollection<ListingInclude>? includes = null, string? language = null, bool? allowSuggestedTitle = null, CancellationToken cancellationToken = default);
    Task<ApiResponse<EtsyListResponse<ShopListingWithAssociations>>> GetListingsByListingIdsAsync(IReadOnlyCollection<long> listingIds, IReadOnlyCollection<ListingInclude>? includes = null, bool? legacy = null, string? currency = null, string? buyerCountry = null, CancellationToken cancellationToken = default);
    Task<ApiResponse<EtsyListResponse<ShopListingWithAssociations>>> GetFeaturedListingsByShopAsync(long shopId, GetFeaturedListingsByShopFilter? filter = null, CancellationToken cancellationToken = default);
    Task<ApiResponse<ListingPropertyValue>> GetListingPropertyAsync(long listingId, long propertyId, CancellationToken cancellationToken = default);
    Task<ApiResponse<EtsyListResponse<ListingPropertyValue>>> GetListingPropertiesAsync(long shopId, long listingId, CancellationToken cancellationToken = default);
    Task<ApiResponse<EtsyListResponse<ShopListingWithAssociations>>> GetListingsByShopReceiptAsync(string accessToken, long shopId, long receiptId, GetListingsByShopReceiptFilter? filter = null, CancellationToken cancellationToken = default);
    Task<ApiResponse<EtsyListResponse<ShopListingWithAssociations>>> GetListingsByShopSectionIdAsync(long shopId, IReadOnlyCollection<long> sectionIds, GetListingsByShopSectionIdFilter? filter = null, CancellationToken cancellationToken = default);
    Task<ApiResponse<EtsyListResponse<BuyerTaxonomyNodeProperty>>> GetPropertiesByBuyerTaxonomyIdAsync(long taxonomyId, CancellationToken cancellationToken = default);
    Task<ApiResponse<EtsyListResponse<BuyerTaxonomyNode>>> GetBuyerTaxonomyNodesAsync(CancellationToken cancellationToken = default);
    Task<ApiResponse<ListingImage>> GetListingImageAsync(long listingId, long listingImageId, CancellationToken cancellationToken = default);
    Task<ApiResponse<EtsyListResponse<ListingImage>>> GetListingImagesAsync(long listingId, CancellationToken cancellationToken = default);
    Task<ApiResponse<ShopListing>> CreateDraftListingAsync(string accessToken, long shopId, CreateDraftListingRequest listing, CancellationToken cancellationToken = default);
    Task<ApiResponse<ShopListing>> UpdateListingAsync(string accessToken, long shopId, long listingId, UpdateListingRequest listing, CancellationToken cancellationToken = default);
    Task<ApiResponse<object>> DeleteListingAsync(string accessToken, long listingId, CancellationToken cancellationToken = default);
    Task<ApiResponse<EtsyListResponse<ShopListingWithAssociations>>> GetListingsByShopReturnPolicyAsync(string accessToken, long shopId, long returnPolicyId, bool? legacy = null, CancellationToken cancellationToken = default);
    Task<ApiResponse<EtsyListResponse<ShopListingWithAssociations>>> GetListingsShippingByListingIdsAsync(string accessToken, IReadOnlyCollection<long> listingIds, CancellationToken cancellationToken = default);
    Task<ApiResponse<ListingPropertyValue>> UpdateListingPropertyAsync(string accessToken, long shopId, long listingId, long propertyId, UpdateListingPropertyRequest property, CancellationToken cancellationToken = default);
    Task<ApiResponse<object>> DeleteListingPropertyAsync(string accessToken, long shopId, long listingId, long propertyId, CancellationToken cancellationToken = default);
    Task<ApiResponse<EtsyListResponse<ShopListingFile>>> GetAllListingFilesAsync(string accessToken, long shopId, long listingId, CancellationToken cancellationToken = default);
    Task<ApiResponse<ShopListingFile>> GetListingFileAsync(string accessToken, long shopId, long listingId, long listingFileId, CancellationToken cancellationToken = default);
    Task<ApiResponse<ShopListingFile>> UploadListingFileAsync(string accessToken, long shopId, long listingId, ListingFileUploadRequest file, CancellationToken cancellationToken = default);
    Task<ApiResponse<object>> DeleteListingFileAsync(string accessToken, long shopId, long listingId, long listingFileId, CancellationToken cancellationToken = default);
    Task<ApiResponse<ListingImage>> UploadListingImageAsync(string accessToken, long shopId, long listingId, ListingImageUploadRequest image, CancellationToken cancellationToken = default);
    Task<ApiResponse<object>> DeleteListingImageAsync(string accessToken, long shopId, long listingId, long listingImageId, CancellationToken cancellationToken = default);
    Task<ApiResponse<ListingInventoryWithAssociations>> GetListingInventoryAsync(string accessToken, long listingId, bool? showDeleted = null, IReadOnlyCollection<ListingInclude>? includes = null, CancellationToken cancellationToken = default);
    Task<ApiResponse<EtsyListResponse<ShopListingWithAssociations>>> GetListingsInventoryByListingIdsAsync(string accessToken, IReadOnlyCollection<long> listingIds, CancellationToken cancellationToken = default);
    Task<ApiResponse<ListingInventory>> UpdateListingInventoryAsync(string accessToken, long listingId, UpdateListingInventoryRequest inventory, string? maxVariationsSupported = null, CancellationToken cancellationToken = default);
    Task<ApiResponse<ListingInventoryProductOffering>> GetListingOfferingAsync(long listingId, long productId, long productOfferingId, bool? legacy = null, CancellationToken cancellationToken = default);
    Task<ApiResponse<ListingInventoryProduct>> GetListingProductAsync(string accessToken, long listingId, long productId, bool? legacy = null, CancellationToken cancellationToken = default);
    Task<ApiResponse<ListingPersonalization>> GetListingPersonalizationAsync(long listingId, CancellationToken cancellationToken = default);
    Task<ApiResponse<ListingPersonalization>> UpdateListingPersonalizationAsync(string accessToken, long shopId, long listingId, ListingPersonalizationUpdateRequest personalization, bool? supportsMultiplePersonalizationQuestions = null, CancellationToken cancellationToken = default);
    Task<ApiResponse<object>> DeleteListingPersonalizationAsync(string accessToken, long shopId, long listingId, CancellationToken cancellationToken = default);
    Task<ApiResponse<ListingTranslation>> GetListingTranslationAsync(long shopId, long listingId, string language, CancellationToken cancellationToken = default);
    Task<ApiResponse<ListingTranslation>> CreateListingTranslationAsync(string accessToken, long shopId, long listingId, string language, ListingTranslationRequest translation, CancellationToken cancellationToken = default);
    Task<ApiResponse<ListingTranslation>> UpdateListingTranslationAsync(string accessToken, long shopId, long listingId, string language, ListingTranslationRequest translation, CancellationToken cancellationToken = default);
    Task<ApiResponse<EtsyListResponse<ListingVariationImage>>> GetListingVariationImagesAsync(long shopId, long listingId, CancellationToken cancellationToken = default);
    Task<ApiResponse<EtsyListResponse<ListingVariationImage>>> UpdateVariationImagesAsync(string accessToken, long shopId, long listingId, IReadOnlyCollection<ListingVariationImage> variationImages, CancellationToken cancellationToken = default);
    Task<ApiResponse<ListingVideo>> GetListingVideoAsync(long listingId, long videoId, CancellationToken cancellationToken = default);
    Task<ApiResponse<EtsyListResponse<ListingVideo>>> GetListingVideosAsync(long listingId, CancellationToken cancellationToken = default);
    Task<ApiResponse<ListingVideo>> UploadListingVideoAsync(string accessToken, long shopId, long listingId, ListingVideoUploadRequest video, CancellationToken cancellationToken = default);
    Task<ApiResponse<object>> DeleteListingVideoAsync(string accessToken, long shopId, long listingId, long videoId, CancellationToken cancellationToken = default);
}
