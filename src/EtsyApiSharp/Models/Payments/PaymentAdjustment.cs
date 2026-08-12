using System.Text.Json.Serialization;

namespace EtsyApiSharp.Models;

/// <summary>
/// Represents a refund, which applies to a prior Etsy payment. All monetary amounts are in USD pennies unless otherwise specified.
/// </summary>
public class PaymentAdjustment
{
    /// <summary>
    /// The numeric ID for a payment adjustment.
    /// </summary>
    [JsonPropertyName("payment_adjustment_id")]
    public long PaymentAdjustmentId { get; set; }

    /// <summary>
    /// A unique numeric ID for a payment to a specific Etsy [shop](/documentation/reference#tag/Shop).
    /// </summary>
    [JsonPropertyName("payment_id")]
    public long PaymentId { get; set; }

    /// <summary>
    /// The status string of the payment adjustment.
    /// </summary>
    [JsonPropertyName("status")]
    public string? Status { get; set; }

    /// <summary>
    /// When true, the payment adjustment was or is likely to complete successfully.
    /// </summary>
    [JsonPropertyName("is_success")]
    public bool IsSuccess { get; set; }

    /// <summary>
    /// The numeric ID for the [user](/documentation/reference#tag/User) (seller) fulfilling the purchase.
    /// </summary>
    [JsonPropertyName("user_id")]
    public long UserId { get; set; }

    /// <summary>
    /// A human-readable string describing the reason for the refund.
    /// </summary>
    [JsonPropertyName("reason_code")]
    public string? ReasonCode { get; set; }

    /// <summary>
    /// The total numeric amount of the refund in the payment currency.
    /// </summary>
    [JsonPropertyName("total_adjustment_amount")]
    public long? TotalAdjustmentAmount { get; set; }

    /// <summary>
    /// The numeric amount of the refund in the shop currency.
    /// </summary>
    [JsonPropertyName("shop_total_adjustment_amount")]
    public long? ShopTotalAdjustmentAmount { get; set; }

    /// <summary>
    /// The numeric amount of the refund in the buyer currency.
    /// </summary>
    [JsonPropertyName("buyer_total_adjustment_amount")]
    public long? BuyerTotalAdjustmentAmount { get; set; }

    /// <summary>
    /// The numeric amount of card processing fees associated with a payment adjustment.
    /// </summary>
    [JsonPropertyName("total_fee_adjustment_amount")]
    public long? TotalFeeAdjustmentAmount { get; set; }

    /// <summary>
    /// The transaction\'s creation date and time, in epoch seconds.
    /// </summary>
    [JsonPropertyName("create_timestamp")]
    public long CreateTimestamp { get; set; }

    /// <summary>
    /// The transaction's creation date and time, in epoch seconds.
    /// </summary>
    [JsonPropertyName("created_timestamp")]
    public long CreatedTimestamp { get; set; }

    /// <summary>
    /// The date and time of the last change to the payment adjustment in epoch seconds.
    /// </summary>
    [JsonPropertyName("update_timestamp")]
    public long UpdateTimestamp { get; set; }

    /// <summary>
    /// The update date and time of the payment adjustment, in epoch seconds.
    /// </summary>
    [JsonPropertyName("updated_timestamp")]
    public long UpdatedTimestamp { get; set; }

    /// <summary>
    /// Payment-adjustment line items, when supplied by Etsy.
    /// </summary>
    [JsonPropertyName("payment_adjustment_items")]
    public List<PaymentAdjustmentItem>? PaymentAdjustmentItems { get; set; }

}
