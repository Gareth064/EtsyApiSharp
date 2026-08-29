using System.Text.Json.Serialization;

namespace EtsyApiSharp.Models;

/// <summary>
/// The record of one shipment event for a ShopReceipt. A receipt may have many ShopReceiptShipment records.
/// </summary>
public class ShopReceiptShipment
{
    /// <summary>
    /// The unique numeric ID of a Shop Receipt Shipment record.
    /// </summary>
    [JsonPropertyName("receipt_shipping_id")]
    public long? ReceiptShippingId { get; set; }

    /// <summary>
    /// The time at which Etsy notified the buyer of the shipment event, in epoch seconds.
    /// </summary>
    [JsonPropertyName("shipment_notification_timestamp")]
    public long ShipmentNotificationTimestamp { get; set; }

    /// <summary>
    /// The name string for the carrier/company responsible for delivering the shipment.
    /// </summary>
    [JsonPropertyName("carrier_name")]
    public string CarrierName { get; set; }

    /// <summary>
    /// The tracking code string provided by the carrier/company for the shipment.
    /// </summary>
    [JsonPropertyName("tracking_code")]
    public string TrackingCode { get; set; }

}
