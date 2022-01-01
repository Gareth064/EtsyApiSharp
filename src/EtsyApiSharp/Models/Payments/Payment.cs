using System.Text.Json.Serialization;

namespace EtsyApiSharp.Models
{
    /// <summary>
    /// Represents a payment made with Etsy Payments. All monetary amounts are in USD pennies unless otherwise specified.
    /// </summary>
    public class Payment
    {
        /// <summary>
        /// A unique numeric ID for a payment to a specific Etsy [shop](/documentation/reference#tag/Shop).
        /// </summary>
        [JsonPropertyName("payment_id")]
        public long PaymentId { get; set; }

        /// <summary>
        /// The numeric ID for the [user](/documentation/reference#tag/User) who paid the purchase.
        /// </summary>
        [JsonPropertyName("buyer_user_id")]
        public long BuyerUserId { get; set; }

        /// <summary>
        /// The unique positive non-zero numeric ID for an Etsy Shop.
        /// </summary>
        [JsonPropertyName("shop_id")]
        public long ShopId { get; set; }

        /// <summary>
        /// The numeric ID for the [receipt](/documentation/reference#tag/Shop-Receipt) associated to this transaction.
        /// </summary>
        [JsonPropertyName("receipt_id")]
        public long ReceiptId { get; set; }

        /// <summary>
        /// An integer equal to gross amount of the order, in pennies, including shipping and taxes.
        /// </summary>
        [JsonPropertyName("amount_gross")]
        public Money AmountGross { get; set; }

        /// <summary>
        /// An integer equal to the original card processing fee of the order in pennies.
        /// </summary>
        [JsonPropertyName("amount_fees")]
        public Money AmountFees { get; set; }

        /// <summary>
        /// An integer equal to the payment value, in pennies, less fees (`amount_gross` - `amount_fees`).
        /// </summary>
        [JsonPropertyName("amount_net")]
        public Money AmountNet { get; set; }

        /// <summary>
        /// The total gross value of the payment posted once the purchase ships. This is equal to the `amount_gross` UNLESS the seller issues a refund prior to shipping. We consider "shipping" to the event which "posts" to the ledger. Therefore, if the seller refunds first, we reduce the `amount_gross` first and post then that amount. The seller never sees the refunded amount in their ledger. This is equal to the "Credit" amount in the ledger entry.
        /// </summary>
        [JsonPropertyName("posted_gross")]
        public Money PostedGross { get; set; }

        /// <summary>
        /// The total value of the fees posted once the purchase ships. Etsy refunds a proportional amount of the fees when a seller refunds a buyer. When the seller issues a refund prior to shipping, the posted amount is less then the original.
        /// </summary>
        [JsonPropertyName("posted_fees")]
        public Money PostedFees { get; set; }

        /// <summary>
        /// The total value of the payment at the time of posting, less fees. (`posted_gross` - `posted_fees`)
        /// </summary>
        [JsonPropertyName("posted_net")]
        public Money PostedNet { get; set; }

        /// <summary>
        /// The gross payment amount after the seller refunds a payment, partially or fully.
        /// </summary>
        [JsonPropertyName("adjusted_gross")]
        public Money AdjustedGross { get; set; }

        /// <summary>
        /// The new fee amount after a seller refunds a payment, partially or fully.
        /// </summary>
        [JsonPropertyName("adjusted_fees")]
        public Money AdjustedFees { get; set; }

        /// <summary>
        /// The total value of the payment after refunds, less fees (`adjusted_gross` - `adjusted_fees`).
        /// </summary>
        [JsonPropertyName("adjusted_net")]
        public Money AdjustedNet { get; set; }

        /// <summary>
        /// The ISO (alphabetic) code string for the payment's currency.
        /// </summary>
        [JsonPropertyName("currency")]
        public string Currency { get; set; }

        /// <summary>
        /// The ISO (alphabetic) code for the shop's currency. The shop displays all prices in this currency by default.
        /// </summary>
        [JsonPropertyName("shop_currency")]
        public string ShopCurrency { get; set; }

        /// <summary>
        /// The currency string of the buyer.
        /// </summary>
        [JsonPropertyName("buyer_currency")]
        public string BuyerCurrency { get; set; }

        /// <summary>
        /// The numeric ID of the user to which the seller ships the order.
        /// </summary>
        [JsonPropertyName("shipping_user_id")]
        public long? ShippingUserId { get; set; }

        /// <summary>
        /// The numeric id identifying the shipping address.
        /// </summary>
        [JsonPropertyName("shipping_address_id")]
        public long ShippingAddressId { get; set; }

        /// <summary>
        /// The numeric ID identifying the billing address of the buyer.
        /// </summary>
        [JsonPropertyName("billing_address_id")]
        public long BillingAddressId { get; set; }

        /// <summary>
        /// A string indicating the current status of the payment, most commonly "settled" or "authed".
        /// </summary>
        [JsonPropertyName("status")]
        public string Status { get; set; }

        /// <summary>
        /// The transaction\'s shipping date and time, in epoch seconds.
        /// </summary>
        [JsonPropertyName("shipped_timestamp")]
        public int? ShippedTimestamp { get; set; }

        /// <summary>
        /// The transaction\'s creation date and time, in epoch seconds.
        /// </summary>
        [JsonPropertyName("create_timestamp")]
        public int CreateTimestamp { get; set; }

        /// <summary>
        /// The date and time of the last change to the payment adjustment in epoch seconds.
        /// </summary>
        [JsonPropertyName("update_timestamp")]
        public int UpdateTimestamp { get; set; }

        /// <summary>
        /// List of refund objects on an Etsy Payments transaction. All monetary amounts are in USD pennies unless otherwise specified.
        /// </summary>
        [JsonPropertyName("payment_adjustments")]
        public List<PaymentAdjustment> PaymentAdjustments { get; set; }

    }
}
