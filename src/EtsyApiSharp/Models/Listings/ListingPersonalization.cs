using System.Text.Json.Serialization;

namespace EtsyApiSharp.Models;

public class ListingPersonalization
{
    [JsonPropertyName("listing_id")]
    public long ListingId { get; set; }
    [JsonPropertyName("personalization_questions")]
    public IReadOnlyCollection<ListingPersonalizationQuestion> PersonalizationQuestions { get; set; } = Array.Empty<ListingPersonalizationQuestion>();
}
