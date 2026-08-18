namespace EtsyApiSharp.Models;
/// <summary>
/// Represents Listing Video Upload Request.
/// </summary>

public class ListingVideoUploadRequest
{
    /// <summary>
    /// Gets or sets the Video Id.
    /// </summary>
    public long? VideoId { get; set; }
    /// <summary>
    /// Gets or sets the Video.
    /// </summary>
    public Stream? Video { get; set; }
    /// <summary>
    /// Gets or sets the File Name.
    /// </summary>
    public string? FileName { get; set; }
}
