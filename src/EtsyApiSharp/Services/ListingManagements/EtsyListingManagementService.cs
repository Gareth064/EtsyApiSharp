using EtsyApiSharp.Helpers;
using EtsyApiSharp.Models;
using EtsyApiSharp.Models.Common;
using EtsyApiSharp.Models.Filters;
using EtsyApiSharp.Models.Listings.Enums;
using System.Globalization;
using System.Net.Http.Headers;

namespace EtsyApiSharp.Services.ListingManagements;

/// <summary>
/// Provides access to Etsy Open API v3 listing and taxonomy read endpoints.
/// </summary>
public class EtsyListingManagementService : IEtsyListingManagementService
{
    public const string HttpClientName = "EtsyApiSharp.Listings";

    private readonly string apiKey;
    private readonly IHttpClientFactory httpClientFactory;

    public EtsyListingManagementService(
        IHttpClientFactory httpClientFactory,
        string clientId,
        string sharedSecret)
    {
        ArgumentNullException.ThrowIfNull(httpClientFactory);

        if (string.IsNullOrWhiteSpace(clientId))
            throw new ArgumentException("An Etsy API key keystring is required.", nameof(clientId));

        if (string.IsNullOrWhiteSpace(sharedSecret))
            throw new ArgumentException("An Etsy API shared secret is required.", nameof(sharedSecret));

        this.httpClientFactory = httpClientFactory;
        apiKey = $"{clientId}:{sharedSecret}";
    }

    public Task<ApiResponse<EtsyListResponse<ShopListingWithAssociations>>> FindAllListingsActiveAsync(
        FindAllListingsActiveFilter? filter = null,
        CancellationToken cancellationToken = default)
    {
        ValidatePagination(filter);
        var query = CreateActiveListingsQuery(filter);
        return SendListAsync<ShopListingWithAssociations>(HttpMethod.Get, Url.ListingUrls.FindAllListingsActive(), query, null, cancellationToken);
    }

    public Task<ApiResponse<EtsyListResponse<ShopListingWithAssociations>>> FindAllActiveListingsByShopAsync(
        long shopId,
        FindAllActiveListingsByShopFilter? filter = null,
        CancellationToken cancellationToken = default)
    {
        ValidateId(shopId, nameof(shopId));
        ValidatePagination(filter);

        var query = CreatePaginationQuery(filter);
        if (filter?.SortOn is not null)
            query["sort_on"] = filter.SortOn.Value.ToString();
        if (filter?.SortOrder is not null)
            query["sort_order"] = filter.SortOrder.Value.ToString();
        AddString(query, "keywords", filter?.Keywords);

        return SendListAsync<ShopListingWithAssociations>(HttpMethod.Get, Url.ListingUrls.FindAllActiveListingsByShop(shopId), query, null, cancellationToken);
    }

    public Task<ApiResponse<EtsyListResponse<SellerTaxonomyNode>>> GetSellerTaxonomyNodesAsync(
        CancellationToken cancellationToken = default) =>
        SendListAsync<SellerTaxonomyNode>(HttpMethod.Get, Url.ListingUrls.GetSellerTaxonomyNodes(), null, null, cancellationToken);

    public Task<ApiResponse<EtsyListResponse<TaxonomyNodeProperty>>> GetPropertiesByTaxonomyIdAsync(
        long taxonomyId,
        CancellationToken cancellationToken = default)
    {
        ValidateId(taxonomyId, nameof(taxonomyId));
        return SendListAsync<TaxonomyNodeProperty>(HttpMethod.Get, Url.ListingUrls.GetPropertiesByTaxonomyId(taxonomyId), null, null, cancellationToken);
    }

    public Task<ApiResponse<EtsyListResponse<ShopListingWithAssociations>>> GetListingsByShopAsync(
        string accessToken,
        long shopId,
        IReadOnlyCollection<ListingInclude>? includes = null,
        GetListingsByShopFilter? filter = null,
        CancellationToken cancellationToken = default)
    {
        ValidateAccessToken(accessToken);
        ValidateId(shopId, nameof(shopId));
        ValidatePagination(filter);

        var query = CreatePaginationQuery(filter);
        if (filter is not null)
        {
            query["state"] = filter.State.ToString();
            query["sort_on"] = filter.SortOn.ToString();
            query["sort_order"] = filter.SortOrder.ToString();
        }
        AddIncludes(query, includes);

        return SendListAsync<ShopListingWithAssociations>(HttpMethod.Get, Url.ListingUrls.GetListingsByShop(shopId), query, accessToken, cancellationToken);
    }

    public Task<ApiResponse<ShopListingWithAssociations>> GetListingAsync(
        long listingId,
        IReadOnlyCollection<ListingInclude>? includes = null,
        string? language = null,
        bool? allowSuggestedTitle = null,
        CancellationToken cancellationToken = default)
    {
        ValidateId(listingId, nameof(listingId));
        var query = new Dictionary<string, string>();
        AddIncludes(query, includes);
        AddString(query, "language", language);
        AddBoolean(query, "allow_suggested_title", allowSuggestedTitle);

        return SendSingleAsync<ShopListingWithAssociations>(HttpMethod.Get, Url.ListingUrls.GetListing(listingId), query, null, cancellationToken);
    }

