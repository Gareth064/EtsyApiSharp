using System.Text.Json.Serialization;

namespace EtsyApiSharp.Models;
/// <summary>
/// Represents User.
/// </summary>

public class User
{
    /// <summary>
    /// The numeric ID of a user. This number is also a valid shop ID for the user's shop.
    /// </summary>
    [JsonPropertyName("user_id")]
    public long UserId { get; set; }

    /// <summary>
    /// An email address string for the user's primary email address. Etsy grants access to this
    /// field case-by-case for qualifying third-party integrations.
    /// </summary>
    [JsonPropertyName("primary_email")]
    public string? PrimaryEmail { get; set; }

    /// <summary>
    /// The user's first name.
    /// </summary>
    [JsonPropertyName("first_name")]
    public string? FirstName { get; set; }

    /// <summary>
    /// The user's last name.
    /// </summary>
    [JsonPropertyName("last_name")]
    public string? LastName { get; set; }

    /// <summary>
    /// The user's avatar URL.
    /// </summary>
    [JsonPropertyName("image_url_75x75")]
    public string? ImageUrl75x75 { get; set; }

}
