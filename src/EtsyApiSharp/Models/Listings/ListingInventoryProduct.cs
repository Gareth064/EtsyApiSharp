using System.Text.Json.Serialization;

namespace EtsyApiSharp.Models
{
    /// <summary>
    /// A representation of a product for a listing.
    /// </summary>
    public class ListingInventoryProduct
    {
        /// <summary>
        /// The numeric ID for a specific [product](/documentation/reference#tag/ShopListing-Product) purchased from a listing.
        /// </summary>
        [JsonPropertyName("product_id")]
        public long ProductId { get; set; }

        /// <summary>
        /// The SKU string for the product
        /// </summary>
        [JsonPropertyName("sku")]
        public string Sku { get; set; }

        /// <summary>
        /// When true, someone deleted this product.
        /// </summary>
        [JsonPropertyName("is_deleted")]
        public bool IsDeleted { get; set; }

        /// <summary>
        /// A list of product offering entries for this product.
        /// </summary>
        [JsonPropertyName("offerings")]
        public string[] Offerings { get; set; }

        /// <summary>
        /// A list of property value entries for this product.
        /// </summary>
        [JsonPropertyName("property_values")]
        public string[] PropertyValues { get; set; }

    }
}
