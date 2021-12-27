using System.Text.Json.Serialization;

namespace EtsyApiSharp.Models
{
    //A taxonomy node in the seller taxonomy tree.
    public class SellerTaxonomyNode
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }


        [JsonPropertyName("level")]
        public int Level { get; set; }


        [JsonPropertyName("name")]
        public string Name { get; set; }


        [JsonPropertyName("parent_id")]
        public int ParentId { get; set; }


        [JsonPropertyName("children")]
        public List<SellerTaxonomyNode> Children { get; set; }


        [JsonPropertyName("full_path_taxonomy_ids")]
        public List<long> FullPathTaxonomyIds { get; set; }


    }
}
