using System.Text.Json.Serialization;

namespace EtsyApiSharp.Models
{
    //The record of one shipment event for a ShopReceipt. A receipt may have many ShopReceiptShipment records.
    public class ShopReceiptShipment
    {
        [JsonPropertyName("receipt_shipping_id")]
        public long ReceiptShippingId { get; set; }


        [JsonPropertyName("shipment_notification_timestamp")]
        public int ShipmentNotificationTimestamp { get; set; }


        [JsonPropertyName("carrier_name")]
        public string CarrierName { get; set; }


        [JsonPropertyName("tracking_code")]
        public string TrackingCode { get; set; }


    }
}