    public Task<ApiResponse<EtsyListResponse<ShopListingWithAssociations>>> GetListingsByListingIdsAsync(
        IReadOnlyCollection<long> listingIds,
        IReadOnlyCollection<ListingInclude>? includes = null,
        bool? legacy = null,
        string? currency = null,
        string? buyerCountry = null,
        CancellationToken cancellationToken = default)
    {
        ValidateIds(listingIds, nameof(listingIds));
        var query = new Dictionary<string, string>
        {
            ["listing_ids"] = string.Join(',', listingIds)
        };
        AddIncludes(query, includes);
        AddBoolean(query, "legacy", legacy);
        AddString(query, "currency", currency);
        AddString(query, "buyer_country", buyerCountry);

        return SendListAsync<ShopListingWithAssociations>(HttpMethod.Get, Url.ListingUrls.GetListingsByListingIds(), query, null, cancellationToken);
    }

    public Task<ApiResponse<EtsyListResponse<ShopListingWithAssociations>>> GetFeaturedListingsByShopAsync(
        long shopId,
        GetFeaturedListingsByShopFilter? filter = null,
        CancellationToken cancellationToken = default)
    {
        ValidateId(shopId, nameof(shopId));
        ValidatePagination(filter);
        var query = CreatePaginationQuery(filter);
        AddBoolean(query, "legacy", filter?.Legacy);

        return SendListAsync<ShopListingWithAssociations>(HttpMethod.Get, Url.ListingUrls.GetFeaturedListingsByShop(shopId), query, null, cancellationToken);
    }

    public Task<ApiResponse<ListingPropertyValue>> GetListingPropertyAsync(
        long listingId,
        long propertyId,
        CancellationToken cancellationToken = default)
    {
        ValidateId(listingId, nameof(listingId));
        ValidateId(propertyId, nameof(propertyId));
        return SendSingleAsync<ListingPropertyValue>(HttpMethod.Get, Url.ListingUrls.GetListingProperty(listingId, propertyId), null, null, cancellationToken);
    }

    public Task<ApiResponse<EtsyListResponse<ListingPropertyValue>>> GetListingPropertiesAsync(
        long shopId,
        long listingId,
        CancellationToken cancellationToken = default)
    {
        ValidateId(shopId, nameof(shopId));
        ValidateId(listingId, nameof(listingId));
        return SendListAsync<ListingPropertyValue>(HttpMethod.Get, Url.ListingUrls.GetListingProperties(shopId, listingId), null, null, cancellationToken);
    }

    public Task<ApiResponse<EtsyListResponse<ShopListingWithAssociations>>> GetListingsByShopReceiptAsync(
        string accessToken,
        long shopId,
        long receiptId,
        GetListingsByShopReceiptFilter? filter = null,
        CancellationToken cancellationToken = default)
    {
        ValidateAccessToken(accessToken);
        ValidateId(shopId, nameof(shopId));
        ValidateId(receiptId, nameof(receiptId));
        ValidatePagination(filter);
        var query = CreatePaginationQuery(filter);
        AddBoolean(query, "legacy", filter?.Legacy);

        return SendListAsync<ShopListingWithAssociations>(HttpMethod.Get, Url.ListingUrls.GetListingsByShopReceipt(shopId, receiptId), query, accessToken, cancellationToken);
    }

    public Task<ApiResponse<EtsyListResponse<ShopListingWithAssociations>>> GetListingsByShopSectionIdAsync(
        long shopId,
        IReadOnlyCollection<long> sectionIds,
        GetListingsByShopSectionIdFilter? filter = null,
        CancellationToken cancellationToken = default)
    {
        ValidateId(shopId, nameof(shopId));
        ValidateIds(sectionIds, nameof(sectionIds));
        ValidatePagination(filter);
        var query = CreatePaginationQuery(filter);
        query["shop_section_ids"] = string.Join(',', sectionIds);
        if (filter is not null)
        {
            query["sort_on"] = filter.SortOn.ToString();
            query["sort_order"] = filter.SortOrder.ToString();
        }
        AddBoolean(query, "legacy", filter?.Legacy);

        return SendListAsync<ShopListingWithAssociations>(HttpMethod.Get, Url.ListingUrls.GetListingsByShopSectionId(shopId), query, null, cancellationToken);
    }

    public Task<ApiResponse<EtsyListResponse<BuyerTaxonomyNodeProperty>>> GetPropertiesByBuyerTaxonomyIdAsync(
        long taxonomyId,
        CancellationToken cancellationToken = default)
    {
        ValidateId(taxonomyId, nameof(taxonomyId));
        return SendListAsync<BuyerTaxonomyNodeProperty>(HttpMethod.Get, Url.ListingUrls.GetPropertiesByBuyerTaxonomyId(taxonomyId), null, null, cancellationToken);
    }

