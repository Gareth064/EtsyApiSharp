using System.Text.Json.Serialization;

namespace EtsyApiSharp.Models;
/// <summary>
/// Represents Create Receipt Shipment Request.
/// </summary>

public class CreateReceiptShipmentRequest
{
    /// <summary>
    /// Gets or sets the Tracking Code.
    /// </summary>
    [JsonPropertyName("tracking_code")]
    public string? TrackingCode { get; set; }
    /// <summary>
    /// Gets or sets the Carrier Name.
    /// </summary>

    [JsonPropertyName("carrier_name")]
    public string? CarrierName { get; set; }
    /// <summary>
    /// Gets or sets the Send Bcc.
    /// </summary>

    [JsonPropertyName("send_bcc")]
    public bool? SendBcc { get; set; }
    /// <summary>
    /// Gets or sets the Note To Buyer.
    /// </summary>

    [JsonPropertyName("note_to_buyer")]
    public string? NoteToBuyer { get; set; }
    /// <summary>
    /// Gets or sets the Mail Class.
    /// </summary>

    [JsonPropertyName("mail_class")]
    public string? MailClass { get; set; }
    /// <summary>
    /// Gets or sets the Weight.
    /// </summary>

    [JsonPropertyName("weight")]
    public float? Weight { get; set; }
    /// <summary>
    /// Gets or sets the Weight Units.
    /// </summary>

    [JsonPropertyName("weight_units")]
    public string? WeightUnits { get; set; }
    /// <summary>
    /// Gets or sets the Length.
    /// </summary>

    [JsonPropertyName("length")]
    public float? Length { get; set; }
    /// <summary>
    /// Gets or sets the Width.
    /// </summary>

    [JsonPropertyName("width")]
    public float? Width { get; set; }
    /// <summary>
    /// Gets or sets the Height.
    /// </summary>

    [JsonPropertyName("height")]
    public float? Height { get; set; }
    /// <summary>
    /// Gets or sets the Dimension Units.
    /// </summary>

    [JsonPropertyName("dimension_units")]
    public string? DimensionUnits { get; set; }
    /// <summary>
    /// Gets or sets the Shipping Label Cost.
    /// </summary>

    [JsonPropertyName("shipping_label_cost")]
    public float? ShippingLabelCost { get; set; }
    /// <summary>
    /// Gets or sets the Shipping Label Currency.
    /// </summary>

    [JsonPropertyName("shipping_label_currency")]
    public string? ShippingLabelCurrency { get; set; }
    /// <summary>
    /// Gets or sets the Revenue Eligibility.
    /// </summary>

    [JsonPropertyName("revenue_eligibility")]
    public string? RevenueEligibility { get; set; }
    /// <summary>
    /// Gets or sets the Ship From Country.
    /// </summary>

    [JsonPropertyName("ship_from_country")]
    public string? ShipFromCountry { get; set; }
    /// <summary>
    /// Gets or sets the Ship To Country.
    /// </summary>

    [JsonPropertyName("ship_to_country")]
    public string? ShipToCountry { get; set; }
    /// <summary>
    /// Gets or sets the Incoterm.
    /// </summary>

    [JsonPropertyName("incoterm")]
    public string? Incoterm { get; set; }
    /// <summary>
    /// Gets or sets the Customs Data.
    /// </summary>

    [JsonPropertyName("customs_data")]
    public IReadOnlyCollection<ReceiptShipmentCustomsData>? CustomsData { get; set; }
    /// <summary>
    /// Gets or sets the Duty Amount.
    /// </summary>

    [JsonPropertyName("duty_amount")]
    public float? DutyAmount { get; set; }
    /// <summary>
    /// Gets or sets the Duty Currency.
    /// </summary>

    [JsonPropertyName("duty_currency")]
    public string? DutyCurrency { get; set; }
    /// <summary>
    /// Gets or sets the Ship Date.
    /// </summary>

    [JsonPropertyName("ship_date")]
    public string? ShipDate { get; set; }
}
