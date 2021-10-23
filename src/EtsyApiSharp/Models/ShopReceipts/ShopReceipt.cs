using System.Text.Json.Serialization;


namespace EtsyApiSharp.Models
{
    //The record of a purchase from a shop. Shop receipts display monetary values using the shop's currency.
    public class ShopReceipt
    {
        [JsonPropertyName("receipt_id")]
        public int ReceiptId { get; set; }


        [JsonPropertyName("receipt_type")]
        public int ReceiptType { get; set; }


        [JsonPropertyName("seller_user_id")]
        public int SellerUserId { get; set; }


        [JsonPropertyName("seller_email")]
        public string SellerEmail { get; set; }


        [JsonPropertyName("buyer_user_id")]
        public int BuyerUserId { get; set; }


        [JsonPropertyName("buyer_email")]
        public string BuyerEmail { get; set; }


        [JsonPropertyName("name")]
        public string Name { get; set; }


        [JsonPropertyName("first_line")]
        public string FirstLine { get; set; }


        [JsonPropertyName("second_line")]
        public string SecondLine { get; set; }


        [JsonPropertyName("city")]
        public string City { get; set; }


        [JsonPropertyName("state")]
        public string State { get; set; }


        [JsonPropertyName("zip")]
        public string Zip { get; set; }


        [JsonPropertyName("status")]
        public string Status { get; set; }


        [JsonPropertyName("formatted_address")]
        public string FormattedAddress { get; set; }


        [JsonPropertyName("country_iso")]
        public string CountryIso { get; set; }


        [JsonPropertyName("payment_method")]
        public string PaymentMethod { get; set; }


        [JsonPropertyName("payment_email")]
        public string PaymentEmail { get; set; }


        [JsonPropertyName("message_from_seller")]
        public string MessageFromSeller { get; set; }


        [JsonPropertyName("message_from_buyer")]
        public string MessageFromBuyer { get; set; }


        [JsonPropertyName("message_from_payment")]
        public string MessageFromPayment { get; set; }


        [JsonPropertyName("is_paid")]
        public bool IsPaid { get; set; }


        [JsonPropertyName("is_shipped")]
        public bool IsShipped { get; set; }


        [JsonPropertyName("create_timestamp")]
        public int CreateTimestamp { get; set; }


        [JsonPropertyName("update_timestamp")]
        public int UpdateTimestamp { get; set; }


        [JsonPropertyName("gift_message")]
        public string GiftMessage { get; set; }


        [JsonPropertyName("grandtotal")]
        public Money Grandtotal { get; set; }


        [JsonPropertyName("subtotal")]
        public Money Subtotal { get; set; }


        [JsonPropertyName("total_price")]
        public Money TotalPrice { get; set; }


        [JsonPropertyName("total_shipping_cost")]
        public Money TotalShippingCost { get; set; }


        [JsonPropertyName("total_tax_cost")]
        public Money TotalTaxCost { get; set; }


        [JsonPropertyName("total_vat_cost")]
        public Money TotalVatCost { get; set; }


        [JsonPropertyName("discount_amt")]
        public Money DiscountAmt { get; set; }


        [JsonPropertyName("gift_wrap_price")]
        public Money GiftWrapPrice { get; set; }


        [JsonPropertyName("shipments")]
        public List<ShopReceiptShipment> Shipments { get; set; }


        [JsonPropertyName("transactions")]
        public List<ListingTranslation> Transactions { get; set; }


    }
}
