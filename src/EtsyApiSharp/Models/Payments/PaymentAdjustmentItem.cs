using System.Text.Json.Serialization;

namespace EtsyApiSharp.Models;

/// <summary>
/// Represents a line item for a payment adjustment.
/// </summary>
public class PaymentAdjustmentItem
{
    [JsonPropertyName("payment_adjustment_id")]
    public long PaymentAdjustmentId { get; set; }

    [JsonPropertyName("payment_adjustment_item_id")]
    public long PaymentAdjustmentItemId { get; set; }

    [JsonPropertyName("adjustment_type")]
    public string? AdjustmentType { get; set; }

    [JsonPropertyName("amount")]
    public long Amount { get; set; }

    [JsonPropertyName("shop_amount")]
    public long ShopAmount { get; set; }

    [JsonPropertyName("transaction_id")]
    public long? TransactionId { get; set; }

    [JsonPropertyName("bill_payment_id")]
    public long? BillPaymentId { get; set; }

    [JsonPropertyName("created_timestamp")]
    public long CreatedTimestamp { get; set; }

    [JsonPropertyName("updated_timestamp")]
    public long UpdatedTimestamp { get; set; }
}
