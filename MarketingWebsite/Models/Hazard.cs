using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MarketingWebsite.Models
{
    public class Hazard
    {
        public int HazardId { get; set; }

        public string HazardName { get; set; }

        public virtual ICollection<How> Hows { get; set; }

        public virtual ICollection<Control> Controls { get; set; }
        
        public virtual Database Database { get; set; }
    }
}