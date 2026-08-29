using System.Text.Json.Serialization;

namespace EtsyApiSharp.Models;
/// <summary>
/// Represents Error Response.
/// </summary>

public class ErrorResponse
{
    /// <summary>
    /// Gets or sets the Error.
    /// </summary>
    [JsonPropertyName("error")]
    public string Error { get; set; }
    /// <summary>
    /// Gets or sets the Error Description.
    /// </summary>

    [JsonPropertyName("error_description")]
    public string? ErrorDescription { get; set; }
}