    public Task<ApiResponse<EtsyListResponse<BuyerTaxonomyNode>>> GetBuyerTaxonomyNodesAsync(
        CancellationToken cancellationToken = default) =>
        SendListAsync<BuyerTaxonomyNode>(HttpMethod.Get, Url.ListingUrls.GetBuyerTaxonomyNodes(), null, null, cancellationToken);

    public Task<ApiResponse<ListingImage>> GetListingImageAsync(
        long listingId,
        long listingImageId,
        CancellationToken cancellationToken = default)
    {
        ValidateId(listingId, nameof(listingId));
        ValidateId(listingImageId, nameof(listingImageId));
        return SendSingleAsync<ListingImage>(HttpMethod.Get, Url.ListingUrls.GetListingImage(listingId, listingImageId), null, null, cancellationToken);
    }

    public Task<ApiResponse<EtsyListResponse<ListingImage>>> GetListingImagesAsync(
        long listingId,
        CancellationToken cancellationToken = default)
    {
        ValidateId(listingId, nameof(listingId));
        return SendListAsync<ListingImage>(HttpMethod.Get, Url.ListingUrls.GetListingImages(listingId), null, null, cancellationToken);
    }

    public Task<ApiResponse<ShopListing>> CreateDraftListingAsync(string accessToken, long shopId, CreateDraftListingRequest listing, CancellationToken cancellationToken = default)
    {
        ValidateAccessToken(accessToken);
        ValidateId(shopId, nameof(shopId));
        ArgumentNullException.ThrowIfNull(listing);
        if (listing.Quantity < 1 || listing.Price <= 0 || listing.TaxonomyId < 1 || string.IsNullOrWhiteSpace(listing.Title) || string.IsNullOrWhiteSpace(listing.Description) || string.IsNullOrWhiteSpace(listing.WhenMade))
            throw new ArgumentException("Quantity, title, description, price, who made, when made, and taxonomy ID are required.", nameof(listing));
        return SendSingleAsync<ShopListing>(HttpMethod.Post, Url.ListingUrls.CreateDraftListing(shopId), null, accessToken, CreateFormContent(listing), cancellationToken);
    }

    public Task<ApiResponse<ShopListing>> UpdateListingAsync(string accessToken, long shopId, long listingId, UpdateListingRequest listing, CancellationToken cancellationToken = default)
    {
        ValidateAccessToken(accessToken);
        ValidateId(shopId, nameof(shopId));
        ValidateId(listingId, nameof(listingId));
        ValidateUpdate(listing, nameof(listing));
        return SendSingleAsync<ShopListing>(HttpMethod.Patch, Url.ListingUrls.UpdateListing(shopId, listingId), null, accessToken, CreateFormContent(listing), cancellationToken);
    }

    public Task<ApiResponse<object>> DeleteListingAsync(string accessToken, long listingId, CancellationToken cancellationToken = default) =>
        SendDeleteAsync(accessToken, Url.ListingUrls.DeleteListing(listingId), nameof(listingId), listingId, cancellationToken);

    public Task<ApiResponse<EtsyListResponse<ShopListingWithAssociations>>> GetListingsByShopReturnPolicyAsync(string accessToken, long shopId, long returnPolicyId, bool? legacy = null, CancellationToken cancellationToken = default)
    {
        ValidateAccessToken(accessToken);
        ValidateId(shopId, nameof(shopId));
        ValidateId(returnPolicyId, nameof(returnPolicyId));
        var query = new Dictionary<string, string>();
        AddBoolean(query, "legacy", legacy);
        return SendListAsync<ShopListingWithAssociations>(HttpMethod.Get, Url.ListingUrls.GetListingsByShopReturnPolicy(shopId, returnPolicyId), query, accessToken, cancellationToken);
    }

    public Task<ApiResponse<EtsyListResponse<ShopListingWithAssociations>>> GetListingsShippingByListingIdsAsync(string accessToken, IReadOnlyCollection<long> listingIds, CancellationToken cancellationToken = default)
    {
        ValidateAccessToken(accessToken);
        ValidateIds(listingIds, nameof(listingIds));
        return SendListAsync<ShopListingWithAssociations>(HttpMethod.Get, Url.ListingUrls.GetListingsShippingByListingIds(), CreateIdsQuery(listingIds), accessToken, cancellationToken);
    }

    public Task<ApiResponse<ListingPropertyValue>> UpdateListingPropertyAsync(string accessToken, long shopId, long listingId, long propertyId, UpdateListingPropertyRequest property, CancellationToken cancellationToken = default)
    {
        ValidateAccessToken(accessToken);
        ValidateId(shopId, nameof(shopId));
        ValidateId(listingId, nameof(listingId));
        ValidateId(propertyId, nameof(propertyId));
        ArgumentNullException.ThrowIfNull(property);
        if (property.ValueIds.Count == 0 || property.Values.Count == 0)
            throw new ArgumentException("At least one property value ID and value are required.", nameof(property));
        return SendSingleAsync<ListingPropertyValue>(HttpMethod.Put, Url.ListingUrls.UpdateListingProperty(shopId, listingId, propertyId), null, accessToken, CreateFormContent(property), cancellationToken);
    }

