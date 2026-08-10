using System.Text.Json.Serialization;

namespace EtsyApiSharp.Models;

public class ReceiptShipmentCustomsData
{
    [JsonPropertyName("country_of_origin")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string? CountryOfOrigin { get; set; }

    [JsonPropertyName("declared_value")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public float? DeclaredValue { get; set; }

    [JsonPropertyName("HS_code")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string? HsCode { get; set; }
}
