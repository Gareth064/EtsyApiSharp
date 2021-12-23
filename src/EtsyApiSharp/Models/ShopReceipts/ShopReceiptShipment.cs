using Newtonsoft.Json;

namespace EtsyApiSharp.Models
{
    //The record of one shipment event for a ShopReceipt. A receipt may have many ShopReceiptShipment records.
    public class ShopReceiptShipment
    {
        [JsonProperty("receipt_shipping_id")]
        public long ReceiptShippingId { get; set; }


        [JsonProperty("shipment_notification_timestamp")]
        public int ShipmentNotificationTimestamp { get; set; }


        [JsonProperty("carrier_name")]
        public string CarrierName { get; set; }


        [JsonProperty("tracking_code")]
        public string TrackingCode { get; set; }


    }
}
