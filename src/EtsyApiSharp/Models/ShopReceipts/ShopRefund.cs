using System.Text.Json.Serialization;

namespace EtsyApiSharp.Models;

/// <summary>
/// The refund record for a receipt.
/// </summary>
public class ShopRefund
{
    /// <summary>
    /// A number equal to the refund total.
    /// </summary>
    [JsonPropertyName("amount")]
    public Money Amount { get; set; }

    /// <summary>
    /// The date & time of the refund, in epoch seconds.
    /// </summary>
    [JsonPropertyName("created_timestamp")]
    public long CreatedTimestamp { get; set; }

    /// <summary>
    /// The reason string given for the refund.
    /// </summary>
    [JsonPropertyName("reason")]
    public string? Reason { get; set; }

    /// <summary>
    /// The note string created by the refund issuer.
    /// </summary>
    [JsonPropertyName("note_from_issuer")]
    public string? NoteFromIssuer { get; set; }

    /// <summary>
    /// The status indication string for the refund.
    /// </summary>
    [JsonPropertyName("status")]
    public string? Status { get; set; }

}
