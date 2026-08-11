namespace EtsyApiSharp.Models;

public class UpdateListingPropertyRequest
{
    public IReadOnlyCollection<long> ValueIds { get; set; } = Array.Empty<long>();
    public IReadOnlyCollection<string> Values { get; set; } = Array.Empty<string>();
    public long? ScaleId { get; set; }
}
