namespace EtsyApiSharp.Models;
/// <summary>
/// Represents Url.
/// </summary>

public class Url
{
    /// <summary>
    /// Represents Base Urls.
    /// </summary>
    public static class BaseUrls
    {
        /// <summary>
        /// Gets or sets the Base Auth Url.
        /// </summary>
        public static readonly string BaseAuthUrl = "https://www.etsy.com/oauth/connect";
        /// <summary>
        /// Gets or sets the Base Token Url.
        /// </summary>
        public static readonly string BaseTokenUrl = "https://api.etsy.com/v3/public/oauth/token";
        /// <summary>
        /// Gets or sets the Base Api Url.
        /// </summary>
        public static readonly string BaseApiUrl = "https://openapi.etsy.com";
    }
    /// <summary>
    /// Represents Receipt Urls.
    /// </summary>

    public static class ReceiptUrls
    {
        /// <summary>
        /// Executes the Get Shop Receipt operation.
        /// </summary>
        public static string GetShopReceipt(long shopId, long receiptId) =>
            $"/v3/application/shops/{shopId}/receipts/{receiptId}";
        /// <summary>
        /// Executes the Get Shop Receipts operation.
        /// </summary>

        public static string GetShopReceipts(long shopId) =>
            $"/v3/application/shops/{shopId}/receipts";
        /// <summary>
        /// Executes the Get Shop Receipt Transaction operation.
        /// </summary>

        public static string GetShopReceiptTransaction(long shopId, long transactionId) =>
            $"/v3/application/shops/{shopId}/transactions/{transactionId}";
        /// <summary>
        /// Executes the Get Shop Receipt Transactions By Listing operation.
        /// </summary>

        public static string GetShopReceiptTransactionsByListing(long shopId, long listingId) =>
            $"/v3/application/shops/{shopId}/listings/{listingId}/transactions";
        /// <summary>
        /// Executes the Get Shop Receipt Transactions By Receipt operation.
        /// </summary>

        public static string GetShopReceiptTransactionsByReceipt(long shopId, long receiptId) =>
            $"/v3/application/shops/{shopId}/receipts/{receiptId}/transactions";
        /// <summary>
        /// Executes the Get Shop Receipt Transactions By Shop operation.
        /// </summary>

        public static string GetShopReceiptTransactionsByShop(long shopId) =>
            $"/v3/application/shops/{shopId}/transactions";
        /// <summary>
        /// Executes the Create Receipt Shipment operation.
        /// </summary>

        public static string CreateReceiptShipment(long shopId, long receiptId) =>
            $"/v3/application/shops/{shopId}/receipts/{receiptId}/tracking";
    }
    /// <summary>
    /// Represents Payment Urls.
    /// </summary>

    public static class PaymentUrls
    {
        /// <summary>
        /// Executes the Get Shop Payment Account Ledger Entry operation.
        /// </summary>
        public static string GetShopPaymentAccountLedgerEntry(long shopId, long ledgerEntryId) =>
            $"/v3/application/shops/{shopId}/payment-account/ledger-entries/{ledgerEntryId}";
        /// <summary>
        /// Executes the Get Shop Payment Account Ledger Entries operation.
        /// </summary>

        public static string GetShopPaymentAccountLedgerEntries(long shopId) =>
            $"/v3/application/shops/{shopId}/payment-account/ledger-entries";
        /// <summary>
        /// Executes the Get Payment Account Ledger Entry Payments operation.
        /// </summary>

        public static string GetPaymentAccountLedgerEntryPayments(long shopId) =>
            $"/v3/application/shops/{shopId}/payment-account/ledger-entries/payments";
        /// <summary>
        /// Executes the Get Shop Payment By Receipt Id operation.
        /// </summary>

        public static string GetShopPaymentByReceiptId(long shopId, long receiptId) =>
            $"/v3/application/shops/{shopId}/receipts/{receiptId}/payments";
        /// <summary>
        /// Executes the Get Payments operation.
        /// </summary>

        public static string GetPayments(long shopId) =>
            $"/v3/application/shops/{shopId}/payments";
    }
    /// <summary>
    /// Represents Review Urls.
    /// </summary>

    public static class ReviewUrls
    {
        /// <summary>
        /// Executes the Get Reviews By Listing operation.
        /// </summary>
        public static string GetReviewsByListing(long listingId) =>
            $"/v3/application/listings/{listingId}/reviews";
        /// <summary>
        /// Executes the Get Reviews By Shop operation.
        /// </summary>

        public static string GetReviewsByShop(long shopId) =>
            $"/v3/application/shops/{shopId}/reviews";
    }
    /// <summary>
    /// Represents Shipping Urls.
    /// </summary>

