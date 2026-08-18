using System.Text.Json.Serialization;

namespace EtsyApiSharp.Models;
/// <summary>
/// Represents Etsy Token Response.
/// </summary>

public class EtsyTokenResponse
{
    /// <summary>
    /// Gets or sets the Access Token.
    /// </summary>
    [JsonPropertyName("access_token")]
    public string AccessToken { get; set; }
    /// <summary>
    /// Gets or sets the Token Type.
    /// </summary>

    [JsonPropertyName("token_type")]
    public string TokenType { get; set; }
    /// <summary>
    /// Gets or sets the Expires In.
    /// </summary>

    [JsonPropertyName("expires_in")]
    public long ExpiresIn { get; set; }
    /// <summary>
    /// Gets or sets the Refresh Token.
    /// </summary>

    [JsonPropertyName("refresh_token")]
    public string RefreshToken { get; set; }
    /// <summary>
    /// Gets or sets the Scope.
    /// </summary>

    [JsonPropertyName("scope")]
    public string Scope { get; set; }
}
