namespace EtsyApiSharp.Models.Common
{
    public class ApiResponse<T>
    {
        public int ResponseCode { get; set; }
        public bool Success { get; set; }
        public T? Data { get; set; }
        public string? Message { get; set; }
    }
}
