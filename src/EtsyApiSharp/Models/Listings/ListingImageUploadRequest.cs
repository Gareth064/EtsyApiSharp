namespace EtsyApiSharp.Models;
/// <summary>
/// Represents Listing Image Upload Request.
/// </summary>

public class ListingImageUploadRequest
{
    /// <summary>
    /// Gets or sets the Image.
    /// </summary>
    public Stream Image { get; set; } = Stream.Null;
    /// <summary>
    /// Gets or sets the File Name.
    /// </summary>
    public string FileName { get; set; } = string.Empty;
    /// <summary>
    /// Gets or sets the Listing Image Id.
    /// </summary>
    public long? ListingImageId { get; set; }
    /// <summary>
    /// Gets or sets the Rank.
    /// </summary>
    public int? Rank { get; set; }
    /// <summary>
    /// Gets or sets the Overwrite.
    /// </summary>
    public bool? Overwrite { get; set; }
    /// <summary>
    /// Gets or sets the Is Watermarked.
    /// </summary>
    public bool? IsWatermarked { get; set; }
    /// <summary>
    /// Gets or sets the Alt Text.
    /// </summary>
    public string? AltText { get; set; }
}
