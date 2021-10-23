using System.Text.Json.Serialization;


namespace EtsyApiSharp.Models
{
    //Represents the translation data for a Listing.
    public class ListingTranslation
    {
        [JsonPropertyName("listing_id")]
        public int ListingId { get; set; }


        [JsonPropertyName("language")]
        public string Language { get; set; }


        [JsonPropertyName("title")]
        public string Title { get; set; }


        [JsonPropertyName("description")]
        public string Description { get; set; }


        [JsonPropertyName("tags")]
        public string[] Tags { get; set; }


    }
}
