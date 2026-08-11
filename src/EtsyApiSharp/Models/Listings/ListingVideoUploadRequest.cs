namespace EtsyApiSharp.Models;

public class ListingVideoUploadRequest
{
    public long? VideoId { get; set; }
    public Stream? Video { get; set; }
    public string? FileName { get; set; }
}
