using System.Text.Json.Serialization;


namespace EtsyApiSharp.Models
{
    //A representation of a product for a listing.
    public class ListingInventoryProduct
    {
        [JsonPropertyName("product_id")]
        public int ProductId { get; set; }


        [JsonPropertyName("sku")]
        public string Sku { get; set; }


        [JsonPropertyName("is_deleted")]
        public bool IsDeleted { get; set; }


        [JsonPropertyName("offerings")]
        public List<ListingInventoryProductOffering> Offerings { get; set; }


        [JsonPropertyName("property_values")]
        public List<ListingPropertyValue> PropertyValues { get; set; }


    }
}
