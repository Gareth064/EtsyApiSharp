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
    /// <summary>
    /// Executes the Find All Listings Active operation.
    /// </summary>
    Task<ApiResponse<EtsyListResponse<ShopListingWithAssociations>>> FindAllListingsActiveAsync(FindAllListingsActiveFilter? filter = null, CancellationToken cancellationToken = default);
    /// <summary>
    /// Executes the Find All Active Listings By Shop operation.
    /// </summary>
    Task<ApiResponse<EtsyListResponse<ShopListingWithAssociations>>> FindAllActiveListingsByShopAsync(long shopId, FindAllActiveListingsByShopFilter? filter = null, CancellationToken cancellationToken = default);
    /// <summary>
    /// Executes the Get Seller Taxonomy Nodes operation.
    /// </summary>
    Task<ApiResponse<EtsyListResponse<SellerTaxonomyNode>>> GetSellerTaxonomyNodesAsync(CancellationToken cancellationToken = default);
    /// <summary>
    /// Executes the Get Properties By Taxonomy Id operation.
    /// </summary>
    Task<ApiResponse<EtsyListResponse<TaxonomyNodeProperty>>> GetPropertiesByTaxonomyIdAsync(long taxonomyId, CancellationToken cancellationToken = default);
    /// <summary>
    /// Executes the Get Listings By Shop operation.
    /// </summary>
    Task<ApiResponse<EtsyListResponse<ShopListingWithAssociations>>> GetListingsByShopAsync(string accessToken, long shopId, IReadOnlyCollection<ListingInclude>? includes = null, GetListingsByShopFilter? filter = null, CancellationToken cancellationToken = default);
    /// <summary>
    /// Executes the Get Listing operation.
    /// </summary>
    Task<ApiResponse<ShopListingWithAssociations>> GetListingAsync(long listingId, IReadOnlyCollection<ListingInclude>? includes = null, string? language = null, bool? allowSuggestedTitle = null, CancellationToken cancellationToken = default);
    /// <summary>
    /// Executes the Get Listings By Listing Ids operation.
    /// </summary>
    Task<ApiResponse<EtsyListResponse<ShopListingWithAssociations>>> GetListingsByListingIdsAsync(IReadOnlyCollection<long> listingIds, IReadOnlyCollection<ListingInclude>? includes = null, bool? legacy = null, string? currency = null, string? buyerCountry = null, CancellationToken cancellationToken = default);
    /// <summary>
    /// Executes the Get Featured Listings By Shop operation.
    /// </summary>
    Task<ApiResponse<EtsyListResponse<ShopListingWithAssociations>>> GetFeaturedListingsByShopAsync(long shopId, GetFeaturedListingsByShopFilter? filter = null, CancellationToken cancellationToken = default);
    /// <summary>
    /// Executes the Get Listing Property operation.
    /// </summary>
    Task<ApiResponse<ListingPropertyValue>> GetListingPropertyAsync(long listingId, long propertyId, CancellationToken cancellationToken = default);
    /// <summary>
    /// Executes the Get Listing Properties operation.
    /// </summary>
    Task<ApiResponse<EtsyListResponse<ListingPropertyValue>>> GetListingPropertiesAsync(long shopId, long listingId, CancellationToken cancellationToken = default);
    /// <summary>
    /// Executes the Get Listings By Shop Receipt operation.
    /// </summary>
    Task<ApiResponse<EtsyListResponse<ShopListingWithAssociations>>> GetListingsByShopReceiptAsync(string accessToken, long shopId, long receiptId, GetListingsByShopReceiptFilter? filter = null, CancellationToken cancellationToken = default);
    /// <summary>
    /// Executes the Get Listings By Shop Section Id operation.
    /// </summary>
    Task<ApiResponse<EtsyListResponse<ShopListingWithAssociations>>> GetListingsByShopSectionIdAsync(long shopId, IReadOnlyCollection<long> sectionIds, GetListingsByShopSectionIdFilter? filter = null, CancellationToken cancellationToken = default);
    /// <summary>
    /// Executes the Get Properties By Buyer Taxonomy Id operation.
    /// </summary>
    Task<ApiResponse<EtsyListResponse<BuyerTaxonomyNodeProperty>>> GetPropertiesByBuyerTaxonomyIdAsync(long taxonomyId, CancellationToken cancellationToken = default);
    /// <summary>
    /// Executes the Get Buyer Taxonomy Nodes operation.
    /// </summary>
    Task<ApiResponse<EtsyListResponse<BuyerTaxonomyNode>>> GetBuyerTaxonomyNodesAsync(CancellationToken cancellationToken = default);
    /// <summary>
    /// Executes the Get Listing Image operation.
    /// </summary>
    Task<ApiResponse<ListingImage>> GetListingImageAsync(long listingId, long listingImageId, CancellationToken cancellationToken = default);
    /// <summary>
    /// Executes the Get Listing Images operation.
    /// </summary>
    Task<ApiResponse<EtsyListResponse<ListingImage>>> GetListingImagesAsync(long listingId, CancellationToken cancellationToken = default);
    /// <summary>
    /// Executes the Create Draft Listing operation.
    /// </summary>
    Task<ApiResponse<ShopListing>> CreateDraftListingAsync(string accessToken, long shopId, CreateDraftListingRequest listing, CancellationToken cancellationToken = default);
    /// <summary>
    /// Executes the Update Listing operation.
    /// </summary>
    Task<ApiResponse<ShopListing>> UpdateListingAsync(string accessToken, long shopId, long listingId, UpdateListingRequest listing, CancellationToken cancellationToken = default);
    /// <summary>
    /// Executes the Delete Listing operation.
    /// </summary>
    Task<ApiResponse<object>> DeleteListingAsync(string accessToken, long listingId, CancellationToken cancellationToken = default);
    /// <summary>
    /// Executes the Get Listings By Shop Return Policy operation.
    /// </summary>
    Task<ApiResponse<EtsyListResponse<ShopListingWithAssociations>>> GetListingsByShopReturnPolicyAsync(string accessToken, long shopId, long returnPolicyId, bool? legacy = null, CancellationToken cancellationToken = default);
    /// <summary>
    /// Executes the Get Listings Shipping By Listing Ids operation.
    /// </summary>
    Task<ApiResponse<EtsyListResponse<ShopListingWithAssociations>>> GetListingsShippingByListingIdsAsync(string accessToken, IReadOnlyCollection<long> listingIds, CancellationToken cancellationToken = default);
    /// <summary>
    /// Executes the Update Listing Property operation.
    /// </summary>
    Task<ApiResponse<ListingPropertyValue>> UpdateListingPropertyAsync(string accessToken, long shopId, long listingId, long propertyId, UpdateListingPropertyRequest property, CancellationToken cancellationToken = default);
    /// <summary>
    /// Executes the Delete Listing Property operation.
    /// </summary>
    Task<ApiResponse<object>> DeleteListingPropertyAsync(string accessToken, long shopId, long listingId, long propertyId, CancellationToken cancellationToken = default);
    /// <summary>
    /// Executes the Get All Listing Files operation.
    /// </summary>
    Task<ApiResponse<EtsyListResponse<ShopListingFile>>> GetAllListingFilesAsync(string accessToken, long shopId, long listingId, CancellationToken cancellationToken = default);
    /// <summary>
    /// Executes the Get Listing File operation.
    /// </summary>
    Task<ApiResponse<ShopListingFile>> GetListingFileAsync(string accessToken, long shopId, long listingId, long listingFileId, CancellationToken cancellationToken = default);
    /// <summary>
    /// Executes the Upload Listing File operation.
    /// </summary>
    Task<ApiResponse<ShopListingFile>> UploadListingFileAsync(string accessToken, long shopId, long listingId, ListingFileUploadRequest file, CancellationToken cancellationToken = default);
    /// <summary>
    /// Executes the Delete Listing File operation.
    /// </summary>
    Task<ApiResponse<object>> DeleteListingFileAsync(string accessToken, long shopId, long listingId, long listingFileId, CancellationToken cancellationToken = default);
    /// <summary>
    /// Executes the Upload Listing Image operation.
    /// </summary>
    Task<ApiResponse<ListingImage>> UploadListingImageAsync(string accessToken, long shopId, long listingId, ListingImageUploadRequest image, CancellationToken cancellationToken = default);
    /// <summary>
    /// Executes the Delete Listing Image operation.
    /// </summary>
    Task<ApiResponse<object>> DeleteListingImageAsync(string accessToken, long shopId, long listingId, long listingImageId, CancellationToken cancellationToken = default);
    /// <summary>
    /// Executes the Get Listing Inventory operation.
    /// </summary>
    Task<ApiResponse<ListingInventoryWithAssociations>> GetListingInventoryAsync(string accessToken, long listingId, bool? showDeleted = null, IReadOnlyCollection<ListingInclude>? includes = null, CancellationToken cancellationToken = default);
    /// <summary>
    /// Executes the Get Listings Inventory By Listing Ids operation.
    /// </summary>
    Task<ApiResponse<EtsyListResponse<ShopListingWithAssociations>>> GetListingsInventoryByListingIdsAsync(string accessToken, IReadOnlyCollection<long> listingIds, CancellationToken cancellationToken = default);
    /// <summary>
    /// Executes the Update Listing Inventory operation.
    /// </summary>
    Task<ApiResponse<ListingInventory>> UpdateListingInventoryAsync(string accessToken, long listingId, UpdateListingInventoryRequest inventory, string? maxVariationsSupported = null, CancellationToken cancellationToken = default);
    /// <summary>
    /// Executes the Get Listing Offering operation.
    /// </summary>
    Task<ApiResponse<ListingInventoryProductOffering>> GetListingOfferingAsync(long listingId, long productId, long productOfferingId, bool? legacy = null, CancellationToken cancellationToken = default);
    /// <summary>
    /// Executes the Get Listing Product operation.
    /// </summary>
    Task<ApiResponse<ListingInventoryProduct>> GetListingProductAsync(string accessToken, long listingId, long productId, bool? legacy = null, CancellationToken cancellationToken = default);
    /// <summary>
    /// Executes the Get Listing Personalization operation.
    /// </summary>
    Task<ApiResponse<ListingPersonalization>> GetListingPersonalizationAsync(long listingId, CancellationToken cancellationToken = default);
    /// <summary>
    /// Executes the Update Listing Personalization operation.
    /// </summary>
    Task<ApiResponse<ListingPersonalization>> UpdateListingPersonalizationAsync(string accessToken, long shopId, long listingId, ListingPersonalizationUpdateRequest personalization, bool? supportsMultiplePersonalizationQuestions = null, CancellationToken cancellationToken = default);
    /// <summary>
    /// Executes the Delete Listing Personalization operation.
    /// </summary>
    Task<ApiResponse<object>> DeleteListingPersonalizationAsync(string accessToken, long shopId, long listingId, CancellationToken cancellationToken = default);
    /// <summary>
    /// Executes the Get Listing Translation operation.
    /// </summary>
    Task<ApiResponse<ListingTranslation>> GetListingTranslationAsync(long shopId, long listingId, string language, CancellationToken cancellationToken = default);
    /// <summary>
    /// Executes the Create Listing Translation operation.
    /// </summary>
    Task<ApiResponse<ListingTranslation>> CreateListingTranslationAsync(string accessToken, long shopId, long listingId, string language, ListingTranslationRequest translation, CancellationToken cancellationToken = default);
    /// <summary>
    /// Executes the Update Listing Translation operation.
    /// </summary>
    Task<ApiResponse<ListingTranslation>> UpdateListingTranslationAsync(string accessToken, long shopId, long listingId, string language, ListingTranslationRequest translation, CancellationToken cancellationToken = default);
    /// <summary>
    /// Executes the Get Listing Variation Images operation.
    /// </summary>
    Task<ApiResponse<EtsyListResponse<ListingVariationImage>>> GetListingVariationImagesAsync(long shopId, long listingId, CancellationToken cancellationToken = default);
    /// <summary>
    /// Executes the Update Variation Images operation.
    /// </summary>
    Task<ApiResponse<EtsyListResponse<ListingVariationImage>>> UpdateVariationImagesAsync(string accessToken, long shopId, long listingId, IReadOnlyCollection<ListingVariationImage> variationImages, CancellationToken cancellationToken = default);
    /// <summary>
    /// Executes the Get Listing Video operation.
    /// </summary>
    Task<ApiResponse<ListingVideo>> GetListingVideoAsync(long listingId, long videoId, CancellationToken cancellationToken = default);
    /// <summary>
    /// Executes the Get Listing Videos operation.
    /// </summary>
    Task<ApiResponse<EtsyListResponse<ListingVideo>>> GetListingVideosAsync(long listingId, CancellationToken cancellationToken = default);
    /// <summary>
    /// Executes the Upload Listing Video operation.
    /// </summary>
    Task<ApiResponse<ListingVideo>> UploadListingVideoAsync(string accessToken, long shopId, long listingId, ListingVideoUploadRequest video, CancellationToken cancellationToken = default);
    /// <summary>
    /// Executes the Delete Listing Video operation.
    /// </summary>
    Task<ApiResponse<object>> DeleteListingVideoAsync(string accessToken, long shopId, long listingId, long videoId, CancellationToken cancellationToken = default);
}
