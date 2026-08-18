using System.Text.Json.Serialization;

namespace EtsyApiSharp.Models;
/// <summary>
/// Represents Receipt Shipment Customs Data.
/// </summary>

public class ReceiptShipmentCustomsData
{
    /// <summary>
    /// Gets or sets the Country Of Origin.
    /// </summary>
    [JsonPropertyName("country_of_origin")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string? CountryOfOrigin { get; set; }
    /// <summary>
    /// Gets or sets the Declared Value.
    /// </summary>

    [JsonPropertyName("declared_value")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public float? DeclaredValue { get; set; }
    /// <summary>
    /// Gets or sets the Hs Code.
    /// </summary>

    [JsonPropertyName("HS_code")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string? HsCode { get; set; }
}