    public Task<ApiResponse<object>> DeleteListingPropertyAsync(string accessToken, long shopId, long listingId, long propertyId, CancellationToken cancellationToken = default)
    {
        ValidateAccessToken(accessToken);
        ValidateId(shopId, nameof(shopId));
        ValidateId(listingId, nameof(listingId));
        return SendDeleteAsync(accessToken, Url.ListingUrls.UpdateListingProperty(shopId, listingId, propertyId), nameof(propertyId), propertyId, cancellationToken);
    }

    public Task<ApiResponse<EtsyListResponse<ShopListingFile>>> GetAllListingFilesAsync(string accessToken, long shopId, long listingId, CancellationToken cancellationToken = default)
    {
        ValidateAccessToken(accessToken);
        ValidateId(shopId, nameof(shopId));
        ValidateId(listingId, nameof(listingId));
        return SendListAsync<ShopListingFile>(HttpMethod.Get, Url.ListingUrls.GetAllListingFiles(shopId, listingId), null, accessToken, cancellationToken);
    }

    public Task<ApiResponse<ShopListingFile>> GetListingFileAsync(string accessToken, long shopId, long listingId, long listingFileId, CancellationToken cancellationToken = default)
    {
        ValidateAccessToken(accessToken);
        ValidateId(shopId, nameof(shopId));
        ValidateId(listingId, nameof(listingId));
        ValidateId(listingFileId, nameof(listingFileId));
        return SendSingleAsync<ShopListingFile>(HttpMethod.Get, Url.ListingUrls.GetListingFile(shopId, listingId, listingFileId), null, accessToken, cancellationToken);
    }

    public Task<ApiResponse<ShopListingFile>> UploadListingFileAsync(string accessToken, long shopId, long listingId, ListingFileUploadRequest file, CancellationToken cancellationToken = default)
    {
        ValidateAccessToken(accessToken);
        ValidateId(shopId, nameof(shopId));
        ValidateId(listingId, nameof(listingId));
        ArgumentNullException.ThrowIfNull(file);
        ValidateUpload(file.File, file.FileName, nameof(file));
        var content = new MultipartFormDataContent();
        content.Add(new StreamContent(file.File), "file", file.FileName);
        AddMultipartValue(content, "listing_file_id", file.ListingFileId);
        AddMultipartValue(content, "rank", file.Rank);
        return SendSingleAsync<ShopListingFile>(HttpMethod.Post, Url.ListingUrls.GetAllListingFiles(shopId, listingId), null, accessToken, content, cancellationToken);
    }

    public Task<ApiResponse<object>> DeleteListingFileAsync(string accessToken, long shopId, long listingId, long listingFileId, CancellationToken cancellationToken = default)
    {
        ValidateAccessToken(accessToken);
        ValidateId(shopId, nameof(shopId));
        ValidateId(listingId, nameof(listingId));
        return SendDeleteAsync(accessToken, Url.ListingUrls.GetListingFile(shopId, listingId, listingFileId), nameof(listingFileId), listingFileId, cancellationToken);
    }

    public Task<ApiResponse<ListingImage>> UploadListingImageAsync(string accessToken, long shopId, long listingId, ListingImageUploadRequest image, CancellationToken cancellationToken = default)
    {
        ValidateAccessToken(accessToken);
        ValidateId(shopId, nameof(shopId));
        ValidateId(listingId, nameof(listingId));
        ArgumentNullException.ThrowIfNull(image);
        ValidateUpload(image.Image, image.FileName, nameof(image));
        var content = new MultipartFormDataContent();
        content.Add(new StreamContent(image.Image), "image", image.FileName);
        AddMultipartValue(content, "listing_image_id", image.ListingImageId);
        AddMultipartValue(content, "rank", image.Rank);
        AddMultipartValue(content, "overwrite", image.Overwrite);
        AddMultipartValue(content, "is_watermarked", image.IsWatermarked);
        AddMultipartValue(content, "alt_text", image.AltText);
        return SendSingleAsync<ListingImage>(HttpMethod.Post, Url.ListingUrls.UploadListingImage(shopId, listingId), null, accessToken, content, cancellationToken);
    }

    public Task<ApiResponse<object>> DeleteListingImageAsync(string accessToken, long shopId, long listingId, long listingImageId, CancellationToken cancellationToken = default)
    {
        ValidateAccessToken(accessToken);
        ValidateId(shopId, nameof(shopId));
        ValidateId(listingId, nameof(listingId));
        return SendDeleteAsync(accessToken, Url.ListingUrls.DeleteListingImage(shopId, listingId, listingImageId), nameof(listingImageId), listingImageId, cancellationToken);
    }

