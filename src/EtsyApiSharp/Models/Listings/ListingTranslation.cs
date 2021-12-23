using Newtonsoft.Json;

namespace EtsyApiSharp.Models
{
    //Represents the translation data for a Listing.
    public class ListingTranslation
    {
        [JsonProperty("listing_id")]
        public long ListingId { get; set; }


        [JsonProperty("language")]
        public string Language { get; set; }


        [JsonProperty("title")]
        public string Title { get; set; }


        [JsonProperty("description")]
        public string Description { get; set; }


        [JsonProperty("tags")]
        public string[] Tags { get; set; }


    }
}
