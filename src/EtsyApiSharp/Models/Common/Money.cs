using System.Text.Json.Serialization;

namespace EtsyApiSharp.Models
{
    /// <summary>
    /// A representation of an amount of money.
    /// </summary>
    public class Money
    {
        /// <summary>
        /// The amount of represented by this data.
        /// </summary>
        [JsonPropertyName("amount")]
        public int Amount { get; set; }

        /// <summary>
        /// The divisor to render the amount.
        /// </summary>
        [JsonPropertyName("divisor")]
        public int Divisor { get; set; }

        /// <summary>
        /// The ISO currency code for this data.
        /// </summary>
        [JsonPropertyName("currency_code")]
        public string CurrencyCode { get; set; }

    }
}
