using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MarketingWebsite.Models
{
    public class Control
    {
        public int ControlId { get; set; }

        public string ControlName { get; set; }

        public virtual Hazard Hazard { get; set; }
    }
}