using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtsyApiSharp.Models.Requests
{
    public class GetShopReceiptsRequest
    {
        public long? MinCreated { get; set; }
        public long? MaxCreated {  get; set; }
        public long? MinLastModified { get; set; }
        public long? MaxLastModified { get; set; }
        public int Limit { get; set; } = 25;
        public int Offset { get; set; } = 0;
        public bool? WasPaid { get; set; }
        public bool? WasShipped { get; set; }

    }
}
