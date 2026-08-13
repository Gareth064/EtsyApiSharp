namespace EtsyApiSharp.Models;

public class Url
{
    public static class BaseUrls
    {
        public static readonly string BaseAuthUrl = "https://www.etsy.com/oauth/connect";
        public static readonly string BaseTokenUrl = "https://api.etsy.com/v3/public/oauth/token";
        public static readonly string BaseApiUrl = "https://openapi.etsy.com";
    }

    public static class ReceiptUrls
    {
        public static string GetShopReceipt(long shopId, long receiptId) =>
            $"/v3/application/shops/{shopId}/receipts/{receiptId}";

        public static string GetShopReceipts(long shopId) =>
            $"/v3/application/shops/{shopId}/receipts";

        public static string GetShopReceiptTransaction(long shopId, long transactionId) =>
            $"/v3/application/shops/{shopId}/transactions/{transactionId}";

        public static string GetShopReceiptTransactionsByListing(long shopId, long listingId) =>
            $"/v3/application/shops/{shopId}/listings/{listingId}/transactions";

        public static string GetShopReceiptTransactionsByReceipt(long shopId, long receiptId) =>
            $"/v3/application/shops/{shopId}/receipts/{receiptId}/transactions";

        public static string GetShopReceiptTransactionsByShop(long shopId) =>
            $"/v3/application/shops/{shopId}/transactions";

        public static string CreateReceiptShipment(long shopId, long receiptId) =>
            $"/v3/application/shops/{shopId}/receipts/{receiptId}/tracking";
    }

    public static class PaymentUrls
    {
        public static string GetShopPaymentAccountLedgerEntry(long shopId, long ledgerEntryId) =>
            $"/v3/application/shops/{shopId}/payment-account/ledger-entries/{ledgerEntryId}";

        public static string GetShopPaymentAccountLedgerEntries(long shopId) =>
            $"/v3/application/shops/{shopId}/payment-account/ledger-entries";

        public static string GetPaymentAccountLedgerEntryPayments(long shopId) =>
            $"/v3/application/shops/{shopId}/payment-account/ledger-entries/payments";

        public static string GetShopPaymentByReceiptId(long shopId, long receiptId) =>
            $"/v3/application/shops/{shopId}/receipts/{receiptId}/payments";

        public static string GetPayments(long shopId) =>
            $"/v3/application/shops/{shopId}/payments";
    }

    public static class ReviewUrls
    {
        public static string GetReviewsByListing(long listingId) =>
            $"/v3/application/listings/{listingId}/reviews";

        public static string GetReviewsByShop(long shopId) =>
            $"/v3/application/shops/{shopId}/reviews";
    }

    public static class ShippingUrls
    {
        public static string GetShippingCarriers() =>
            "/v3/application/shipping-carriers";

        public static string GetShopShippingProfiles(long shopId) =>
            $"/v3/application/shops/{shopId}/shipping-profiles";

        public static string GetShopShippingProfile(long shopId, long shippingProfileId) =>
            $"/v3/application/shops/{shopId}/shipping-profiles/{shippingProfileId}";

        public static string GetShopShippingProfileDestinations(long shopId, long shippingProfileId) =>
            $"/v3/application/shops/{shopId}/shipping-profiles/{shippingProfileId}/destinations";

        public static string GetShopShippingProfileDestination(long shopId, long shippingProfileId, long destinationId) =>
            $"/v3/application/shops/{shopId}/shipping-profiles/{shippingProfileId}/destinations/{destinationId}";

        public static string GetShopShippingProfileUpgrades(long shopId, long shippingProfileId) =>
            $"/v3/application/shops/{shopId}/shipping-profiles/{shippingProfileId}/upgrades";

        public static string GetShopShippingProfileUpgrade(long shopId, long shippingProfileId, long upgradeId) =>
            $"/v3/application/shops/{shopId}/shipping-profiles/{shippingProfileId}/upgrades/{upgradeId}";
    }

    public static class ListingUrls
    {
        public static string GetListing(long listingId) =>
            $"/v3/application/listings/{listingId}";

        public static string GetFeaturedListingsByShop(long shopId) =>
            $"/v3/application/shops/{shopId}/listings/featured";

        public static string FindAllListingsActive() =>
            $"/v3/application/listings/active";

        public static string FindAllActiveListingsByShop(long shopId) =>
            $"/v3/application/shops/{shopId}/listings/active";

        public static string GetListingsByShop(long shopId) =>
            $"/v3/application/shops/{shopId}/listings";

        public static string GetListingsByShopReceipt(long shopId, long receiptId) =>
            $"/v3/application/shops/{shopId}/receipts/{receiptId}/listings";

        public static string GetListingProperties(long shopId, long listingId) =>
            $"/v3/application/shops/{shopId}/listings/{listingId}/properties";

        public static string GetListingProperty(long listingId, long propertyId) =>
            $"/v3/application/listings/{listingId}/properties/{propertyId}";

        public static string GetListingsByListingIds() =>
            $"/v3/application/listings/batch";

        public static string GetListingsByShopSectionId(long shopId) =>
            $"/v3/application/shops/{shopId}/shop-sections/listings";

        public static string GetListingImage(long listingId, long listingImageId) =>
            $"/v3/application/listings/{listingId}/images/{listingImageId}";

        public static string GetListingImages(long listingId) =>
            $"/v3/application/listings/{listingId}/images";

