using System.Text.Json.Serialization;


namespace EtsyApiSharp.Models
{
    //A list of taxonomy nodes from the seller taxonomy tree.
    public class SellerTaxonomyNodes
    {
        [JsonPropertyName("count")]
        public int Count { get; set; }


        [JsonPropertyName("results")]
        public List<SellerTaxonomyNodes> Results { get; set; }


    }
}
