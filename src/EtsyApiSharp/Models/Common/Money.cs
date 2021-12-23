using Newtonsoft.Json;

namespace EtsyApiSharp.Models
{
    //A representation of an amount of money.
    public class Money
    {
        [JsonProperty("amount")]
        public int Amount { get; set; }
        [JsonProperty("divisor")]
        public int Divisor { get; set; }
        [JsonProperty("currency_code")]
        public string CurrencyCode { get; set; }
    }
}
