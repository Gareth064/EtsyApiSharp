using System.Text.Json.Serialization;

namespace EtsyApiSharp.Models
{
    /// <summary>
    /// A file associated with a digital listing.
    /// </summary>
    public class ShopListingFile
    {
        /// <summary>
        /// The unique numeric ID of a file associated with a digital listing.
        /// </summary>
        [JsonPropertyName("listing_file_id")]
        public long ListingFileId { get; set; }

        /// <summary>
        /// The numeric ID for the [listing](/documentation/reference#tag/ShopListing) associated to this transaction.
        /// </summary>
        [JsonPropertyName("listing_id")]
        public long ListingId { get; set; }

        /// <summary>
        /// The numeric index of the display order position of this file in the listing, starting at 1.
        /// </summary>
        [JsonPropertyName("rank")]
        public int Rank { get; set; }

        /// <summary>
        /// The file name string for a file associated with a digital listing.
        /// </summary>
        [JsonPropertyName("filename")]
        public string Filename { get; set; }

        /// <summary>
        /// A human-readable format size string for the size of a file.
        /// </summary>
        [JsonPropertyName("filesize")]
        public string Filesize { get; set; }

        /// <summary>
        /// A number indicating the size of a file, measured in bytes.
        /// </summary>
        [JsonPropertyName("size_bytes")]
        public int SizeBytes { get; set; }

        /// <summary>
        /// A type string indicating a file's MIME type.
        /// </summary>
        [JsonPropertyName("filetype")]
        public string Filetype { get; set; }

        /// <summary>
        /// The unique numeric ID of a file associated with a digital listing.
        /// </summary>
        [JsonPropertyName("create_timestamp")]
        public int CreateTimestamp { get; set; }

        /// <summary>
        /// The unique numeric ID of a file associated with a digital listing.
        /// </summary>
        [JsonPropertyName("created_timestamp")]
        public int CreatedTimestamp { get; set; }

    }
}
