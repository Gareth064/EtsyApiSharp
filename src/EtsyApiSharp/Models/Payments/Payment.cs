using System.Text.Json.Serialization;

namespace EtsyApiSharp.Models
{
    //Represents a payment made with Etsy Payments. All monetary amounts are in USD pennies unless otherwise specified.
    public class Payment
    {
        [JsonPropertyName("payment_id")]
        public int PaymentId { get; set; }


        [JsonPropertyName("buyer_user_id")]
        public int BuyerUserId { get; set; }


        [JsonPropertyName("shop_id")]
        public int ShopId { get; set; }


        [JsonPropertyName("receipt_id")]
        public int ReceiptId { get; set; }


        [JsonPropertyName("amount_gross")]
        public Money AmountGross { get; set; }


        [JsonPropertyName("amount_fees")]
        public Money AmountFees { get; set; }


        [JsonPropertyName("amount_net")]
        public Money AmountNet { get; set; }


        [JsonPropertyName("posted_gross")]
        public Money PostedGross { get; set; }


        [JsonPropertyName("posted_fees")]
        public Money PostedFees { get; set; }


        [JsonPropertyName("posted_net")]
        public Money PostedNet { get; set; }


        [JsonPropertyName("adjusted_gross")]
        public Money AdjustedGross { get; set; }


        [JsonPropertyName("adjusted_fees")]
        public Money AdjustedFees { get; set; }


        [JsonPropertyName("adjusted_net")]
        public Money AdjustedNet { get; set; }


        [JsonPropertyName("currency")]
        public string Currency { get; set; }


        [JsonPropertyName("shop_currency")]
        public string ShopCurrency { get; set; }


        [JsonPropertyName("buyer_currency")]
        public string BuyerCurrency { get; set; }


        [JsonPropertyName("shipping_user_id")]
        public int ShippingUserId { get; set; }


        [JsonPropertyName("shipping_address_id")]
        public int ShippingAddressId { get; set; }


        [JsonPropertyName("billing_address_id")]
        public int BillingAddressId { get; set; }


        [JsonPropertyName("status")]
        public string Status { get; set; }


        [JsonPropertyName("shipped_timestamp")]
        public int ShippedTimestamp { get; set; }


        [JsonPropertyName("create_timestamp")]
        public int CreateTimestamp { get; set; }


        [JsonPropertyName("update_timestamp")]
        public int UpdateTimestamp { get; set; }


        [JsonPropertyName("payment_adjustments")]
        public List<PaymentAdjustment> PaymentAdjustments { get; set; }


    }
}
