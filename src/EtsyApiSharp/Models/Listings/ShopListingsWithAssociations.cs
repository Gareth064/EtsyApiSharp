using System.Text.Json.Serialization;


namespace EtsyApiSharp.Models
{
    //A set of ShopListing resources with associations.
    public class ShopListingsWithAssociations
    {
        [JsonPropertyName("count")]
        public int Count { get; set; }


        [JsonPropertyName("results")]
        public List<ShopListingWithAssociations> Results { get; set; }


    }
}
