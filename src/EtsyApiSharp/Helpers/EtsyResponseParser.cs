using EtsyApiSharp.Models;
using EtsyApiSharp.Models.Common;
using System.Text.Json;

namespace EtsyApiSharp.Helpers;

internal static class EtsyResponseParser
{
    /// <summary>
    /// Executes the Parse Response Of List operation.
    /// </summary>
    public static async Task<ApiResponse<EtsyListResponse<T>>> ParseResponseOfList<T>(
        HttpResponseMessage response,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(response);

        var result = CreateResponse<EtsyListResponse<T>>(response);
        var bodyContent = await ReadContentAsync(response, cancellationToken).ConfigureAwait(false);

        if (response.IsSuccessStatusCode)
        {
            result.Success = true;
            result.Data = DeserializeResponse<EtsyListResponse<T>>(bodyContent);
            return result;
        }

        result.Message = ParseErrorMessage(response, bodyContent);
        return result;
    }
    /// <summary>
    /// Executes the Parse Response Of Single operation.
    /// </summary>

    public static async Task<ApiResponse<T>> ParseResponseOfSingle<T>(
        HttpResponseMessage response,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(response);

        var result = CreateResponse<T>(response);
        var bodyContent = await ReadContentAsync(response, cancellationToken).ConfigureAwait(false);

        if (response.IsSuccessStatusCode)
        {
            result.Success = true;
            result.Data = DeserializeResponse<T>(bodyContent);
            return result;
        }

        result.Message = ParseErrorMessage(response, bodyContent);
        return result;
    }

    private static ApiResponse<T> CreateResponse<T>(HttpResponseMessage response) => new()
    {
        ResponseCode = (int)response.StatusCode,
        Success = false,
        Data = default,
        Message = null,
        RequestedResource = response.RequestMessage?.RequestUri?.ToString() ?? string.Empty,
        ResponseHeaders = BuildHeadersFromResponse(response)
    };

    private static async Task<string> ReadContentAsync(
        HttpResponseMessage response,
        CancellationToken cancellationToken) => response.Content is null
        ? string.Empty
        : await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);

    private static T? DeserializeResponse<T>(string bodyContent)
    {
        if (string.IsNullOrWhiteSpace(bodyContent))
            return default;

        return JsonSerializer.Deserialize<T>(bodyContent);
    }

    private static string ParseErrorMessage(HttpResponseMessage response, string bodyContent)
    {
        try
        {
            var error = JsonSerializer.Deserialize<ErrorResponse>(bodyContent);
            if (!string.IsNullOrWhiteSpace(error?.Error))
            {
                return string.IsNullOrWhiteSpace(error.ErrorDescription)
                    ? error.Error
                    : $"{error.Error}: {error.ErrorDescription}";
            }
        }
        catch (JsonException)
        {
            // Preserve the HTTP response when an intermediary returns a non-JSON error body.
        }

        return response.ReasonPhrase ?? $"HTTP {(int)response.StatusCode}";
    }

    private static ResponseHeaders BuildHeadersFromResponse(HttpResponseMessage response)
    {
        var headers = new ResponseHeaders();

        if (TryGetInt32Header(response, "X-Limit-Per-Second", out var limitPerSecond))
            headers.LimitPerSecond = limitPerSecond;

        if (TryGetInt32Header(response, "X-Remaining-This-Second", out var remainingThisSecond))
            headers.RemainingThisSecond = remainingThisSecond;

        if (TryGetInt32Header(response, "X-Limit-Per-Day", out var limitPerDay))
            headers.LimitPerDay = limitPerDay;

        if (TryGetInt32Header(response, "X-Remaining-Today", out var remainingToday))
            headers.RemainingToday = remainingToday;

        if (response.Headers.TryGetValues("X-Etsy-Request-Uuid", out var requestUuids))
            headers.EtsyRequestUuid = requestUuids.FirstOrDefault();

        return headers;
    }

    private static bool TryGetInt32Header(
        HttpResponseMessage response,
        string headerName,
        out int value)
    {
        value = default;
        return response.Headers.TryGetValues(headerName, out var values) &&
            int.TryParse(values.FirstOrDefault(), out value);
    }
}
