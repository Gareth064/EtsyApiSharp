using System.Text.Json.Serialization;

namespace EtsyApiSharp.Models;

public class ListingPersonalizationUpdateRequest
{
    [JsonPropertyName("personalization_questions")]
    public IReadOnlyCollection<ListingPersonalizationQuestion> PersonalizationQuestions { get; set; } = Array.Empty<ListingPersonalizationQuestion>();
}
