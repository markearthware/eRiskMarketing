using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MarketingWebsite.Models
{
    public class User
    {
        public Guid UserId { get; set; }

        public string FirstName { get; set; }

        public string Surname { get; set; }

        public string JobTitle { get; set; }

        public bool IsDeleted { get; set; }

        public virtual ICollection<Task> Tasks { get; set; }

        public virtual Database Database { get; set; }

        public virtual Company Company { get; set; }
    }
}