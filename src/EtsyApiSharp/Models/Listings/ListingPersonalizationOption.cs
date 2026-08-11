using System.Text.Json.Serialization;

namespace EtsyApiSharp.Models;

public class ListingPersonalizationOption
{
    [JsonPropertyName("option_id")]
    public long? OptionId { get; set; }
    [JsonPropertyName("label")]
    public string Label { get; set; } = string.Empty;
    [JsonPropertyName("add_on_price")]
    public decimal? AddOnPrice { get; set; }
}
