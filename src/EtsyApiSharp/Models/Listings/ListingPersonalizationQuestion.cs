using System.Text.Json.Serialization;

namespace EtsyApiSharp.Models;

public class ListingPersonalizationQuestion
{
    [JsonPropertyName("question_id")]
    public long? QuestionId { get; set; }
    [JsonPropertyName("question_text")]
    public string QuestionText { get; set; } = string.Empty;
    [JsonPropertyName("instructions")]
    public string? Instructions { get; set; }
    [JsonPropertyName("question_type")]
    public string QuestionType { get; set; } = string.Empty;
    [JsonPropertyName("required")]
    public bool Required { get; set; }
    [JsonPropertyName("max_allowed_files")]
    public int? MaxAllowedFiles { get; set; }
    [JsonPropertyName("max_allowed_characters")]
    public int? MaxAllowedCharacters { get; set; }
    [JsonPropertyName("options")]
    public IReadOnlyCollection<ListingPersonalizationOption>? Options { get; set; }
}
