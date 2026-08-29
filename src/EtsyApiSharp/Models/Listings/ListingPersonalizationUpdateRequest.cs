using System.Text.Json.Serialization;

namespace EtsyApiSharp.Models;
/// <summary>
/// Represents Listing Personalization Update Request.
/// </summary>

public class ListingPersonalizationUpdateRequest
{
    /// <summary>
    /// Executes the Empty operation.
    /// </summary>
    [JsonPropertyName("personalization_questions")]
    public IReadOnlyCollection<ListingPersonalizationQuestion> PersonalizationQuestions { get; set; } = Array.Empty<ListingPersonalizationQuestion>();
}
