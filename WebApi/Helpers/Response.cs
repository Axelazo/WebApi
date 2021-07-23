using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Helpers
{
    public class Response
    {
        public string status { get; set; }
        public string message { get; set; }
        public Object data { get; set; }
    }
}
