namespace EtsyApiSharp.Models.Common;

public class ApiResponse<T>
{
    public int ResponseCode { get; set; }
    public bool Success { get; set; }
    public T? Data { get; set; }
    public string? Message { get; set; }
    public ResponseHeaders ResponseHeaders { get; set; } = new ResponseHeaders();
    public string RequestedResource { get; set; }
}

public class ResponseHeaders
{
    public int LimitPerSecond { get; set; }
    public int RemainingThisSecond { get; set; }
    public int LimitPerDay { get; set; }
    public int RemainingToday { get; set; }
    public string EtsyRequestUuid { get; set; }
}
