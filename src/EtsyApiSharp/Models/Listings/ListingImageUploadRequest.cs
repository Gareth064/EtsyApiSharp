namespace EtsyApiSharp.Models;

public class ListingImageUploadRequest
{
    public Stream Image { get; set; } = Stream.Null;
    public string FileName { get; set; } = string.Empty;
    public long? ListingImageId { get; set; }
    public int? Rank { get; set; }
    public bool? Overwrite { get; set; }
    public bool? IsWatermarked { get; set; }
    public string? AltText { get; set; }
}
