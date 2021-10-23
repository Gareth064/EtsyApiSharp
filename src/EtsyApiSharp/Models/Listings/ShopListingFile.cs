using System.Text.Json.Serialization;


namespace EtsyApiSharp.Models
{
    //A file associated with a digital listing.
    public class ShopListingFile
    {
        [JsonPropertyName("listing_file_id")]
        public int ListingFileId { get; set; }


        [JsonPropertyName("listing_id")]
        public int ListingId { get; set; }


        [JsonPropertyName("rank")]
        public int Rank { get; set; }


        [JsonPropertyName("filename")]
        public string Filename { get; set; }


        [JsonPropertyName("filesize")]
        public string Filesize { get; set; }


        [JsonPropertyName("size_bytes")]
        public int SizeBytes { get; set; }


        [JsonPropertyName("filetype")]
        public string Filetype { get; set; }


        [JsonPropertyName("create_timestamp")]
        public int CreateTimestamp { get; set; }


    }
}
