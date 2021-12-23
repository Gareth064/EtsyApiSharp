using Newtonsoft.Json;


namespace EtsyApiSharp.Models
{
    //The record of a purchase from a shop. Shop receipts display monetary values using the shop's currency.
    public class ShopReceipt
    {
        [JsonProperty("receipt_id")]
        public long ReceiptId { get; set; }


        [JsonProperty("receipt_type")]
        public long ReceiptType { get; set; }


        [JsonProperty("seller_user_id")]
        public long SellerUserId { get; set; }


        [JsonProperty("seller_email")]
        public string SellerEmail { get; set; }


        [JsonProperty("buyer_user_id")]
        public long BuyerUserId { get; set; }


        [JsonProperty("buyer_email")]
        public string BuyerEmail { get; set; }


        [JsonProperty("name")]
        public string Name { get; set; }


        [JsonProperty("first_line")]
        public string FirstLine { get; set; }


        [JsonProperty("second_line")]
        public string SecondLine { get; set; }


        [JsonProperty("city")]
        public string City { get; set; }


        [JsonProperty("state")]
        public string State { get; set; }


        [JsonProperty("zip")]
        public string Zip { get; set; }


        [JsonProperty("status")]
        public string Status { get; set; }


        [JsonProperty("formatted_address")]
        public string FormattedAddress { get; set; }


        [JsonProperty("country_iso")]
        public string CountryIso { get; set; }


        [JsonProperty("payment_method")]
        public string PaymentMethod { get; set; }


        [JsonProperty("payment_email")]
        public string PaymentEmail { get; set; }


        [JsonProperty("message_from_seller")]
        public string MessageFromSeller { get; set; }


        [JsonProperty("message_from_buyer")]
        public string MessageFromBuyer { get; set; }


        [JsonProperty("message_from_payment")]
        public string MessageFromPayment { get; set; }


        [JsonProperty("is_paid")]
        public bool IsPaid { get; set; }


        [JsonProperty("is_shipped")]
        public bool IsShipped { get; set; }


        [JsonProperty("create_timestamp")]
        public long CreateTimestamp { get; set; }


        [JsonProperty("update_timestamp")]
        public long UpdateTimestamp { get; set; }


        [JsonProperty("gift_message")]
        public string GiftMessage { get; set; }


        [JsonProperty("grandtotal")]
        public Money Grandtotal { get; set; }


        [JsonProperty("subtotal")]
        public Money Subtotal { get; set; }


        [JsonProperty("total_price")]
        public Money TotalPrice { get; set; }


        [JsonProperty("total_shipping_cost")]
        public Money TotalShippingCost { get; set; }


        [JsonProperty("total_tax_cost")]
        public Money TotalTaxCost { get; set; }


        [JsonProperty("total_vat_cost")]
        public Money TotalVatCost { get; set; }


        [JsonProperty("discount_amt")]
        public Money DiscountAmt { get; set; }


        [JsonProperty("gift_wrap_price")]
        public Money GiftWrapPrice { get; set; }


        [JsonProperty("shipments")]
        public List<ShopReceiptShipment> Shipments { get; set; }


        [JsonProperty("transactions")]
        public List<ListingTranslation> Transactions { get; set; }


    }
}
