namespace EtsyApiSharp.Models.Listings.Enums;

/// <summary>
/// Identifies the source of a GPSR manufacturer or EU responsible person.
/// </summary>
public enum GpsrEconomicOperatorType
{
    /// <summary>The seller's shop is the economic operator.</summary>
    seller,

    /// <summary>An economic operator record belonging to the shop is used.</summary>
    economic_operator
}
