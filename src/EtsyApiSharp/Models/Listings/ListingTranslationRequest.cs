namespace EtsyApiSharp.Models;

public class ListingTranslationRequest
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public IReadOnlyCollection<string>? Tags { get; set; }
}
