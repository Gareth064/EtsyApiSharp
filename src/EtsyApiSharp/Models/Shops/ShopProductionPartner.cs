using System.Text.Json.Serialization;

namespace EtsyApiSharp.Models
{
    /// <summary>
    /// Represents a description of a shop production partner.
    /// </summary>
    public class ShopProductionPartner
    {
        /// <summary>
        /// The numeric ID of a production partner.
        /// </summary>
        [JsonPropertyName("production_partner_id")]
        public long ProductionPartnerId { get; set; }

        /// <summary>
        /// The name or title of the production partner.
        /// </summary>
        [JsonPropertyName("partner_name")]
        public string PartnerName { get; set; }

        /// <summary>
        /// A string representing the production partner location.
        /// </summary>
        [JsonPropertyName("location")]
        public string Location { get; set; }

    }
}
