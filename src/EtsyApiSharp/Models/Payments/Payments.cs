using System.Text.Json.Serialization;


namespace EtsyApiSharp.Models
{
    //Represents several payments made with Etsy Payments. All monetary amounts are in USD pennies unless otherwise specified.
    public class Payments
    {
        [JsonPropertyName("count")]
        public int Count { get; set; }


        [JsonPropertyName("results")]
        public List<Payments> Results { get; set; }


    }
}
