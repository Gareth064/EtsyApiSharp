namespace EtsyApiSharp.Models;

/// <summary>
/// Values required to create a shop economic operator.
/// </summary>
public class CreateShopEconomicOperatorRequest
{
    /// <summary>Gets or sets the operator's legal or trading name.</summary>
    public string OperatorName { get; set; } = string.Empty;
    /// <summary>Gets or sets the operator's contact email address.</summary>
    public string Email { get; set; } = string.Empty;
    /// <summary>Gets or sets the first address line.</summary>
    public string FirstLine { get; set; } = string.Empty;
    /// <summary>Gets or sets the city.</summary>
    public string City { get; set; } = string.Empty;
    /// <summary>Gets or sets the postal code.</summary>
    public string Zip { get; set; } = string.Empty;
    /// <summary>Gets or sets the ISO 3166-1 alpha-2 country code.</summary>
    public string CountryIso { get; set; } = string.Empty;
    /// <summary>Gets or sets the contact telephone number.</summary>
    public string Phone { get; set; } = string.Empty;
    /// <summary>Gets or sets the second address line.</summary>
    public string? SecondLine { get; set; }
    /// <summary>Gets or sets the state, province, or region.</summary>
    public string? State { get; set; }
    /// <summary>Gets or sets the telephone number's ISO country code.</summary>
    public string? PhoneCountryIso { get; set; }
    /// <summary>Gets or sets the operator's trademark name.</summary>
    public string? TrademarkName { get; set; }
}
