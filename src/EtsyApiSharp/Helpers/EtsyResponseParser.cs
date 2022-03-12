using EtsyApiSharp.Models.Common;
using System.Text.Json;

namespace EtsyApiSharp.Helpers
{
    internal static class EtsyResponseParser
    {
        public static async Task<ApiResponse<EtsyListResponse<T>>> ParseResponseOfList<T>(HttpResponseMessage response)
        {
            ApiResponse<EtsyListResponse<T>> result;

            if (response.IsSuccessStatusCode)
            {
                var bodyContent = await response.Content.ReadAsStringAsync();
                var data = JsonSerializer.Deserialize<EtsyListResponse<T>>(bodyContent);

                result = new ApiResponse<EtsyListResponse<T>>
                {
                    ResponseCode = 200,
                    Success = true,
                    Data = data,
                    Message = null
                };

                return result;
            }

            result = new ApiResponse<EtsyListResponse<T>>
            {
                ResponseCode = (int)response.StatusCode,
                Success = false,
                Data = null,
                Message = response.ReasonPhrase
            };

            return result;
        }

        public static async Task<ApiResponse<T>> ParseResponseOfSingle<T>(HttpResponseMessage response)
        {
            ApiResponse<T> result;

            if (response.IsSuccessStatusCode)
            {
                var bodyContent = await response.Content.ReadAsStringAsync();
                var data = JsonSerializer.Deserialize<T>(bodyContent);

                result = new ApiResponse<T>
                {
                    ResponseCode = 200,
                    Success = true,
                    Data = data,
                    Message = null
                };

                return result;
            }

            result = new ApiResponse<T>
            {
                ResponseCode = (int)response.StatusCode,
                Success = false,
                Data = default(T),
                Message = response.ReasonPhrase
            };

            return result;
        }
    }
}
