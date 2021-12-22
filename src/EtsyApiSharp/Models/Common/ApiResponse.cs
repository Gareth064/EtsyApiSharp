using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtsyApiSharp.Models.Common
{
    public class ApiResponse<T>
    {
        public int ResponseCode { get; set; }
        public bool Success { get; set; }
        public T? Data { get; set; }
        public string? Message { get; set; }
    }

    public  class ApiListResponse<T>
    {
        public int ResponseCode { get; set; }
        public bool Success { get; set; }
        public List<T>? Data { get; set; }
        public string? Message { get; set; }
    }
}
