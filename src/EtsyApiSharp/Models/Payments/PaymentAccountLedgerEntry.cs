using System.Text.Json.Serialization;

namespace EtsyApiSharp.Models;

/// <summary>
/// Represents an entry in a shop's ledger.
/// </summary>
public class PaymentAccountLedgerEntry
{
    /// <summary>
    /// The ledger entry's numeric ID.
    /// </summary>
    [JsonPropertyName("entry_id")]
    public long EntryId { get; set; }

    /// <summary>
    /// The ledger's numeric ID.
    /// </summary>
    [JsonPropertyName("ledger_id")]
    public long LedgerId { get; set; }

    /// <summary>
    /// The sequence allows ledger entries to be sorted chronologically. The higher the sequence, the more recent the entry.
    /// </summary>
    [JsonPropertyName("sequence_number")]
    public long SequenceNumber { get; set; }

    /// <summary>
    /// The amount of money credited to the ledger.
    /// </summary>
    [JsonPropertyName("amount")]
    public long Amount { get; set; }

    /// <summary>
    /// The currency of the entry on the ledger.
    /// </summary>
    [JsonPropertyName("currency")]
    public string? Currency { get; set; }

    /// <summary>
    /// Details what kind of ledger entry this is: a payment, refund, reversal of a failed refund, disbursement, returned disbursement, recoupment, miscellaneous credit, miscellaneous debit, or bill payment.
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; set; }

    /// <summary>
    /// The amount of money in the shop's ledger the moment after this entry was applied.
    /// </summary>
    [JsonPropertyName("balance")]
    public long Balance { get; set; }

    /// <summary>
    /// The date and time the ledger entry was created in Epoch seconds..
    /// </summary>
    [JsonPropertyName("create_date")]
    public long CreateDate { get; set; }

    /// <summary>
    /// The date and time the ledger entry was created in epoch seconds.
    /// </summary>
    [JsonPropertyName("created_timestamp")]
    public long CreatedTimestamp { get; set; }

    /// <summary>
    /// The original reference type for the ledger entry.
    /// </summary>
    [JsonPropertyName("ledger_type")]
    public string? LedgerType { get; set; }

    /// <summary>
    /// The object type the ledger entry refers to.
    /// </summary>
    [JsonPropertyName("reference_type")]
    public string? ReferenceType { get; set; }

    /// <summary>
    /// The object id the ledger entry refers to.
    /// </summary>
    [JsonPropertyName("reference_id")]
    public string? ReferenceId { get; set; }

    /// <summary>
    /// The parent ledger entry ID used to match related entries.
    /// </summary>
    [JsonPropertyName("parent_entry_id")]
    public long ParentEntryId { get; set; }

    /// <summary>
    /// Refunds associated with this ledger entry, when supplied by Etsy.
    /// </summary>
    [JsonPropertyName("payment_adjustments")]
    public List<PaymentAdjustment>? PaymentAdjustments { get; set; }

}