    public Task<ApiResponse<ListingInventoryWithAssociations>> GetListingInventoryAsync(string accessToken, long listingId, bool? showDeleted = null, IReadOnlyCollection<ListingInclude>? includes = null, CancellationToken cancellationToken = default)
    {
        ValidateAccessToken(accessToken);
        ValidateId(listingId, nameof(listingId));
        var query = new Dictionary<string, string>();
        AddBoolean(query, "show_deleted", showDeleted);
        AddIncludes(query, includes);
        return SendSingleAsync<ListingInventoryWithAssociations>(HttpMethod.Get, Url.ListingUrls.GetListingInventory(listingId), query, accessToken, cancellationToken);
    }

    public Task<ApiResponse<EtsyListResponse<ShopListingWithAssociations>>> GetListingsInventoryByListingIdsAsync(string accessToken, IReadOnlyCollection<long> listingIds, CancellationToken cancellationToken = default)
    {
        ValidateAccessToken(accessToken);
        ValidateIds(listingIds, nameof(listingIds));
        return SendListAsync<ShopListingWithAssociations>(HttpMethod.Get, Url.ListingUrls.GetListingsInventoryByListingIds(), CreateIdsQuery(listingIds), accessToken, cancellationToken);
    }

    public Task<ApiResponse<ListingInventory>> UpdateListingInventoryAsync(string accessToken, long listingId, UpdateListingInventoryRequest inventory, string? maxVariationsSupported = null, CancellationToken cancellationToken = default)
    {
        ValidateAccessToken(accessToken);
        ValidateId(listingId, nameof(listingId));
        ArgumentNullException.ThrowIfNull(inventory);
        if (inventory.Products.Count == 0)
            throw new ArgumentException("At least one listing inventory product is required.", nameof(inventory));
        if (maxVariationsSupported is not null && maxVariationsSupported is not ("2" or "3"))
            throw new ArgumentException("The maximum variations value must be '2' or '3'.", nameof(maxVariationsSupported));
        var query = new Dictionary<string, string>();
        AddString(query, "max_variations_supported", maxVariationsSupported);
        return SendSingleAsync<ListingInventory>(HttpMethod.Put, Url.ListingUrls.GetListingInventory(listingId), query, accessToken, HttpContentHelper.CreateJsonContent(inventory, true), cancellationToken);
    }

    public Task<ApiResponse<ListingInventoryProductOffering>> GetListingOfferingAsync(long listingId, long productId, long productOfferingId, bool? legacy = null, CancellationToken cancellationToken = default)
    {
        ValidateId(listingId, nameof(listingId));
        ValidateId(productId, nameof(productId));
        ValidateId(productOfferingId, nameof(productOfferingId));
        var query = new Dictionary<string, string>();
        AddBoolean(query, "legacy", legacy);
        return SendSingleAsync<ListingInventoryProductOffering>(HttpMethod.Get, Url.ListingUrls.GetListingOffering(listingId, productId, productOfferingId), query, null, cancellationToken);
    }

    public Task<ApiResponse<ListingInventoryProduct>> GetListingProductAsync(string accessToken, long listingId, long productId, bool? legacy = null, CancellationToken cancellationToken = default)
    {
        ValidateAccessToken(accessToken);
        ValidateId(listingId, nameof(listingId));
        ValidateId(productId, nameof(productId));
        var query = new Dictionary<string, string>();
        AddBoolean(query, "legacy", legacy);
        return SendSingleAsync<ListingInventoryProduct>(HttpMethod.Get, Url.ListingUrls.GetListingProduct(listingId, productId), query, accessToken, cancellationToken);
    }

    public Task<ApiResponse<ListingPersonalization>> GetListingPersonalizationAsync(long listingId, CancellationToken cancellationToken = default)
    {
        ValidateId(listingId, nameof(listingId));
        return SendSingleAsync<ListingPersonalization>(HttpMethod.Get, Url.ListingUrls.GetListingPersonalization(listingId), null, null, cancellationToken);
    }

    public Task<ApiResponse<ListingPersonalization>> UpdateListingPersonalizationAsync(string accessToken, long shopId, long listingId, ListingPersonalizationUpdateRequest personalization, bool? supportsMultiplePersonalizationQuestions = null, CancellationToken cancellationToken = default)
    {
        ValidateAccessToken(accessToken);
        ValidateId(shopId, nameof(shopId));
        ValidateId(listingId, nameof(listingId));
        ArgumentNullException.ThrowIfNull(personalization);
        if (personalization.PersonalizationQuestions.Count == 0)
            throw new ArgumentException("At least one personalization question is required.", nameof(personalization));
        var query = new Dictionary<string, string>();
        AddBoolean(query, "supports_multiple_personalization_questions", supportsMultiplePersonalizationQuestions);
        return SendSingleAsync<ListingPersonalization>(HttpMethod.Put, Url.ListingUrls.UpdateListingPersonalization(shopId, listingId), query, accessToken, HttpContentHelper.CreateJsonContent(personalization, true), cancellationToken);
    }

