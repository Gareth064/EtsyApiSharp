using System.Text.Json.Serialization;

namespace EtsyApiSharp.Models;

/// <summary>
/// The record of a purchase from a shop. Shop receipts display monetary values using the shop's currency.
/// </summary>
public class ShopReceipt
{
    /// <summary>
    /// The numeric ID for the [receipt](/documentation/reference#tag/Shop-Receipt) associated to this transaction.
    /// </summary>
    [JsonPropertyName("receipt_id")]
    public long ReceiptId { get; set; }

    /// <summary>
    /// The numeric value for the Etsy channel that serviced the purchase: 0 or 5 for Etsy.com, 1 for a Pattern shop.
    /// </summary>
    [JsonPropertyName("receipt_type")]
    public long ReceiptType { get; set; }

    /// <summary>
    /// The numeric ID for the [user](/documentation/reference#tag/User) (seller) fulfilling the purchase.
    /// </summary>
    [JsonPropertyName("seller_user_id")]
    public long SellerUserId { get; set; }

    /// <summary>
    /// The email address string for the seller of the listing.
    /// </summary>
    [JsonPropertyName("seller_email")]
    public string? SellerEmail { get; set; }

    /// <summary>
    /// The numeric ID for the [user](/documentation/reference#tag/User) making the purchase.
    /// </summary>
    [JsonPropertyName("buyer_user_id")]
    public long BuyerUserId { get; set; }

    /// <summary>
    /// The email address string for the buyer of the listing.
    /// </summary>
    [JsonPropertyName("buyer_email")]
    public string? BuyerEmail { get; set; }

