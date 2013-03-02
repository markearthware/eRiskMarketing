using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MarketingWebsite.Models
{
    public class Who
    {
        public int WhoId { get; set; }

        public string WhoName { get; set; }

        public virtual Database Database { get; set; }
    }
}