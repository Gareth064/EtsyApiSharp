using System.Text.Json.Serialization;

namespace EtsyApiSharp.Models;

/// <summary>
/// Reference urls and metadata for an image associated with a specific listing. The `url_fullxfull` parameter contains the URL for full-sized binary image file.
/// </summary>
public class ListingImage
{
    /// <summary>
    /// The numeric ID for the [listing](/documentation/reference#tag/ShopListing) associated to this transaction.
    /// </summary>
    [JsonPropertyName("listing_id")]
    public long ListingId { get; set; }

    /// <summary>
    /// The numeric ID of the primary [listing image](/documentation/reference#tag/ShopListing-Image) for this transaction.
    /// </summary>
    [JsonPropertyName("listing_image_id")]
    public long ListingImageId { get; set; }

    /// <summary>
    /// The webhex string for the image's average color, in webhex notation.
    /// </summary>
    [JsonPropertyName("hex_code")]
    public string HexCode { get; set; }

    /// <summary>
    /// The numeric red value equal to the image's average red value, from 0-255 (RGB color).
    /// </summary>
    [JsonPropertyName("red")]
    public int? Red { get; set; }

    /// <summary>
    /// The numeric red value equal to the image's average red value, from 0-255 (RGB color).
    /// </summary>
    [JsonPropertyName("green")]
    public int? Green { get; set; }

    /// <summary>
    /// The numeric red value equal to the image's average red value, from 0-255 (RGB color).
    /// </summary>
    [JsonPropertyName("blue")]
    public int? Blue { get; set; }

    /// <summary>
    /// The numeric hue equal to the image's average hue, from 0-360 (HSV color).
    /// </summary>
    [JsonPropertyName("hue")]
    public int? Hue { get; set; }

    /// <summary>
    /// The numeric saturation equal to the image's average saturation, from 0-100 (HSV color).
    /// </summary>
    [JsonPropertyName("saturation")]
    public int? Saturation { get; set; }

    /// <summary>
    /// The numeric brightness equal to the image's average brightness, from 0-100 (HSV color).
    /// </summary>
    [JsonPropertyName("brightness")]
    public int? Brightness { get; set; }

    /// <summary>
    /// When true, the image is in black &amp; white.
    /// </summary>
    [JsonPropertyName("is_black_and_white")]
    public bool? IsBlackAndWhite { get; set; }

    /// <summary>
    /// The listing image\'s creation time, in epoch seconds.
    /// </summary>
    [JsonPropertyName("creation_tsz")]
    public int CreationTsz { get; set; }

    /// <summary>
    /// The listing image\'s creation time, in epoch seconds.
    /// </summary>
    [JsonPropertyName("created_timestamp")]
    public int CreatedTimestamp { get; set; }

    /// <summary>
    /// The positive non-zero numeric position in the images displayed in a listing, with rank 1 images appearing in the left-most position in a listing.
    /// </summary>
    [JsonPropertyName("rank")]
    public int Rank { get; set; }

    /// <summary>
    /// The url string for a 75x75 pixel thumbnail of the image.
    /// </summary>
    [JsonPropertyName("url_75x75")]
    public string Url75X75 { get; set; }

    /// <summary>
    /// The url string for a 170x135 pixel thumbnail of the image.
    /// </summary>
    [JsonPropertyName("url_170x135")]
    public string Url170X135 { get; set; }

    /// <summary>
    /// The url string for a thumbnail of the image, no more than 570 pixels wide with variable height.
    /// </summary>
    [JsonPropertyName("url_570xN")]
    public string Url570Xn { get; set; }

    /// <summary>
    /// The url string for the full-size image, up to 3000 pixels in each dimension.
    /// </summary>
    [JsonPropertyName("url_fullxfull")]
    public string UrlFullxfull { get; set; }

    /// <summary>
    /// The numeric height, measured in pixels, of the full-sized image referenced in url_fullxfull.
    /// </summary>
    [JsonPropertyName("full_height")]
    public int? FullHeight { get; set; }

    /// <summary>
    /// The numeric width, measured in pixels, of the full-sized image referenced in url_fullxfull.
    /// </summary>
    [JsonPropertyName("full_width")]
    public int? FullWidth { get; set; }

    /// <summary>
    /// Alt text for the listing image.
    /// </summary>
    [JsonPropertyName("alt_text")]
    public string AltText { get; set; }

}
