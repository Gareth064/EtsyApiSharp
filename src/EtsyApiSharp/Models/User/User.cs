using System.Text.Json.Serialization;

namespace EtsyApiSharp.Models;

/// <summary>
/// Represents a single user of the site
/// </summary>
public class User
{
    /// <summary>
    /// The numeric ID of a user. This number is also a valid shop ID for the user\'s shop.
    /// </summary>
    [JsonPropertyName("user_id")]
    public long UserId { get; set; }

    /// <summary>
    /// The user\'s login name string.
    /// </summary>
    [JsonPropertyName("login_name")]
    public string LoginName { get; set; }

    /// <summary>
    /// An email address string for the user\'s primary email address.
    /// </summary>
    [JsonPropertyName("primary_email")]
    public string PrimaryEmail { get; set; }

    /// <summary>
    /// The user\'s first name.
    /// </summary>
    [JsonPropertyName("first_name")]
    public string FirstName { get; set; }

    /// <summary>
    /// The user\'s last name.
    /// </summary>
    [JsonPropertyName("last_name")]
    public string LastName { get; set; }

    /// <summary>
    /// The date and time the user created their account, in epoch seconds.
    /// </summary>
    [JsonPropertyName("create_timestamp")]
    public int CreateTimestamp { get; set; }

    /// <summary>
    /// The numeric ID of the user who referred this user.
    /// </summary>
    [JsonPropertyName("referred_by_user_id")]
    public long? ReferredByUserId { get; set; }

    /// <summary>
    /// Deprecated. Always true.
    /// </summary>
    [JsonPropertyName("use_new_inventory_endpoints")]
    public bool UseNewInventoryEndpoints { get; set; }

    /// <summary>
    /// True if the user is seller.
    /// </summary>
    [JsonPropertyName("is_seller")]
    public bool IsSeller { get; set; }

    /// <summary>
    /// The user\'s biography.
    /// </summary>
    [JsonPropertyName("bio")]
    public string Bio { get; set; }

    /// <summary>
    /// The user\'s gender.
    /// </summary>
    [JsonPropertyName("gender")]
    public string Gender { get; set; }

    /// <summary>
    /// The user\'s month of birth.
    /// </summary>
    [JsonPropertyName("birth_month")]
    public string BirthMonth { get; set; }

    /// <summary>
    /// The user\'s day of birth.
    /// </summary>
    [JsonPropertyName("birth_day")]
    public string BirthDay { get; set; }

    /// <summary>
    /// The number of transactions where the user has bought.
    /// </summary>
    [JsonPropertyName("transaction_buy_count")]
    public int? TransactionBuyCount { get; set; }

    /// <summary>
    /// The number of transactions where the user has sold.
    /// </summary>
    [JsonPropertyName("transaction_sold_count")]
    public int? TransactionSoldCount { get; set; }

}
