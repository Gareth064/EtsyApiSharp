using EtsyApiSharp.Models;
using EtsyApiSharp.Models.Common;
using System.Text.Json;

namespace EtsyApiSharp.Helpers;

internal static class EtsyResponseParser
{
    public static async Task<ApiResponse<EtsyListResponse<T>>> ParseResponseOfList<T>(HttpResponseMessage response)
    {
        var result = new ApiResponse<EtsyListResponse<T>>();
        result.RequestedResource = response.RequestMessage.RequestUri.ToString();
        result.ResponseHeaders = BuildHeadersFromResponse(response);
        string? bodyContent;

        if (response.IsSuccessStatusCode)
        {
            bodyContent = await response.Content.ReadAsStringAsync();
            var data = JsonSerializer.Deserialize<EtsyListResponse<T>>(bodyContent);

            result.ResponseCode = 200;
            result.Success = true;
            result.Data = data;
            result.Message = null;

            return result;
        }

        bodyContent = await response.Content.ReadAsStringAsync();
        var error = JsonSerializer.Deserialize<ErrorResponse>(bodyContent);

        result.ResponseCode = (int)response.StatusCode;
        result.Success = false;
        result.Data = null;
        result.Message = $"{error.Error}: {error.ErrorDescription}";

        return result;
    }

    public static async Task<ApiResponse<T>> ParseResponseOfSingle<T>(HttpResponseMessage response)
    {
        var result = new ApiResponse<T>();

        result.ResponseHeaders = BuildHeadersFromResponse(response);

        string? bodyContent;

        if (response.IsSuccessStatusCode)
        {
            bodyContent = await response.Content.ReadAsStringAsync();
            var data = JsonSerializer.Deserialize<T>(bodyContent);

            result.ResponseCode = 200;
            result.Success = true;
            result.Data = data;
            result.Message = null;

            return result;
        }

        bodyContent = await response.Content.ReadAsStringAsync();
        var error = JsonSerializer.Deserialize<ErrorResponse>(bodyContent);

        result.ResponseCode = (int)response.StatusCode;
        result.Success = false;
        result.Data = default;
        result.Message = $"{error.Error}: {error.ErrorDescription}";

        return result;
    }

    private static ResponseHeaders BuildHeadersFromResponse(HttpResponseMessage response)
    {
        var headers = new ResponseHeaders();

        if (response.Headers.Contains("X-Limit-Per-Second"))
            headers.LimitPerSecond = Int32.Parse(response.Headers.GetValues("X-Limit-Per-Second").FirstOrDefault());

        if (response.Headers.Contains("X-Remaining-This-Second"))
            headers.RemainingThisSecond = Int32.Parse(response.Headers.GetValues("X-Remaining-This-Second").FirstOrDefault());

        if (response.Headers.Contains("X-Limit-Per-Day"))
            headers.LimitPerDay = Int32.Parse(response.Headers.GetValues("X-Limit-Per-Day").FirstOrDefault());

        if (response.Headers.Contains("X-Remaining-Today"))
            headers.RemainingToday = Int32.Parse(response.Headers.GetValues("X-Remaining-Today").FirstOrDefault());

        if (response.Headers.Contains("X-Etsy-Request-Uuid"))
            headers.EtsyRequestUuid = response.Headers.GetValues("X-Etsy-Request-Uuid").FirstOrDefault();

        return headers;
    }
}
