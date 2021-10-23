using System.Text.Json.Serialization;


namespace EtsyApiSharp.Models
{
    //A set of ShopReceiptTransaction resources
    public class ShopReceiptTransactions
    {
        [JsonPropertyName("count")]
        public int Count { get; set; }


        [JsonPropertyName("results")]
        public List<ShopReceiptTransaction> Results { get; set; }


    }
}
