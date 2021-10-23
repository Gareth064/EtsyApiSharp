using System.Text.Json.Serialization;


namespace EtsyApiSharp.Models
{
    //The receipts for a specific Shop.
    public class ShopReceipts
    {
        [JsonPropertyName("count")]
        public int Count { get; set; }


        [JsonPropertyName("results")]
        public List<ShopReceipt> Results { get; set; }


    }
}
