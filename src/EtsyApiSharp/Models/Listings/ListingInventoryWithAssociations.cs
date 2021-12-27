using System.Text.Json.Serialization;

namespace EtsyApiSharp.Models
{
    //A representation of a single listing's inventory record with associations
    public class ListingInventoryWithAssociations
    {
        [JsonPropertyName("products")]
        public List<ListingInventoryProduct> Products { get; set; }

        [JsonPropertyName("price_on_property")]
        public List<long> PriceOnProperty { get; set; }

        [JsonPropertyName("quantity_on_property")]
        public List<long> QuantityOnProperty { get; set; }

        [JsonPropertyName("sku_on_property")]
        public List<long> SkuOnProperty { get; set; }

        [JsonPropertyName("listing")]
        public ShopListing Listing { get; set; }


    }
}
