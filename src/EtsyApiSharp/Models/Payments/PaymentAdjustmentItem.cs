using System.Text.Json.Serialization;

namespace EtsyApiSharp.Models;
/// <summary>
/// Represents Payment Adjustment Item.
/// </summary>

public class PaymentAdjustmentItem
{
    /// <summary>
    /// Gets or sets the Payment Adjustment Id.
    /// </summary>
    [JsonPropertyName("payment_adjustment_id")]
    public long PaymentAdjustmentId { get; set; }
    /// <summary>
    /// Gets or sets the Payment Adjustment Item Id.
    /// </summary>

    [JsonPropertyName("payment_adjustment_item_id")]
    public long PaymentAdjustmentItemId { get; set; }
    /// <summary>
    /// Gets or sets the Adjustment Type.
    /// </summary>

    [JsonPropertyName("adjustment_type")]
    public string? AdjustmentType { get; set; }
    /// <summary>
    /// Gets or sets the Amount.
    /// </summary>

    [JsonPropertyName("amount")]
    public long Amount { get; set; }
    /// <summary>
    /// Gets or sets the Shop Amount.
    /// </summary>

    [JsonPropertyName("shop_amount")]
    public long ShopAmount { get; set; }
    /// <summary>
    /// Gets or sets the Transaction Id.
    /// </summary>

    [JsonPropertyName("transaction_id")]
    public long? TransactionId { get; set; }
    /// <summary>
    /// Gets or sets the Bill Payment Id.
    /// </summary>

    [JsonPropertyName("bill_payment_id")]
    public long? BillPaymentId { get; set; }
    /// <summary>
    /// Gets or sets the Created Timestamp.
    /// </summary>

    [JsonPropertyName("created_timestamp")]
    public long CreatedTimestamp { get; set; }
    /// <summary>
    /// Gets or sets the Updated Timestamp.
    /// </summary>

    [JsonPropertyName("updated_timestamp")]
    public long UpdatedTimestamp { get; set; }
}
