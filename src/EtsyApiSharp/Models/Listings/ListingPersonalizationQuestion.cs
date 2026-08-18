using System.Text.Json.Serialization;

namespace EtsyApiSharp.Models;
/// <summary>
/// Represents Listing Personalization Question.
/// </summary>

public class ListingPersonalizationQuestion
{
    /// <summary>
    /// Gets or sets the Question Id.
    /// </summary>
    [JsonPropertyName("question_id")]
    public long? QuestionId { get; set; }
    /// <summary>
    /// Gets or sets the Question Text.
    /// </summary>
    [JsonPropertyName("question_text")]
    public string QuestionText { get; set; } = string.Empty;
    /// <summary>
    /// Gets or sets the Instructions.
    /// </summary>
    [JsonPropertyName("instructions")]
    public string? Instructions { get; set; }
    /// <summary>
    /// Gets or sets the Question Type.
    /// </summary>
    [JsonPropertyName("question_type")]
    public string QuestionType { get; set; } = string.Empty;
    /// <summary>
    /// Gets or sets the Required.
    /// </summary>
    [JsonPropertyName("required")]
    public bool Required { get; set; }
    /// <summary>
    /// Gets or sets the Max Allowed Files.
    /// </summary>
    [JsonPropertyName("max_allowed_files")]
    public int? MaxAllowedFiles { get; set; }
    /// <summary>
    /// Gets or sets the Max Allowed Characters.
    /// </summary>
    [JsonPropertyName("max_allowed_characters")]
    public int? MaxAllowedCharacters { get; set; }
    /// <summary>
    /// Gets or sets the Options.
    /// </summary>
    [JsonPropertyName("options")]
    public IReadOnlyCollection<ListingPersonalizationOption>? Options { get; set; }
}
