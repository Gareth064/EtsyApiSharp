namespace EtsyApiSharp.Models;
/// <summary>
/// Represents Update Listing Property Request.
/// </summary>

public class UpdateListingPropertyRequest
{
    /// <summary>
    /// Executes the Empty operation.
    /// </summary>
    public IReadOnlyCollection<long> ValueIds { get; set; } = Array.Empty<long>();
    /// <summary>
    /// Executes the Empty operation.
    /// </summary>
    public IReadOnlyCollection<string> Values { get; set; } = Array.Empty<string>();
    /// <summary>
    /// Gets or sets the Scale Id.
    /// </summary>
    public long? ScaleId { get; set; }
}