    /// <summary>
    /// The name string for the recipient in the shipping address.
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; }

    /// <summary>
    /// The first address line string for the recipient in the shipping address.
    /// </summary>
    [JsonPropertyName("first_line")]
    public string? FirstLine { get; set; }

    /// <summary>
    /// The optional second address line string for the recipient in the shipping address.
    /// </summary>
    [JsonPropertyName("second_line")]
    public string? SecondLine { get; set; }

    /// <summary>
    /// The city string for the recipient in the shipping address.
    /// </summary>
    [JsonPropertyName("city")]
    public string? City { get; set; }

    /// <summary>
    /// The state string for the recipient in the shipping address.
    /// </summary>
    [JsonPropertyName("state")]
    public string? State { get; set; }

    /// <summary>
    /// The zip code string (not necessarily a number) for the recipient in the shipping address.
    /// </summary>
    [JsonPropertyName("zip")]
    public string? Zip { get; set; }

    /// <summary>
    /// The current order status string. One of: `Open`, `Paid`, `Completed`, `Payment Processing`.
    /// </summary>
    [JsonPropertyName("status")]
    public string Status { get; set; }

    /// <summary>
    /// The formatted shipping address string for the recipient in the shipping address.
    /// </summary>
    [JsonPropertyName("formatted_address")]
    public string? FormattedAddress { get; set; }

    /// <summary>
    /// The ISO-3166 alpha-2 country code string for the recipient in the shipping address.
    /// </summary>
    [JsonPropertyName("country_iso")]
    public string? CountryIso { get; set; }

    /// <summary>
    /// The payment method string identifying purchaser's payment method, which must be one of: \'cc\' (credit card), \'paypal\', \'check\', \'mo\' (money order), \'bt\' (bank transfer), \'other\', \'ideal\', \'sofort\', \'apple_pay\', \'google\', \'android_pay\', \'google_pay\', \'klarna\', \'k_pay_in_4\' (klarna), \'k_pay_in_3\' (klarna), or \'k_financing\' (klarna).
    /// </summary>
    [JsonPropertyName("payment_method")]
    public string PaymentMethod { get; set; }

    /// <summary>
    /// The email address string for the email address to which to send payment confirmation
    /// </summary>
    [JsonPropertyName("payment_email")]
    public string? PaymentEmail { get; set; }

    /// <summary>
    /// An optional message string from the seller.
    /// </summary>
    [JsonPropertyName("message_from_seller")]
    public string? MessageFromSeller { get; set; }

    /// <summary>
    /// An optional message string from the buyer.
    /// </summary>
    [JsonPropertyName("message_from_buyer")]
    public string? MessageFromBuyer { get; set; }

    /// <summary>
    /// The machine-generated acknowledgement string from the payment system.
    /// </summary>
    [JsonPropertyName("message_from_payment")]
    public string? MessageFromPayment { get; set; }

    /// <summary>
    /// When true, buyer paid for this purchase.
    /// </summary>
    [JsonPropertyName("is_paid")]
    public bool IsPaid { get; set; }

    /// <summary>
    /// When true, seller shipped the products.
    /// </summary>
    [JsonPropertyName("is_shipped")]
    public bool IsShipped { get; set; }

    /// <summary>
    /// The receipt\'s creation time, in epoch seconds.
    /// </summary>
    [JsonPropertyName("create_timestamp")]
    public long CreateTimestamp { get; set; }

    /// <summary>
    /// The receipt\'s creation time, in epoch seconds.
    /// </summary>
    [JsonPropertyName("created_timestamp")]
    public long CreatedTimestamp { get; set; }

    /// <summary>
    /// The time of the last update to the receipt, in epoch seconds.
    /// </summary>
    [JsonPropertyName("update_timestamp")]
    public long UpdateTimestamp { get; set; }

    /// <summary>
    /// The time of the last update to the receipt, in epoch seconds.
    /// </summary>
    [JsonPropertyName("updated_timestamp")]
    public long UpdatedTimestamp { get; set; }

    /// <summary>
    /// When true, the buyer indicated this purchase is a gift.
    /// </summary>
    [JsonPropertyName("is_gift")]
    public bool IsGift { get; set; }

    /// <summary>
    /// A gift message string the buyer requests delivered with the product.
    /// </summary>
    [JsonPropertyName("gift_message")]
    public string GiftMessage { get; set; }

    /// <summary>
    /// The name of the person who sent the gift.
    /// </summary>
    [JsonPropertyName("gift_sender")]
    public string GiftSender { get; set; }

    /// <summary>
    /// A number equal to the total_price minus the coupon discount plus tax and shipping costs.
    /// </summary>
    [JsonPropertyName("grandtotal")]
    public Money Grandtotal { get; set; }

    /// <summary>
    /// A number equal to the total_price minus coupon discounts. Does not included tax or shipping costs.
    /// </summary>
    [JsonPropertyName("subtotal")]
    public Money Subtotal { get; set; }

    /// <summary>
    /// A number equal to the sum of the individual listings\' (price * quantity). Does not included tax or shipping costs.
    /// </summary>
    [JsonPropertyName("total_price")]
    public Money TotalPrice { get; set; }

    /// <summary>
    /// A number equal to the total shipping cost of the receipt.
    /// </summary>
    [JsonPropertyName("total_shipping_cost")]
    public Money TotalShippingCost { get; set; }

    /// <summary>
    /// The total sales tax of the receipt.
    /// </summary>
    [JsonPropertyName("total_tax_cost")]
    public Money TotalTaxCost { get; set; }

    /// <summary>
    /// A number equal to the total value-added tax (VAT) of the receipt.
    /// </summary>
    [JsonPropertyName("total_vat_cost")]
    public Money TotalVatCost { get; set; }

    /// <summary>
    /// The numeric total discounted price for the receipt when using a discount (percent or fixed) coupon. Free shipping coupons are not included in this discount amount.
    /// </summary>
    [JsonPropertyName("discount_amt")]
    public Money DiscountAmt { get; set; }

    /// <summary>
    /// The numeric price of gift wrap for this receipt.
    /// </summary>
    [JsonPropertyName("gift_wrap_price")]
    public Money GiftWrapPrice { get; set; }

    /// <summary>
    /// A list of shipment statements for this receipt.
    /// </summary>
    [JsonPropertyName("shipments")]
    public List<ShopReceiptShipment> Shipments { get; set; }

    /// <summary>
    /// Array of transactions for the receipt.
    /// </summary>
    [JsonPropertyName("transactions")]
    public List<ShopReceiptTransaction> Transactions { get; set; }

    /// <summary>
    /// Refunds for a given receipt.
    /// </summary>
    [JsonPropertyName("refunds")]
    public List<ShopRefund> Refunds { get; set; }

}
