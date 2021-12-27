using System.Text.Json.Serialization;

namespace EtsyApiSharp.Models
{
    //A shipping carrier's mail class, which is used to calculate an Estimated Delivery Date.
    public class ShippingCarrierMailClass
    {
        [JsonPropertyName("mail_class_key")]
        public string MailClassKey { get; set; }


        [JsonPropertyName("name")]
        public string Name { get; set; }


    }
}
