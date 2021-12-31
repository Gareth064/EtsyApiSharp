using System.Text.Json.Serialization;

namespace EtsyApiSharp.Models
{
    /// <summary>
    /// A representation of a single listing's inventory record.
    /// </summary>
    public class ListingInventory
    {
        /// <summary>
        /// A JSON array of products available in a listing, even if only one product. All field names in the JSON blobs are lowercase.
        /// </summary>
        [JsonPropertyName("products")]
        public string[] Products { get; set; }

        /// <summary>
        /// An array of unique [listing property](/documentation/reference#operation/getListingProperties) ID integers for the properties that change product prices, if any. For example, if you charge specific prices for different sized products in the same listing, then this array contains the property ID for size.
        /// </summary>
        [JsonPropertyName("price_on_property")]
        public string[] PriceOnProperty { get; set; }

        /// <summary>
        /// An array of unique [listing property](/documentation/reference#operation/getListingProperties) ID integers for the properties that change the quantity of the products, if any. For example, if you stock specific quantities of different colored products in the same listing, then this array contains the property ID for color.
        /// </summary>
        [JsonPropertyName("quantity_on_property")]
        public string[] QuantityOnProperty { get; set; }

        /// <summary>
        /// An array of unique [listing property](/documentation/reference#operation/getListingProperties) ID integers for the properties that change the product SKU, if any. For example, if you use specific skus for different colored products in the same listing, then this array contains the property ID for color.
        /// </summary>
        [JsonPropertyName("sku_on_property")]
        public string[] SkuOnProperty { get; set; }

    }
}
