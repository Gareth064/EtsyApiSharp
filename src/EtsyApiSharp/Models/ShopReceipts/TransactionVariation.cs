using System.Text.Json.Serialization;

namespace EtsyApiSharp.Models;

/// <summary>
/// A variation chosen by the buyer during checkout.
/// </summary>
public class TransactionVariation
{
    /// <summary>
    /// The variation property ID.
    /// </summary>
    [JsonPropertyName("property_id")]
    public long PropertyId { get; set; }

    /// <summary>
    /// The ID of the variation value selected.
    /// </summary>
    [JsonPropertyName("value_id")]
    public long? ValueId { get; set; }

    /// <summary>
    /// Formatted name of the variation.
    /// </summary>
    [JsonPropertyName("formatted_name")]
    public string FormattedName { get; set; }

    /// <summary>
    /// Value of the variation entered by the buyer.
    /// </summary>
    [JsonPropertyName("formatted_value")]
    public string FormattedValue { get; set; }

    /// <summary>
    /// The ID of the original personalization question, when this entry represents a personalization.
    /// </summary>
    [JsonPropertyName("question_id")]
    public long? QuestionId { get; set; }

}
