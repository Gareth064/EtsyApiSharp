using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtsyApiSharp.Models.Common
{
    public  class ApiResponse
    {
        public int ResponseCode { get; set; }
        public bool Success { get; set; }
        public object? Data { get; set; }
        public string? Message { get; set; }
    }
}
