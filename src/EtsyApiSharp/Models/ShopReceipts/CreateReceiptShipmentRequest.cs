using System.Text.Json.Serialization;

namespace EtsyApiSharp.Models;

public class CreateReceiptShipmentRequest
{
    [JsonPropertyName("tracking_code")]
    public string? TrackingCode { get; set; }

    [JsonPropertyName("carrier_name")]
    public string? CarrierName { get; set; }

    [JsonPropertyName("send_bcc")]
    public bool? SendBcc { get; set; }

    [JsonPropertyName("note_to_buyer")]
    public string? NoteToBuyer { get; set; }

    [JsonPropertyName("mail_class")]
    public string? MailClass { get; set; }

    [JsonPropertyName("weight")]
    public float? Weight { get; set; }

    [JsonPropertyName("weight_units")]
    public string? WeightUnits { get; set; }

    [JsonPropertyName("length")]
    public float? Length { get; set; }

    [JsonPropertyName("width")]
    public float? Width { get; set; }

    [JsonPropertyName("height")]
    public float? Height { get; set; }

    [JsonPropertyName("dimension_units")]
    public string? DimensionUnits { get; set; }

    [JsonPropertyName("shipping_label_cost")]
    public float? ShippingLabelCost { get; set; }

    [JsonPropertyName("shipping_label_currency")]
    public string? ShippingLabelCurrency { get; set; }

    [JsonPropertyName("revenue_eligibility")]
    public string? RevenueEligibility { get; set; }

    [JsonPropertyName("ship_from_country")]
    public string? ShipFromCountry { get; set; }

    [JsonPropertyName("ship_to_country")]
    public string? ShipToCountry { get; set; }

    [JsonPropertyName("incoterm")]
    public string? Incoterm { get; set; }

    [JsonPropertyName("customs_data")]
    public IReadOnlyCollection<ReceiptShipmentCustomsData>? CustomsData { get; set; }

    [JsonPropertyName("duty_amount")]
    public float? DutyAmount { get; set; }

    [JsonPropertyName("duty_currency")]
    public string? DutyCurrency { get; set; }

    [JsonPropertyName("ship_date")]
    public string? ShipDate { get; set; }
}
