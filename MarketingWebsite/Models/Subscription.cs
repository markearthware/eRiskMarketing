using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MarketingWebsite.Models
{
    public class Subscription
    {
        public int SubscriptionId { get; set; }

        public int SubscriptionTypeId { get; set; }

        public DateTime DateAdded { get; set; }

        public DateTime LastRenewalDate { get; set; }

        public DateTime ExpiryDate { get; set; }

        public bool IsActive { get; set; }

        public virtual Company Company { get; set; }
    }
}