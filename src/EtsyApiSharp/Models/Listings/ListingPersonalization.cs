using System.Text.Json.Serialization;

namespace EtsyApiSharp.Models;
/// <summary>
/// Represents Listing Personalization.
/// </summary>

public class ListingPersonalization
{
    /// <summary>
    /// Gets or sets the Listing Id.
    /// </summary>
    [JsonPropertyName("listing_id")]
    public long ListingId { get; set; }
    /// <summary>
    /// Executes the Empty operation.
    /// </summary>
    [JsonPropertyName("personalization_questions")]
    public IReadOnlyCollection<ListingPersonalizationQuestion> PersonalizationQuestions { get; set; } = Array.Empty<ListingPersonalizationQuestion>();
}
