using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApiDemoLite.Models
{
    public class Candidate
    {
        public Candidate()
        {
            id = 0;
            name = string.Empty;
            dob = DateTime.MinValue;
            gender = string.Empty;            
        }
        
        public int id { get; set; }
        public string name { get; set; }        
        public DateTime dob { get; set; }
        public string gender { get; set; }        
    }

}