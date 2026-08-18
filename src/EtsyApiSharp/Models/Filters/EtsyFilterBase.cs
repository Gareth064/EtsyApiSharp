namespace EtsyApiSharp.Models.Filters;
/// <summary>
/// Represents Etsy Filter Base.
/// </summary>

public abstract class EtsyFilterBase
{
    /// <summary>
    /// Gets or sets the Limit.
    /// </summary>
    public int Limit { get; set; } = 25;
    /// <summary>
    /// Gets or sets the Offset.
    /// </summary>
    public int Offset { get; set; }
}
