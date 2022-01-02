namespace EtsyApiSharp.Models
{
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

    }
}
