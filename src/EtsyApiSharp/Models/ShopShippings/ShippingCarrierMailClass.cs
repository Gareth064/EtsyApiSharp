using System.Text.Json.Serialization;

namespace EtsyApiSharp.Models
{
    /// <summary>
    /// A shipping carrier's mail class, which is used to calculate an Estimated Delivery Date.
    /// </summary>
    public class ShippingCarrierMailClass
    {
        /// <summary>
        /// The unique identifier of this mail class.
        /// </summary>
        [JsonPropertyName("mail_class_key")]
        public string MailClassKey { get; set; }

        /// <summary>
        /// The name of this mail class.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

    }
}
