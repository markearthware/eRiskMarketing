using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MarketingWebsite.Models
{
    public class How
    {
        public int HowId { get; set; }

        public string HowName { get; set; }

        public virtual Hazard Hazard { get; set; }
    }
}