    public Task<ApiResponse<object>> DeleteListingPersonalizationAsync(string accessToken, long shopId, long listingId, CancellationToken cancellationToken = default)
    {
        ValidateAccessToken(accessToken);
        ValidateId(shopId, nameof(shopId));
        return SendDeleteAsync(accessToken, Url.ListingUrls.UpdateListingPersonalization(shopId, listingId), nameof(listingId), listingId, cancellationToken);
    }

    public Task<ApiResponse<ListingTranslation>> GetListingTranslationAsync(long shopId, long listingId, string language, CancellationToken cancellationToken = default)
    {
        ValidateId(shopId, nameof(shopId));
        ValidateId(listingId, nameof(listingId));
        ValidateLanguage(language);
        return SendSingleAsync<ListingTranslation>(HttpMethod.Get, Url.ListingUrls.GetListingTranslation(shopId, listingId, language), null, null, cancellationToken);
    }

    public Task<ApiResponse<ListingTranslation>> CreateListingTranslationAsync(string accessToken, long shopId, long listingId, string language, ListingTranslationRequest translation, CancellationToken cancellationToken = default) =>
        SendListingTranslationAsync(HttpMethod.Post, accessToken, shopId, listingId, language, translation, cancellationToken);

    public Task<ApiResponse<ListingTranslation>> UpdateListingTranslationAsync(string accessToken, long shopId, long listingId, string language, ListingTranslationRequest translation, CancellationToken cancellationToken = default) =>
        SendListingTranslationAsync(HttpMethod.Put, accessToken, shopId, listingId, language, translation, cancellationToken);

    public Task<ApiResponse<EtsyListResponse<ListingVariationImage>>> GetListingVariationImagesAsync(long shopId, long listingId, CancellationToken cancellationToken = default)
    {
        ValidateId(shopId, nameof(shopId));
        ValidateId(listingId, nameof(listingId));
        return SendListAsync<ListingVariationImage>(HttpMethod.Get, Url.ListingUrls.GetListingVariationImages(shopId, listingId), null, null, cancellationToken);
    }

    public Task<ApiResponse<EtsyListResponse<ListingVariationImage>>> UpdateVariationImagesAsync(string accessToken, long shopId, long listingId, IReadOnlyCollection<ListingVariationImage> variationImages, CancellationToken cancellationToken = default)
    {
        ValidateAccessToken(accessToken);
        ValidateId(shopId, nameof(shopId));
        ValidateId(listingId, nameof(listingId));
        ArgumentNullException.ThrowIfNull(variationImages);
        if (variationImages.Count == 0)
            throw new ArgumentException("At least one variation image is required.", nameof(variationImages));
        var body = new { variation_images = variationImages };
        return SendListAsync<ListingVariationImage>(HttpMethod.Post, Url.ListingUrls.GetListingVariationImages(shopId, listingId), null, accessToken, HttpContentHelper.CreateJsonContent(body, true), cancellationToken);
    }

    public Task<ApiResponse<ListingVideo>> GetListingVideoAsync(long listingId, long videoId, CancellationToken cancellationToken = default)
    {
        ValidateId(listingId, nameof(listingId));
        ValidateId(videoId, nameof(videoId));
        return SendSingleAsync<ListingVideo>(HttpMethod.Get, Url.ListingUrls.GetListingVideo(listingId, videoId), null, null, cancellationToken);
    }

    public Task<ApiResponse<EtsyListResponse<ListingVideo>>> GetListingVideosAsync(long listingId, CancellationToken cancellationToken = default)
    {
        ValidateId(listingId, nameof(listingId));
        return SendListAsync<ListingVideo>(HttpMethod.Get, Url.ListingUrls.GetListingVideos(listingId), null, null, cancellationToken);
    }

    public Task<ApiResponse<ListingVideo>> UploadListingVideoAsync(string accessToken, long shopId, long listingId, ListingVideoUploadRequest video, CancellationToken cancellationToken = default)
    {
        ValidateAccessToken(accessToken);
        ValidateId(shopId, nameof(shopId));
        ValidateId(listingId, nameof(listingId));
        ArgumentNullException.ThrowIfNull(video);
        if (video.VideoId is null && (video.Video is null || !video.Video.CanRead || string.IsNullOrWhiteSpace(video.FileName)))
            throw new ArgumentException("An existing video ID or a readable video stream and file name are required.", nameof(video));
        var content = new MultipartFormDataContent();
        AddMultipartValue(content, "video_id", video.VideoId);
        if (video.Video is not null)
            content.Add(new StreamContent(video.Video), "video", video.FileName ?? "video");
        return SendSingleAsync<ListingVideo>(HttpMethod.Post, Url.ListingUrls.UploadListingVideo(shopId, listingId), null, accessToken, content, cancellationToken);
    }