    public static class ShippingUrls
    {
        /// <summary>
        /// Executes the Get Shipping Carriers operation.
        /// </summary>
        public static string GetShippingCarriers() =>
            "/v3/application/shipping-carriers";
        /// <summary>
        /// Executes the Get Shop Shipping Profiles operation.
        /// </summary>

        public static string GetShopShippingProfiles(long shopId) =>
            $"/v3/application/shops/{shopId}/shipping-profiles";
        /// <summary>
        /// Executes the Get Shop Shipping Profile operation.
        /// </summary>

        public static string GetShopShippingProfile(long shopId, long shippingProfileId) =>
            $"/v3/application/shops/{shopId}/shipping-profiles/{shippingProfileId}";
        /// <summary>
        /// Executes the Get Shop Shipping Profile Destinations operation.
        /// </summary>

        public static string GetShopShippingProfileDestinations(long shopId, long shippingProfileId) =>
            $"/v3/application/shops/{shopId}/shipping-profiles/{shippingProfileId}/destinations";
        /// <summary>
        /// Executes the Get Shop Shipping Profile Destination operation.
        /// </summary>

        public static string GetShopShippingProfileDestination(long shopId, long shippingProfileId, long destinationId) =>
            $"/v3/application/shops/{shopId}/shipping-profiles/{shippingProfileId}/destinations/{destinationId}";
        /// <summary>
        /// Executes the Get Shop Shipping Profile Upgrades operation.
        /// </summary>

        public static string GetShopShippingProfileUpgrades(long shopId, long shippingProfileId) =>
            $"/v3/application/shops/{shopId}/shipping-profiles/{shippingProfileId}/upgrades";
        /// <summary>
        /// Executes the Get Shop Shipping Profile Upgrade operation.
        /// </summary>

        public static string GetShopShippingProfileUpgrade(long shopId, long shippingProfileId, long upgradeId) =>
            $"/v3/application/shops/{shopId}/shipping-profiles/{shippingProfileId}/upgrades/{upgradeId}";
    }
    /// <summary>
    /// Represents Listing Urls.
    /// </summary>

    public static class ListingUrls
    {
        /// <summary>
        /// Executes the Get Listing operation.
        /// </summary>
        public static string GetListing(long listingId) =>
            $"/v3/application/listings/{listingId}";
        /// <summary>
        /// Executes the Get Featured Listings By Shop operation.
        /// </summary>

        public static string GetFeaturedListingsByShop(long shopId) =>
            $"/v3/application/shops/{shopId}/listings/featured";
        /// <summary>
        /// Executes the Find All Listings Active operation.
        /// </summary>

        public static string FindAllListingsActive() =>
            $"/v3/application/listings/active";
        /// <summary>
        /// Executes the Find All Active Listings By Shop operation.
        /// </summary>

        public static string FindAllActiveListingsByShop(long shopId) =>
            $"/v3/application/shops/{shopId}/listings/active";
        /// <summary>
        /// Executes the Get Listings By Shop operation.
        /// </summary>

        public static string GetListingsByShop(long shopId) =>
            $"/v3/application/shops/{shopId}/listings";
        /// <summary>
        /// Executes the Get Listings By Shop Receipt operation.
        /// </summary>

        public static string GetListingsByShopReceipt(long shopId, long receiptId) =>
            $"/v3/application/shops/{shopId}/receipts/{receiptId}/listings";
        /// <summary>
        /// Executes the Get Listing Properties operation.
        /// </summary>

        public static string GetListingProperties(long shopId, long listingId) =>
            $"/v3/application/shops/{shopId}/listings/{listingId}/properties";
        /// <summary>
        /// Executes the Get Listing Property operation.
        /// </summary>

        public static string GetListingProperty(long listingId, long propertyId) =>
            $"/v3/application/listings/{listingId}/properties/{propertyId}";
        /// <summary>
        /// Executes the Get Listings By Listing Ids operation.
        /// </summary>

        public static string GetListingsByListingIds() =>
            $"/v3/application/listings/batch";
        /// <summary>
        /// Executes the Get Listings By Shop Section Id operation.
        /// </summary>

        public static string GetListingsByShopSectionId(long shopId) =>
            $"/v3/application/shops/{shopId}/shop-sections/listings";
        /// <summary>
        /// Executes the Get Listing Image operation.
        /// </summary>

        public static string GetListingImage(long listingId, long listingImageId) =>
            $"/v3/application/listings/{listingId}/images/{listingImageId}";
        /// <summary>
        /// Executes the Get Listing Images operation.
        /// </summary>

