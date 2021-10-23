using System.Text.Json.Serialization;


namespace EtsyApiSharp.Models
{
    //A supported shipping carrier, which is used to calculate an Estimated Delivery Date.
    public class ShippingCarrier
    {
        [JsonPropertyName("shipping_carrier_id")]
        public int ShippingCarrierId { get; set; }


        [JsonPropertyName("name")]
        public string Name { get; set; }


        [JsonPropertyName("domestic_classes")]
        public List<ShippingCarrierMailClass> DomesticClasses { get; set; }


        [JsonPropertyName("international_classes")]
        public List<ShippingCarrierMailClass> InternationalClasses { get; set; }


    }
}
