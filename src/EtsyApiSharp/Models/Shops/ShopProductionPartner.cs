using System.Text.Json.Serialization;


namespace EtsyApiSharp.Models
{
    //Represents a description of a shop production partner.
    public class ShopProductionPartner
    {
        [JsonPropertyName("production_partner_id")]
        public int ProductionPartnerId { get; set; }


        [JsonPropertyName("partner_name")]
        public string PartnerName { get; set; }


        [JsonPropertyName("location")]
        public string Location { get; set; }


    }
}
