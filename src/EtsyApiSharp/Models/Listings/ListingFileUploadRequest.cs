namespace EtsyApiSharp.Models;
/// <summary>
/// Represents Listing File Upload Request.
/// </summary>

public class ListingFileUploadRequest
{
    /// <summary>
    /// Gets or sets the File.
    /// </summary>
    public Stream File { get; set; } = Stream.Null;
    /// <summary>
    /// Gets or sets the File Name.
    /// </summary>
    public string FileName { get; set; } = string.Empty;
    /// <summary>
    /// Gets or sets the Listing File Id.
    /// </summary>
    public long? ListingFileId { get; set; }
    /// <summary>
    /// Gets or sets the Rank.
    /// </summary>
    public int? Rank { get; set; }
}
