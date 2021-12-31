using System.Text.Json.Serialization;

namespace EtsyApiSharp.Models
{
    /// <summary>
    /// A taxonomy node in the seller taxonomy tree.
    /// </summary>
    public class SellerTaxonomyNode
    {
        /// <summary>
        /// The unique numeric ID of an Etsy taxonomy node, which is a metadata category for listings organized into the seller taxonomy hierarchy tree. For example, the \"shoes\" taxonomy node (ID: 1429, level: 1) is higher in the hierarchy than \"girls' shoes\" (ID: 1440, level: 2). The taxonomy nodes assigned to a listing support access to specific standardized product scales and properties. For example, listings assigned the taxonomy nodes \"shoes\" or \"girls' shoes\" support access to the \"EU\" shoe size scale with its associated property names and IDs for EU shoe sizes, such as property `value_id`:\"1394\", and `name`:\"38\".
        /// </summary>
        [JsonPropertyName("id")]
        public int Id { get; set; }

        /// <summary>
        /// The integer depth of this taxonomy node in the seller taxonomy tree, with roots at level 0.
        /// </summary>
        [JsonPropertyName("level")]
        public int Level { get; set; }

        /// <summary>
        /// The name string for this taxonomy node.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// The numeric taxonomy ID of the parent of this node.
        /// </summary>
        [JsonPropertyName("parent_id")]
        public long? ParentId { get; set; }

        /// <summary>
        /// An array of taxonomy nodes for all the direct children of this taxonomy node in the seller taxanomy tree.
        /// </summary>
        [JsonPropertyName("children")]
        public string[] Children { get; set; }

        /// <summary>
        /// An array of `taxonomy_id`s including this node and all of its direct parents in the seller taxonomy tree up to a root node. They are listed in order from root to leaf.
        /// </summary>
        [JsonPropertyName("full_path_taxonomy_ids")]
        public string[] FullPathTaxonomyIds { get; set; }

    }
}
