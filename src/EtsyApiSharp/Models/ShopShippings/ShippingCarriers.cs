using System.Text.Json.Serialization;

namespace EtsyApiSharp.Models
{
    //Represents several ShippingCarriers.
    public class ShippingCarriers
    {
        [JsonPropertyName("count")]
        public int Count { get; set; }


        [JsonPropertyName("results")]
        public List<ShippingCarrier> Results { get; set; }


    }
}
