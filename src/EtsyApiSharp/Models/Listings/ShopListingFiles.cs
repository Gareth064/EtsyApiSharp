using System.Text.Json.Serialization;


namespace EtsyApiSharp.Models
{
    //Represents several ShopListingFiles.
    public class ShopListingFiles
    {
        [JsonPropertyName("count")]
        public int Count { get; set; }


        [JsonPropertyName("results")]
        public List<ShopListingFile> Results { get; set; }


    }
}
