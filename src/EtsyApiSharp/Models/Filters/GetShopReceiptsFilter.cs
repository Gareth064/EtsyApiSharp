namespace EtsyApiSharp.Models.Filters;

public class GetShopReceiptsFilter : EtsyFilterBase
{
    public long? MinCreated { get; set; }
    public long? MaxCreated { get; set; }
    public long? MinLastModified { get; set; }
    public long? MaxLastModified { get; set; }
    public bool? WasPaid { get; set; }
    public bool? WasShipped { get; set; }
    public bool? WasDelivered { get; set; }

}
