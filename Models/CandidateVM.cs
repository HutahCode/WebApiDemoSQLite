using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiDemoLite.Models
{
    public class CandidateVM: Candidate
    {
        public List<Favourite> favouriteCollection { get; set; }

    }


}