    public Task<ApiResponse<object>> DeleteListingVideoAsync(string accessToken, long shopId, long listingId, long videoId, CancellationToken cancellationToken = default)
    {
        ValidateAccessToken(accessToken);
        ValidateId(shopId, nameof(shopId));
        ValidateId(listingId, nameof(listingId));
        return SendDeleteAsync(accessToken, Url.ListingUrls.DeleteListingVideo(shopId, listingId, videoId), nameof(videoId), videoId, cancellationToken);
    }

    private Task<ApiResponse<EtsyListResponse<T>>> SendListAsync<T>(HttpMethod method, string relativeUrl, IReadOnlyDictionary<string, string>? query, string? accessToken, CancellationToken cancellationToken) =>
        SendListAsync<T>(method, relativeUrl, query, accessToken, null, cancellationToken);

    private Task<ApiResponse<EtsyListResponse<T>>> SendListAsync<T>(HttpMethod method, string relativeUrl, IReadOnlyDictionary<string, string>? query, string? accessToken, HttpContent? content, CancellationToken cancellationToken) =>
        SendAsync(method, relativeUrl, query, accessToken, content, cancellationToken, EtsyResponseParser.ParseResponseOfList<T>);

    private Task<ApiResponse<T>> SendSingleAsync<T>(HttpMethod method, string relativeUrl, IReadOnlyDictionary<string, string>? query, string? accessToken, CancellationToken cancellationToken) =>
        SendSingleAsync<T>(method, relativeUrl, query, accessToken, null, cancellationToken);

    private Task<ApiResponse<T>> SendSingleAsync<T>(HttpMethod method, string relativeUrl, IReadOnlyDictionary<string, string>? query, string? accessToken, HttpContent? content, CancellationToken cancellationToken) =>
        SendAsync(method, relativeUrl, query, accessToken, content, cancellationToken, EtsyResponseParser.ParseResponseOfSingle<T>);

    private async Task<ApiResponse<TResponse>> SendAsync<TResponse>(
        HttpMethod method,
        string relativeUrl,
        IReadOnlyDictionary<string, string>? query,
        string? accessToken,
        HttpContent? content,
        CancellationToken cancellationToken,
        Func<HttpResponseMessage, CancellationToken, Task<ApiResponse<TResponse>>> parseResponse)
    {
        using var request = new HttpRequestMessage(method, BuildUri(relativeUrl, query))
        {
            Content = content
        };
        if (accessToken is not null)
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        request.Headers.Add("x-api-key", apiKey);

        var httpClient = httpClientFactory.CreateClient(HttpClientName);
        using var response = await httpClient.SendAsync(request, cancellationToken).ConfigureAwait(false);
        response.RequestMessage ??= request;
        return await parseResponse(response, cancellationToken).ConfigureAwait(false);
    }

    private Task<ApiResponse<object>> SendDeleteAsync(string accessToken, string relativeUrl, string idName, long id, CancellationToken cancellationToken)
    {
        ValidateId(id, idName);
        return SendSingleAsync<object>(HttpMethod.Delete, relativeUrl, null, accessToken, cancellationToken);
    }

    private Task<ApiResponse<ListingTranslation>> SendListingTranslationAsync(HttpMethod method, string accessToken, long shopId, long listingId, string language, ListingTranslationRequest translation, CancellationToken cancellationToken)
    {
        ValidateAccessToken(accessToken);
        ValidateId(shopId, nameof(shopId));
        ValidateId(listingId, nameof(listingId));
        ValidateLanguage(language);
        ArgumentNullException.ThrowIfNull(translation);
        if (string.IsNullOrWhiteSpace(translation.Title) || string.IsNullOrWhiteSpace(translation.Description))
            throw new ArgumentException("A translation title and description are required.", nameof(translation));
        return SendSingleAsync<ListingTranslation>(method, Url.ListingUrls.GetListingTranslation(shopId, listingId, language), null, accessToken, CreateFormContent(translation), cancellationToken);
    }

    private static Dictionary<string, string> CreateActiveListingsQuery(FindAllListingsActiveFilter? filter)
    {
        var query = CreatePaginationQuery(filter);
        if (filter is null)
            return query;

        if (filter.SortOn is not null)
            query["sort_on"] = filter.SortOn.Value.ToString();
        if (filter.SortOrder is not null)
            query["sort_order"] = filter.SortOrder.Value.ToString();
        AddString(query, "keywords", filter.Keywords);
        AddDecimal(query, "min_price", filter.MinPrice);
        AddDecimal(query, "max_price", filter.MaxPrice);
        if (filter.TaxonomyId is not null)
            query["taxonomy_id"] = filter.TaxonomyId.Value.ToString(CultureInfo.InvariantCulture);
        AddString(query, "shop_location", filter.ShopLocation);
        AddBoolean(query, "is_safe", filter.IsSafe);
        AddString(query, "currency", filter.Currency);
        AddString(query, "buyer_country", filter.BuyerCountry);
        return query;
    }

