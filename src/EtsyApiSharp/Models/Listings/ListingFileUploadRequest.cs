namespace EtsyApiSharp.Models;

public class ListingFileUploadRequest
{
    public Stream File { get; set; } = Stream.Null;
    public string FileName { get; set; } = string.Empty;
    public long? ListingFileId { get; set; }
    public int? Rank { get; set; }
}
