using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MarketingWebsite.Models
{
    public class Database
    {
        public int DatabaseId { get; set; }

        public string DatabaseName { get; set; }

        public int MaxGreen { get; set; }

        public int MaxAmber { get; set; }

        public int MaxRed { get; set; }

        public bool IsPublished { get; set; }

        public virtual ICollection<SeverityDropDownItem> SeverityList { get; set; }

        public virtual ICollection<LikelihoodDropDownItem> LikelihoodList { get; set; }

        public virtual ICollection<User> Users { get; set; }

        public virtual ICollection<Hazard> Hazards { get; set; }

        public virtual ICollection<Who> Whos { get; set; }
    }
}