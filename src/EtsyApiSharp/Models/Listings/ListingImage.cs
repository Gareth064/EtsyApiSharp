using System.Text.Json.Serialization;

namespace EtsyApiSharp.Models
{
    //Reference urls and metadata for an image associated with a specific listing. The `url_fullxfull` parameter contains the URL for full-sized binary image file.
    public class ListingImage
    {
        [JsonPropertyName("listing_id")]
        public int ListingId { get; set; }


        [JsonPropertyName("listing_image_id")]
        public int ListingImageId { get; set; }


        [JsonPropertyName("hex_code")]
        public string HexCode { get; set; }


        [JsonPropertyName("red")]
        public int Red { get; set; }


        [JsonPropertyName("green")]
        public int Green { get; set; }


        [JsonPropertyName("blue")]
        public int Blue { get; set; }


        [JsonPropertyName("hue")]
        public int Hue { get; set; }


        [JsonPropertyName("saturation")]
        public int Saturation { get; set; }


        [JsonPropertyName("brightness")]
        public int Brightness { get; set; }


        [JsonPropertyName("is_black_and_white")]
        public bool IsBlackAndWhite { get; set; }


        [JsonPropertyName("creation_tsz")]
        public int CreationTsz { get; set; }


        [JsonPropertyName("rank")]
        public int Rank { get; set; }


        [JsonPropertyName("url_75x75")]
        public string Url75X75 { get; set; }


        [JsonPropertyName("url_170x135")]
        public string Url170X135 { get; set; }


        [JsonPropertyName("url_570xN")]
        public string Url570Xn { get; set; }


        [JsonPropertyName("url_fullxfull")]
        public string UrlFullxfull { get; set; }


        [JsonPropertyName("full_height")]
        public int FullHeight { get; set; }


        [JsonPropertyName("full_width")]
        public int FullWidth { get; set; }


    }
}
