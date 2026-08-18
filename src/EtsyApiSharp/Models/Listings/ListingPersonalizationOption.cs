using System.Text.Json.Serialization;

namespace EtsyApiSharp.Models;
/// <summary>
/// Represents Listing Personalization Option.
/// </summary>

public class ListingPersonalizationOption
{
    /// <summary>
    /// Gets or sets the Option Id.
    /// </summary>
    [JsonPropertyName("option_id")]
    public long? OptionId { get; set; }
    /// <summary>
    /// Gets or sets the Label.
    /// </summary>
    [JsonPropertyName("label")]
    public string Label { get; set; } = string.Empty;
    /// <summary>
    /// Gets or sets the Add On Price.
    /// </summary>
    [JsonPropertyName("add_on_price")]
    public decimal? AddOnPrice { get; set; }
}
