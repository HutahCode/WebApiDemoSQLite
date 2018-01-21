using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiDemoLite.Models
{
    public class Response
    {
        public Response()
        {
            this.success =true;
            this.errorMessage = string.Empty;
        }

        public int id { get; set; } = 0;
        public bool success { get; set; }
        public string errorMessage { get; set; }
    }
}