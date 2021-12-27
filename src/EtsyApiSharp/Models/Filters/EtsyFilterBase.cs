namespace EtsyApiSharp.Models.Filters
{
    public abstract class EtsyFilterBase
    {
        public int Limit { get; set; } = 25;
        public int Offset { get; set; }
    }
}