        public static string GetSellerTaxonomyNodes() =>
            "/v3/application/seller-taxonomy/nodes";

        public static string GetPropertiesByTaxonomyId(long taxonomyId) =>
            $"/v3/application/seller-taxonomy/nodes/{taxonomyId}/properties";

        public static string GetBuyerTaxonomyNodes() =>
            "/v3/application/buyer-taxonomy/nodes";

        public static string GetPropertiesByBuyerTaxonomyId(long taxonomyId) =>
            $"/v3/application/buyer-taxonomy/nodes/{taxonomyId}/properties";

        public static string CreateDraftListing(long shopId) =>
            $"/v3/application/shops/{shopId}/listings";

        public static string UpdateListing(long shopId, long listingId) =>
            $"/v3/application/shops/{shopId}/listings/{listingId}";

        public static string DeleteListing(long listingId) =>
            $"/v3/application/listings/{listingId}";

        public static string GetListingsByShopReturnPolicy(long shopId, long returnPolicyId) =>
            $"/v3/application/shops/{shopId}/policies/return/{returnPolicyId}/listings";

        public static string GetListingsShippingByListingIds() =>
            "/v3/application/listings/batch/shipping";

        public static string UpdateListingProperty(long shopId, long listingId, long propertyId) =>
            $"/v3/application/shops/{shopId}/listings/{listingId}/properties/{propertyId}";

        public static string GetAllListingFiles(long shopId, long listingId) =>
            $"/v3/application/shops/{shopId}/listings/{listingId}/files";

        public static string GetListingFile(long shopId, long listingId, long listingFileId) =>
            $"/v3/application/shops/{shopId}/listings/{listingId}/files/{listingFileId}";

        public static string UploadListingImage(long shopId, long listingId) =>
            $"/v3/application/shops/{shopId}/listings/{listingId}/images";

        public static string DeleteListingImage(long shopId, long listingId, long listingImageId) =>
            $"/v3/application/shops/{shopId}/listings/{listingId}/images/{listingImageId}";

        public static string GetListingInventory(long listingId) =>
            $"/v3/application/listings/{listingId}/inventory";

        public static string GetListingsInventoryByListingIds() =>
            "/v3/application/listings/batch/inventory";

        public static string GetListingOffering(long listingId, long productId, long offeringId) =>
            $"/v3/application/listings/{listingId}/products/{productId}/offerings/{offeringId}";

        public static string GetListingProduct(long listingId, long productId) =>
            $"/v3/application/listings/{listingId}/inventory/products/{productId}";

        public static string GetListingPersonalization(long listingId) =>
            $"/v3/application/listings/{listingId}/personalization";

        public static string UpdateListingPersonalization(long shopId, long listingId) =>
            $"/v3/application/shops/{shopId}/listings/{listingId}/personalization";

        public static string GetListingTranslation(long shopId, long listingId, string language) =>
            $"/v3/application/shops/{shopId}/listings/{listingId}/translations/{language}";

        public static string GetListingVariationImages(long shopId, long listingId) =>
            $"/v3/application/shops/{shopId}/listings/{listingId}/variation-images";

        public static string GetListingVideo(long listingId, long videoId) =>
            $"/v3/application/listings/{listingId}/videos/{videoId}";

        public static string GetListingVideos(long listingId) =>
            $"/v3/application/listings/{listingId}/videos";

        public static string UploadListingVideo(long shopId, long listingId) =>
            $"/v3/application/shops/{shopId}/listings/{listingId}/videos";

        public static string DeleteListingVideo(long shopId, long listingId, long videoId) =>
            $"/v3/application/shops/{shopId}/listings/{listingId}/videos/{videoId}";
    }

    public static class ShopUrls
    {
        public static string GetShop(long shopId) =>
            $"/v3/application/shops/{shopId}";

        public static string GetShopByOwnerUserId(long userId) =>
            $"/v3/application/users/{userId}/shops";

        public static string UpdateShop(long shopId) =>
            $"/v3/application/shops/{shopId}";

        public static string FindShops() =>
            $"/v3/application/shops";

        public static string GetShopProductionPartners(long shopId) =>
            $"/v3/application/shops/{shopId}/production-partners";

        public static string GetShopSections(long shopId) =>
            $"/v3/application/shops/{shopId}/sections";

        public static string GetShopSection(long shopId, long shopSectionId) =>
            $"/v3/application/shops/{shopId}/sections/{shopSectionId}";
    }

    public static class UserUrls
    {
        public static string GetUser(long userId) =>
            $"/v3/application/users/{userId}";

        public static string GetMe() =>
            "/v3/application/users/me";

        public static string GetUserAddresses() =>
            "/v3/application/user/addresses";

        public static string GetUserAddress(long userAddressId) =>
            $"/v3/application/user/addresses/{userAddressId}";
    }

    public static class ShopPolicyUrls
    {
        public static string GetShopReturnPolicies(long shopId) =>
            $"/v3/application/shops/{shopId}/policies/return";

        public static string GetShopReturnPolicy(long shopId, long returnPolicyId) =>
            $"/v3/application/shops/{shopId}/policies/return/{returnPolicyId}";

        public static string ConsolidateShopReturnPolicies(long shopId) =>
            $"/v3/application/shops/{shopId}/policies/return/consolidate";
    }
}
