using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EtsyApiSharp.Models.ShopReceipts
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<Root>(myJsonResponse);
    public class Grandtotal
    {
        [JsonPropertyName("amount")]
        public int Amount { get; set; }

        [JsonPropertyName("divisor")]
        public int Divisor { get; set; }

        [JsonPropertyName("currency_code")]
        public string CurrencyCode { get; set; }
    }

    public class Subtotal
    {
        [JsonPropertyName("amount")]
        public int Amount { get; set; }

        [JsonPropertyName("divisor")]
        public int Divisor { get; set; }

        [JsonPropertyName("currency_code")]
        public string CurrencyCode { get; set; }
    }

    public class TotalPrice
    {
        [JsonPropertyName("amount")]
        public int Amount { get; set; }

        [JsonPropertyName("divisor")]
        public int Divisor { get; set; }

        [JsonPropertyName("currency_code")]
        public string CurrencyCode { get; set; }
    }

    public class TotalShippingCost
    {
        [JsonPropertyName("amount")]
        public int Amount { get; set; }

        [JsonPropertyName("divisor")]
        public int Divisor { get; set; }

        [JsonPropertyName("currency_code")]
        public string CurrencyCode { get; set; }
    }

    public class TotalTaxCost
    {
        [JsonPropertyName("amount")]
        public int Amount { get; set; }

        [JsonPropertyName("divisor")]
        public int Divisor { get; set; }

        [JsonPropertyName("currency_code")]
        public string CurrencyCode { get; set; }
    }

    public class TotalVatCost
    {
        [JsonPropertyName("amount")]
        public int Amount { get; set; }

        [JsonPropertyName("divisor")]
        public int Divisor { get; set; }

        [JsonPropertyName("currency_code")]
        public string CurrencyCode { get; set; }
    }

    public class DiscountAmt
    {
        [JsonPropertyName("amount")]
        public int Amount { get; set; }

        [JsonPropertyName("divisor")]
        public int Divisor { get; set; }

        [JsonPropertyName("currency_code")]
        public string CurrencyCode { get; set; }
    }

    public class GiftWrapPrice
    {
        [JsonPropertyName("amount")]
        public int Amount { get; set; }

        [JsonPropertyName("divisor")]
        public int Divisor { get; set; }

        [JsonPropertyName("currency_code")]
        public string CurrencyCode { get; set; }
    }

    public class Shipment
    {
        [JsonPropertyName("receipt_shipping_id")]
        public int ReceiptShippingId { get; set; }

        [JsonPropertyName("shipment_notification_timestamp")]
        public int ShipmentNotificationTimestamp { get; set; }

        [JsonPropertyName("carrier_name")]
        public string CarrierName { get; set; }

        [JsonPropertyName("tracking_code")]
        public string TrackingCode { get; set; }
    }

    public class Price
    {
        [JsonPropertyName("amount")]
        public int Amount { get; set; }

        [JsonPropertyName("divisor")]
        public int Divisor { get; set; }

        [JsonPropertyName("currency_code")]
        public string CurrencyCode { get; set; }
    }

    public class ShippingCost
    {
        [JsonPropertyName("amount")]
        public int Amount { get; set; }

        [JsonPropertyName("divisor")]
        public int Divisor { get; set; }

        [JsonPropertyName("currency_code")]
        public string CurrencyCode { get; set; }
    }

    public class Root
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
        public Grandtotal Grandtotal { get; set; }

        [JsonPropertyName("subtotal")]
        public Subtotal Subtotal { get; set; }

        [JsonPropertyName("total_price")]
        public TotalPrice TotalPrice { get; set; }

        [JsonPropertyName("total_shipping_cost")]
        public TotalShippingCost TotalShippingCost { get; set; }

        [JsonPropertyName("total_tax_cost")]
        public TotalTaxCost TotalTaxCost { get; set; }

        [JsonPropertyName("total_vat_cost")]
        public TotalVatCost TotalVatCost { get; set; }

        [JsonPropertyName("discount_amt")]
        public DiscountAmt DiscountAmt { get; set; }

        [JsonPropertyName("gift_wrap_price")]
        public GiftWrapPrice GiftWrapPrice { get; set; }

        [JsonPropertyName("shipments")]
        public List<Shipment> Shipments { get; set; }

        [JsonPropertyName("transactions")]
        public List<ShopReceiptTransaction> Transactions { get; set; }
    }


}
