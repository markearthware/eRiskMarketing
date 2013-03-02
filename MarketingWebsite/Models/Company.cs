using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MarketingWebsite.Models
{
    public class Company
    {
        public int CompanyId { get; set; }

        public string CompanyName { get; set; }

        public string CompanyLogoUrl { get; set; }

        public virtual ICollection<User> Users {get; set;}

        public virtual ICollection<Database> Databases { get; set; }

        public virtual ICollection<Subscription> Subscriptions { get; set; }
    }
}