    private static Dictionary<string, string> CreatePaginationQuery(EtsyFilterBase? filter)
    {
        var query = new Dictionary<string, string>();
        if (filter is null)
            return query;
        if (filter.Limit != 25)
            query["limit"] = filter.Limit.ToString(CultureInfo.InvariantCulture);
        if (filter.Offset != 0)
            query["offset"] = filter.Offset.ToString(CultureInfo.InvariantCulture);
        return query;
    }

    private static Dictionary<string, string> CreateIdsQuery(IReadOnlyCollection<long> ids) => new()
    {
        ["listing_ids"] = string.Join(',', ids)
    };

    private static FormUrlEncodedContent CreateFormContent(object request)
    {
        var values = new List<KeyValuePair<string, string>>();
        foreach (var property in request.GetType().GetProperties())
        {
            var value = property.GetValue(request);
            if (value is null)
                continue;
            var serializedValue = value is System.Collections.IEnumerable collection and not string
                ? string.Join(',', collection.Cast<object>().Select(SerializeValue))
                : SerializeValue(value);
            values.Add(new KeyValuePair<string, string>(ToSnakeCase(property.Name), serializedValue));
        }
        return new FormUrlEncodedContent(values);
    }

    private static string SerializeValue(object value) => value switch
    {
        bool boolean => boolean ? "true" : "false",
        IFormattable formattable => formattable.ToString(null, CultureInfo.InvariantCulture),
        _ => value.ToString() ?? string.Empty
    };

    private static string ToSnakeCase(string value) => string.Concat(value.Select((character, index) =>
        index > 0 && char.IsUpper(character) ? $"_{char.ToLowerInvariant(character)}" : char.ToLowerInvariant(character).ToString()));

    private static void AddMultipartValue(MultipartFormDataContent content, string name, object? value)
    {
        if (value is not null)
            content.Add(new StringContent(SerializeValue(value)), name);
    }

    private static Uri BuildUri(string relativeUrl, IReadOnlyDictionary<string, string>? query)
    {
        var uriBuilder = new UriBuilder($"{Url.BaseUrls.BaseApiUrl}{relativeUrl}");
        if (query is { Count: > 0 })
        {
            uriBuilder.Query = string.Join("&", query.Select(parameter =>
                $"{Uri.EscapeDataString(parameter.Key)}={Uri.EscapeDataString(parameter.Value)}"));
        }
        return uriBuilder.Uri;
    }

    private static void AddIncludes(Dictionary<string, string> query, IReadOnlyCollection<ListingInclude>? includes)
    {
        if (includes is { Count: > 0 })
            query["includes"] = string.Join(',', includes.Select(include => include.ToString().ToLowerInvariant()));
    }

    private static void AddString(Dictionary<string, string> query, string name, string? value)
    {
        if (!string.IsNullOrWhiteSpace(value))
            query[name] = value;
    }

    private static void AddBoolean(Dictionary<string, string> query, string name, bool? value)
    {
        if (value is not null)
            query[name] = value.Value ? "true" : "false";
    }

    private static void AddDecimal(Dictionary<string, string> query, string name, double? value)
    {
        if (value is not null)
            query[name] = value.Value.ToString(CultureInfo.InvariantCulture);
    }

    private static void ValidatePagination(EtsyFilterBase? filter)
    {
        if (filter is null)
            return;
        if (filter.Limit is < 1 or > 100)
            throw new ArgumentOutOfRangeException(nameof(filter), "The limit must be between 1 and 100.");
        if (filter.Offset < 0)
            throw new ArgumentOutOfRangeException(nameof(filter), "The offset cannot be negative.");
    }

    private static void ValidateAccessToken(string accessToken)
    {
        if (string.IsNullOrWhiteSpace(accessToken))
            throw new ArgumentException("An Etsy OAuth access token is required.", nameof(accessToken));
    }

    private static void ValidateId(long id, string parameterName)
    {
        if (id < 1)
            throw new ArgumentOutOfRangeException(parameterName, "Etsy IDs must be greater than zero.");
    }

    private static void ValidateIds(IReadOnlyCollection<long> ids, string parameterName)
    {
        ArgumentNullException.ThrowIfNull(ids);
        if (ids.Count == 0 || ids.Any(id => id < 1))
            throw new ArgumentException("At least one positive Etsy ID is required.", parameterName);
    }

    private static void ValidateUpdate(object update, string parameterName)
    {
        ArgumentNullException.ThrowIfNull(update);
        if (update.GetType().GetProperties().All(property => property.GetValue(update) is null))
            throw new ArgumentException("At least one value must be supplied.", parameterName);
    }

    private static void ValidateUpload(Stream? stream, string? fileName, string parameterName)
    {
        if (stream is null || !stream.CanRead || string.IsNullOrWhiteSpace(fileName))
            throw new ArgumentException("A readable stream and file name are required.", parameterName);
    }

    private static void ValidateLanguage(string language)
    {
        if (string.IsNullOrWhiteSpace(language))
            throw new ArgumentException("An IETF language tag is required.", nameof(language));
    }
}
