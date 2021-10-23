using System.Text.Json.Serialization;


namespace EtsyApiSharp.Models
{
    //Represents a list of shop production partners.
    public class ShopProductionPartners
    {
        [JsonPropertyName("count")]
        public int Count { get; set; }


        [JsonPropertyName("results")]
        public List<ShopProductionPartner> Results { get; set; }


    }
}
