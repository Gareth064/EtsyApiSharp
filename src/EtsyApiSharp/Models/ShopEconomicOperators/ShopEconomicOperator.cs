using System.Text.Json.Serialization;

namespace EtsyApiSharp.Models;

/// <summary>
/// An economic operator (manufacturer or EU responsible person) for GPSR compliance.
/// </summary>
public class ShopEconomicOperator
{
    /// <summary>Gets or sets the economic operator ID.</summary>
    [JsonPropertyName("economic_operator_id")]
    public long EconomicOperatorId { get; set; }

    /// <summary>Gets or sets the shop ID.</summary>
    [JsonPropertyName("shop_id")]
    public long ShopId { get; set; }

    /// <summary>Gets or sets the operator's legal or trading name.</summary>
    [JsonPropertyName("operator_name")]
    public string? OperatorName { get; set; }

    /// <summary>Gets or sets the contact email address.</summary>
    [JsonPropertyName("email")]
    public string? Email { get; set; }

    /// <summary>Gets or sets the trademark name.</summary>
    [JsonPropertyName("trademark_name")]
    public string? TrademarkName { get; set; }

    /// <summary>Gets or sets the first address line.</summary>
    [JsonPropertyName("first_line")]
    public string? FirstLine { get; set; }

    /// <summary>Gets or sets the second address line.</summary>
    [JsonPropertyName("second_line")]
    public string? SecondLine { get; set; }

    /// <summary>Gets or sets the city.</summary>
    [JsonPropertyName("city")]
    public string? City { get; set; }

    /// <summary>Gets or sets the state, province, or region.</summary>
    [JsonPropertyName("state")]
    public string? State { get; set; }

    /// <summary>Gets or sets the postal code.</summary>
    [JsonPropertyName("zip")]
    public string? Zip { get; set; }

    /// <summary>Gets or sets the ISO country code.</summary>
    [JsonPropertyName("country_iso")]
    public string? CountryIso { get; set; }

    /// <summary>Gets or sets the contact telephone number.</summary>
    [JsonPropertyName("phone")]
    public string? Phone { get; set; }

    /// <summary>Gets or sets the telephone country code.</summary>
    [JsonPropertyName("phone_country_iso")]
    public string? PhoneCountryIso { get; set; }

    /// <summary>Gets or sets the creation timestamp.</summary>
    [JsonPropertyName("create_date")]
    public long CreateDate { get; set; }

    /// <summary>Gets or sets the last update timestamp.</summary>
    [JsonPropertyName("update_date")]
    public long UpdateDate { get; set; }
}
