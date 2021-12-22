using System.Text.Json.Serialization;


namespace EtsyApiSharp.Models
{
    //A representation of an amount of money.
    public class Money
    {
        [JsonPropertyName("amount")]
        public int Amount { get; set; }
        [JsonPropertyName("divisor")]
        public int Divisor { get; set; }
        [JsonPropertyName("currency_code")]
        public string CurrencyCode { get; set; }
    }
}
