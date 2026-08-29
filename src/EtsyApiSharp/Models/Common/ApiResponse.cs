namespace EtsyApiSharp.Models.Common;
/// <summary>
/// Represents Api Response.
/// </summary>

public class ApiResponse<T>
{
    /// <summary>
    /// Gets or sets the Response Code.
    /// </summary>
    public int ResponseCode { get; set; }
    /// <summary>
    /// Gets or sets the Success.
    /// </summary>
    public bool Success { get; set; }
    /// <summary>
    /// Gets or sets the Data.
    /// </summary>
    public T? Data { get; set; }
    /// <summary>
    /// Gets or sets the Message.
    /// </summary>
    public string? Message { get; set; }
    /// <summary>
    /// Executes the Response Headers operation.
    /// </summary>
    public ResponseHeaders ResponseHeaders { get; set; } = new ResponseHeaders();
    /// <summary>
    /// Gets or sets the Requested Resource.
    /// </summary>
    public string RequestedResource { get; set; } = string.Empty;
}
/// <summary>
/// Represents Response Headers.
/// </summary>

public class ResponseHeaders
{
    /// <summary>
    /// Gets or sets the Limit Per Second.
    /// </summary>
    public int LimitPerSecond { get; set; }
    /// <summary>
    /// Gets or sets the Remaining This Second.
    /// </summary>
    public int RemainingThisSecond { get; set; }
    /// <summary>
    /// Gets or sets the Limit Per Day.
    /// </summary>
    public int LimitPerDay { get; set; }
    /// <summary>
    /// Gets or sets the Remaining Today.
    /// </summary>
    public int RemainingToday { get; set; }
    /// <summary>
    /// Gets or sets the Etsy Request Uuid.
    /// </summary>
    public string? EtsyRequestUuid { get; set; }
}