        public static string GetListingImages(long listingId) =>
            $"/v3/application/listings/{listingId}/images";
        /// <summary>
        /// Executes the Get Seller Taxonomy Nodes operation.
        /// </summary>

        public static string GetSellerTaxonomyNodes() =>
            "/v3/application/seller-taxonomy/nodes";
        /// <summary>
        /// Executes the Get Properties By Taxonomy Id operation.
        /// </summary>

        public static string GetPropertiesByTaxonomyId(long taxonomyId) =>
            $"/v3/application/seller-taxonomy/nodes/{taxonomyId}/properties";
        /// <summary>
        /// Executes the Get Buyer Taxonomy Nodes operation.
        /// </summary>

        public static string GetBuyerTaxonomyNodes() =>
            "/v3/application/buyer-taxonomy/nodes";
        /// <summary>
        /// Executes the Get Properties By Buyer Taxonomy Id operation.
        /// </summary>

        public static string GetPropertiesByBuyerTaxonomyId(long taxonomyId) =>
            $"/v3/application/buyer-taxonomy/nodes/{taxonomyId}/properties";
        /// <summary>
        /// Executes the Create Draft Listing operation.
        /// </summary>

        public static string CreateDraftListing(long shopId) =>
            $"/v3/application/shops/{shopId}/listings";
        /// <summary>
        /// Executes the Update Listing operation.
        /// </summary>

        public static string UpdateListing(long shopId, long listingId) =>
            $"/v3/application/shops/{shopId}/listings/{listingId}";
        /// <summary>
        /// Executes the Delete Listing operation.
        /// </summary>

        public static string DeleteListing(long listingId) =>
            $"/v3/application/listings/{listingId}";
        /// <summary>
        /// Executes the Get Listings By Shop Return Policy operation.
        /// </summary>

        public static string GetListingsByShopReturnPolicy(long shopId, long returnPolicyId) =>
            $"/v3/application/shops/{shopId}/policies/return/{returnPolicyId}/listings";
        /// <summary>
        /// Executes the Get Listings Shipping By Listing Ids operation.
        /// </summary>

        public static string GetListingsShippingByListingIds() =>
            "/v3/application/listings/batch/shipping";
        /// <summary>
        /// Executes the Update Listing Property operation.
        /// </summary>

        public static string UpdateListingProperty(long shopId, long listingId, long propertyId) =>
            $"/v3/application/shops/{shopId}/listings/{listingId}/properties/{propertyId}";
        /// <summary>
        /// Executes the Get All Listing Files operation.
        /// </summary>

        public static string GetAllListingFiles(long shopId, long listingId) =>
            $"/v3/application/shops/{shopId}/listings/{listingId}/files";
        /// <summary>
        /// Executes the Get Listing File operation.
        /// </summary>

        public static string GetListingFile(long shopId, long listingId, long listingFileId) =>
            $"/v3/application/shops/{shopId}/listings/{listingId}/files/{listingFileId}";
        /// <summary>
        /// Executes the Upload Listing Image operation.
        /// </summary>

        public static string UploadListingImage(long shopId, long listingId) =>
            $"/v3/application/shops/{shopId}/listings/{listingId}/images";
        /// <summary>
        /// Executes the Delete Listing Image operation.
        /// </summary>

        public static string DeleteListingImage(long shopId, long listingId, long listingImageId) =>
            $"/v3/application/shops/{shopId}/listings/{listingId}/images/{listingImageId}";
        /// <summary>
        /// Executes the Get Listing Inventory operation.
        /// </summary>

        public static string GetListingInventory(long listingId) =>
            $"/v3/application/listings/{listingId}/inventory";
        /// <summary>
        /// Executes the Get Listings Inventory By Listing Ids operation.
        /// </summary>

        public static string GetListingsInventoryByListingIds() =>
            "/v3/application/listings/batch/inventory";
        /// <summary>
        /// Executes the Get Listing Offering operation.
        /// </summary>

        public static string GetListingOffering(long listingId, long productId, long offeringId) =>
            $"/v3/application/listings/{listingId}/products/{productId}/offerings/{offeringId}";
        /// <summary>
        /// Executes the Get Listing Product operation.
        /// </summary>

        public static string GetListingProduct(long listingId, long productId) =>
            $"/v3/application/listings/{listingId}/inventory/products/{productId}";
        /// <summary>
        /// Executes the Get Listing Personalization operation.
        /// </summary>

        public static string GetListingPersonalization(long listingId) =>
            $"/v3/application/listings/{listingId}/personalization";
        /// <summary>
        /// Executes the Update Listing Personalization operation.
        /// </summary>

