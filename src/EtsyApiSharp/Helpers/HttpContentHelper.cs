using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EtsyApiSharp.Helpers;

/// <summary>
/// Creates HTTP content for Etsy API requests.
/// </summary>
public static class HttpContentHelper
{
    /// <summary>
    /// Serializes an object as UTF-8 JSON content.
    /// </summary>
    public static StringContent CreateJsonContent<T>(T data, bool ignoreNullValues = false)
    {
        var options = new JsonSerializerOptions
        {
            DefaultIgnoreCondition = ignoreNullValues
                ? JsonIgnoreCondition.WhenWritingNull
                : JsonIgnoreCondition.Never
        };
        var json = JsonSerializer.Serialize(data, options);
        return new StringContent(json, Encoding.UTF8, "application/json");
    }
}
