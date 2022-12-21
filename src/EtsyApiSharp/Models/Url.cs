namespace EtsyApiSharp.Models;

public class Url
{
    public static class AuthUrls
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
            $"/v3/application/shops/";
    }
}
