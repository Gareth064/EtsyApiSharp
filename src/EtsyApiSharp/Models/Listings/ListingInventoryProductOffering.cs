using System.Text.Json.Serialization;

namespace EtsyApiSharp.Models
{
    //A representation of an offering for a listing.
    public class ListingInventoryProductOffering
    {
        [JsonPropertyName("offering_id")]
        public int OfferingId { get; set; }


        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }


        [JsonPropertyName("is_enabled")]
        public bool IsEnabled { get; set; }


        [JsonPropertyName("is_deleted")]
        public bool IsDeleted { get; set; }


        [JsonPropertyName("price")]
        public Money Price { get; set; }


    }
}
