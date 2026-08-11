using System.Text.Json.Serialization;

namespace EtsyApiSharp.Models;

/// <summary>
/// A transaction object associated with a shop receipt. Etsy generates one transaction per listing purchased as recorded on the order receipt.
/// </summary>
public class ShopReceiptTransaction
{
    /// <summary>
    /// The unique numeric ID for a transaction.
    /// </summary>
    [JsonPropertyName("transaction_id")]
    public long TransactionId { get; set; }

    /// <summary>
    /// The title string of the [listing](/documentation/reference#tag/ShopListing) purchased in this transaction.
    /// </summary>
    [JsonPropertyName("title")]
    public string? Title { get; set; }

    /// <summary>
    /// The description string of the [listing](/documentation/reference#tag/ShopListing) purchased in this transaction.
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; set; }

    /// <summary>
    /// The numeric user ID for the seller in this transaction.
    /// </summary>
    [JsonPropertyName("seller_user_id")]
    public long SellerUserId { get; set; }

    /// <summary>
    /// The numeric user ID for the buyer in this transaction.
    /// </summary>
    [JsonPropertyName("buyer_user_id")]
    public long BuyerUserId { get; set; }

    /// <summary>
    /// The transaction\'s creation date and time, in epoch seconds.
    /// </summary>
    [JsonPropertyName("create_timestamp")]
    public long CreateTimestamp { get; set; }

    /// <summary>
    /// The transaction\'s creation date and time, in epoch seconds.
    /// </summary>
    [JsonPropertyName("created_timestamp")]
    public long CreatedTimestamp { get; set; }

    /// <summary>
    /// The transaction\'s paid date and time, in epoch seconds.
    /// </summary>
    [JsonPropertyName("paid_timestamp")]
    public long? PaidTimestamp { get; set; }

    /// <summary>
    /// The transaction\'s shipping date and time, in epoch seconds.
    /// </summary>
    [JsonPropertyName("shipped_timestamp")]
    public long? ShippedTimestamp { get; set; }

    /// <summary>
    /// The numeric quantity of products purchased in this transaction.
    /// </summary>
    [JsonPropertyName("quantity")]
    public long Quantity { get; set; }

    /// <summary>
    /// The numeric ID of the primary [listing image](/documentation/reference#tag/ShopListing-Image) for this transaction.
    /// </summary>
    [JsonPropertyName("listing_image_id")]
    public long? ListingImageId { get; set; }

    /// <summary>
    /// The numeric ID for the [receipt](/documentation/reference#tag/Shop-Receipt) associated to this transaction.
    /// </summary>
    [JsonPropertyName("receipt_id")]
    public long ReceiptId { get; set; }

    /// <summary>
    /// When true, the transaction recorded the purchase of a digital listing.
    /// </summary>
    [JsonPropertyName("is_digital")]
    public bool IsDigital { get; set; }

    /// <summary>
    /// A string describing the files purchased in this transaction.
    /// </summary>
    [JsonPropertyName("file_data")]
    public string FileData { get; set; }

    /// <summary>
    /// The numeric ID for the [listing](/documentation/reference#tag/ShopListing) associated to this transaction.
    /// </summary>
    [JsonPropertyName("listing_id")]
    public long? ListingId { get; set; }

    /// <summary>
    /// The type string for the transaction, usually "listing"
    /// </summary>
    [JsonPropertyName("transaction_type")]
    public string TransactionType { get; set; }

    /// <summary>
    /// The numeric ID for a specific [product](/documentation/reference#tag/ShopListing-Product) purchased from a listing.
    /// </summary>
    [JsonPropertyName("product_id")]
    public long? ProductId { get; set; }

    /// <summary>
    /// The SKU string for the product
    /// </summary>
    [JsonPropertyName("sku")]
    public string? Sku { get; set; }

    /// <summary>
    /// A money object representing the price recorded the transaction.
    /// </summary>
    [JsonPropertyName("price")]
    public Money Price { get; set; }

    /// <summary>
    /// A money object representing the shipping cost for this transaction.
    /// </summary>
    [JsonPropertyName("shipping_cost")]
    public Money ShippingCost { get; set; }

    /// <summary>
    /// Array of variations and personalizations the buyer chose.
    /// </summary>
    [JsonPropertyName("variations")]
    public List<TransactionVariation> Variations { get; set; }

    /// <summary>
    /// A list of property value entries for this product.
    /// </summary>
    [JsonPropertyName("product_data")]
    public List<ListingPropertyValue> ProductData { get; set; }

    /// <summary>
    /// The ID of the shipping profile selected for this listing.
    /// </summary>
    [JsonPropertyName("shipping_profile_id")]
    public long? ShippingProfileId { get; set; }

    /// <summary>
    /// The minimum number of days for processing the listing.
    /// </summary>
    [JsonPropertyName("min_processing_days")]
    public long? MinProcessingDays { get; set; }

    /// <summary>
    /// The maximum number of days for processing the listing.
    /// </summary>
    [JsonPropertyName("max_processing_days")]
    public long? MaxProcessingDays { get; set; }

    /// <summary>
    /// Name of the selected shipping method.
    /// </summary>
    [JsonPropertyName("shipping_method")]
    public string? ShippingMethod { get; set; }

    /// <summary>
    /// The name of the shipping upgrade selected for this listing. Default value is null.
    /// </summary>
    [JsonPropertyName("shipping_upgrade")]
    public string? ShippingUpgrade { get; set; }

    /// <summary>
    /// The date & time of the expected ship date, in epoch seconds.
    /// </summary>
    [JsonPropertyName("expected_ship_date")]
    public long? ExpectedShipDate { get; set; }

    /// <summary>
    /// The amount of the buyer coupon that was discounted in the shop's currency.
    /// </summary>
    [JsonPropertyName("buyer_coupon")]
    public float BuyerCoupon { get; set; }

    /// <summary>
    /// The amount of the shop coupon that was discounted in the shop's currency.
    /// </summary>
    [JsonPropertyName("shop_coupon")]
    public float ShopCoupon { get; set; }

}
