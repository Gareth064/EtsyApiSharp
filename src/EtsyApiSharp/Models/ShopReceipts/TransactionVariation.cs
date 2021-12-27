using System.Text.Json.Serialization;

namespace EtsyApiSharp.Models
{
    //A list of variations chosen by the buyer during checkout.
    public class TransactionVariation
    {
        [JsonPropertyName("property_id")]
        public int PropertyId { get; set; }


        [JsonPropertyName("value_id")]
        public int ValueId { get; set; }


        [JsonPropertyName("formatted_name")]
        public string FormattedName { get; set; }


        [JsonPropertyName("formatted_value")]
        public string FormattedValue { get; set; }


    }
}
