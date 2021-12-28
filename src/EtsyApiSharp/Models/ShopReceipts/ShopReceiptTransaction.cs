using System.Text.Json.Serialization;

namespace EtsyApiSharp.Models
{
    //A transaction object associated with a shop receipt. Etsy generates one transaction per listing purchased as recorded on the order receipt.
    public class ShopReceiptTransaction
    {
        [JsonPropertyName("transaction_id")]
        public long TransactionId { get; set; }


        [JsonPropertyName("title")]
        public string Title { get; set; }


        [JsonPropertyName("description")]
        public string Description { get; set; }


        [JsonPropertyName("seller_user_id")]
        public long SellerUserId { get; set; }


        [JsonPropertyName("buyer_user_id")]
        public long BuyerUserId { get; set; }


        [JsonPropertyName("create_timestamp")]
        public long CreateTimestamp { get; set; }


        [JsonPropertyName("paid_timestamp")]
        public long PaidTimestamp { get; set; }


        [JsonPropertyName("shipped_timestamp")]
        public long ShippedTimestamp { get; set; }


        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }


        [JsonPropertyName("listing_image_id")]
        public long ListingImageId { get; set; }


        [JsonPropertyName("receipt_id")]
        public long ReceiptId { get; set; }


        [JsonPropertyName("is_digital")]
        public bool IsDigital { get; set; }


        [JsonPropertyName("file_data")]
        public string FileData { get; set; }


        [JsonPropertyName("listing_id")]
        public long ListingId { get; set; }


        [JsonPropertyName("transaction_type")]
        public string TransactionType { get; set; }


        [JsonPropertyName("product_id")]
        public long ProductId { get; set; }


        [JsonPropertyName("sku")]
        public string Sku { get; set; }


        [JsonPropertyName("price")]
        public Money Price { get; set; }


        [JsonPropertyName("shipping_cost")]
        public Money ShippingCost { get; set; }


        [JsonPropertyName("variations")]
        public List<TransactionVariation> Variations { get; set; }


        [JsonPropertyName("shipping_profile_id")]
        public long ShippingProfileId { get; set; }


        [JsonPropertyName("min_processing_days")]
        public int? MinProcessingDays { get; set; }


        [JsonPropertyName("max_processing_days")]
        public int? MaxProcessingDays { get; set; }


        [JsonPropertyName("shipping_method")]
        public string ShippingMethod { get; set; }


        [JsonPropertyName("shipping_upgrade")]
        public string ShippingUpgrade { get; set; }


        [JsonPropertyName("expected_ship_date")]
        public long ExpectedShipDate { get; set; }


    }
}
