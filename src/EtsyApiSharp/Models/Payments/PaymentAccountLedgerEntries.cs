using System.Text.Json.Serialization;


namespace EtsyApiSharp.Models
{
    //A set of PaymentAccountLedgerEntry resources
    public class PaymentAccountLedgerEntries
    {
        [JsonPropertyName("count")]
        public int Count { get; set; }


        [JsonPropertyName("results")]
        public List<PaymentAccountLedgerEntry> Results { get; set; }


    }
}