        public static string UpdateListingPersonalization(long shopId, long listingId) =>
            $"/v3/application/shops/{shopId}/listings/{listingId}/personalization";
        /// <summary>
        /// Executes the Get Listing Translation operation.
        /// </summary>

        public static string GetListingTranslation(long shopId, long listingId, string language) =>
            $"/v3/application/shops/{shopId}/listings/{listingId}/translations/{language}";
        /// <summary>
        /// Executes the Get Listing Variation Images operation.
        /// </summary>

        public static string GetListingVariationImages(long shopId, long listingId) =>
            $"/v3/application/shops/{shopId}/listings/{listingId}/variation-images";
        /// <summary>
        /// Executes the Get Listing Video operation.
        /// </summary>

        public static string GetListingVideo(long listingId, long videoId) =>
            $"/v3/application/listings/{listingId}/videos/{videoId}";
        /// <summary>
        /// Executes the Get Listing Videos operation.
        /// </summary>

        public static string GetListingVideos(long listingId) =>
            $"/v3/application/listings/{listingId}/videos";
        /// <summary>
        /// Executes the Upload Listing Video operation.
        /// </summary>

        public static string UploadListingVideo(long shopId, long listingId) =>
            $"/v3/application/shops/{shopId}/listings/{listingId}/videos";
        /// <summary>
        /// Executes the Delete Listing Video operation.
        /// </summary>

        public static string DeleteListingVideo(long shopId, long listingId, long videoId) =>
            $"/v3/application/shops/{shopId}/listings/{listingId}/videos/{videoId}";
    }
    /// <summary>
    /// Represents Shop Urls.
    /// </summary>

    public static class ShopUrls
    {
        /// <summary>
        /// Executes the Get Shop operation.
        /// </summary>
        public static string GetShop(long shopId) =>
            $"/v3/application/shops/{shopId}";
        /// <summary>
        /// Executes the Get Shop By Owner User Id operation.
        /// </summary>

        public static string GetShopByOwnerUserId(long userId) =>
            $"/v3/application/users/{userId}/shops";
        /// <summary>
        /// Executes the Update Shop operation.
        /// </summary>

        public static string UpdateShop(long shopId) =>
            $"/v3/application/shops/{shopId}";
        /// <summary>
        /// Executes the Find Shops operation.
        /// </summary>

        public static string FindShops() =>
            $"/v3/application/shops";
        /// <summary>
        /// Executes the Get Shop Production Partners operation.
        /// </summary>

        public static string GetShopProductionPartners(long shopId) =>
            $"/v3/application/shops/{shopId}/production-partners";
        /// <summary>
        /// Executes the Get Shop Sections operation.
        /// </summary>

        public static string GetShopSections(long shopId) =>
            $"/v3/application/shops/{shopId}/sections";
        /// <summary>
        /// Executes the Get Shop Section operation.
        /// </summary>

        public static string GetShopSection(long shopId, long shopSectionId) =>
            $"/v3/application/shops/{shopId}/sections/{shopSectionId}";
    }
    /// <summary>
    /// Represents User Urls.
    /// </summary>

    public static class UserUrls
    {
        /// <summary>
        /// Executes the Get User operation.
        /// </summary>
        public static string GetUser(long userId) =>
            $"/v3/application/users/{userId}";
        /// <summary>
        /// Executes the Get Me operation.
        /// </summary>

        public static string GetMe() =>
            "/v3/application/users/me";
        /// <summary>
        /// Executes the Get User Addresses operation.
        /// </summary>

        public static string GetUserAddresses() =>
            "/v3/application/user/addresses";
        /// <summary>
        /// Executes the Get User Address operation.
        /// </summary>

        public static string GetUserAddress(long userAddressId) =>
            $"/v3/application/user/addresses/{userAddressId}";
    }
    /// <summary>
    /// Represents Shop Policy Urls.
    /// </summary>

    public static class ShopPolicyUrls
    {
        /// <summary>
        /// Executes the Get Shop Return Policies operation.
        /// </summary>
        public static string GetShopReturnPolicies(long shopId) =>
            $"/v3/application/shops/{shopId}/policies/return";
        /// <summary>
        /// Executes the Get Shop Return Policy operation.
        /// </summary>

        public static string GetShopReturnPolicy(long shopId, long returnPolicyId) =>
            $"/v3/application/shops/{shopId}/policies/return/{returnPolicyId}";
        /// <summary>
        /// Executes the Consolidate Shop Return Policies operation.
        /// </summary>

        public static string ConsolidateShopReturnPolicies(long shopId) =>
            $"/v3/application/shops/{shopId}/policies/return/consolidate";
    }
}
