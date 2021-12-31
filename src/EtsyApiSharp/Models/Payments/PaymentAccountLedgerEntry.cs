using System.Text.Json.Serialization;

namespace EtsyApiSharp.Models
{
    /// <summary>
    /// Represents an entry in a shop's ledger.
    /// </summary>
    public class PaymentAccountLedgerEntry
    {
        [JsonPropertyName("entry_id")]
        public int EntryId { get; set; }

        /// <summary>
        /// Ledger ID
        /// </summary>
        [JsonPropertyName("ledger_id")]
        public int LedgerId { get; set; }


        [JsonPropertyName("sequence_number")]
        public int SequenceNumber { get; set; }


        [JsonPropertyName("amount")]
        public int Amount { get; set; }


        [JsonPropertyName("currency")]
        public string Currency { get; set; }


        [JsonPropertyName("description")]
        public string Description { get; set; }


        [JsonPropertyName("balance")]
        public int Balance { get; set; }


        [JsonPropertyName("create_date")]
        public int CreateDate { get; set; }


        [JsonPropertyName("ledger_type")]
        public string LedgerType { get; set; }


    }
}
