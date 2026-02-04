using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace EtsyApiSharp.Helpers;

/// <summary>
/// Helper class for creating HTTP content with proper headers for Etsy API requests
/// </summary>
public static class HttpContentHelper
{
    /// <summary>
    /// Creates StringContent with JSON data and proper Content-Type header including UTF-8 charset
    /// as required by Etsy API v3 documentation
    /// </summary>
    /// <typeparam name="T">Type of the object to serialize</typeparam>
    /// <param name="data">The object to serialize to JSON</param>
    /// <returns>StringContent with application/json; charset=utf-8 content type</returns>
    public static StringContent CreateJsonContent<T>(T data)
    {
        var json = JsonSerializer.Serialize(data);
        var content = new StringContent(json, Encoding.UTF8);
        content.Headers.ContentType = new MediaTypeHeaderValue("application/json")
        {
            CharSet = "utf-8"
        };
        return content;
    }
}
