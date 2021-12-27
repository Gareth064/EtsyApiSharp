using System.Text.Json.Serialization;

namespace EtsyApiSharp.Models
{
    //A section within a shop, into which a user can sort listings.
    public class ShopSection
    {
        [JsonPropertyName("shop_section_id")]
        public int ShopSectionId { get; set; }


        [JsonPropertyName("title")]
        public string Title { get; set; }


        [JsonPropertyName("rank")]
        public int Rank { get; set; }


        [JsonPropertyName("user_id")]
        public int UserId { get; set; }


        [JsonPropertyName("active_listing_count")]
        public int ActiveListingCount { get; set; }


    }
}
