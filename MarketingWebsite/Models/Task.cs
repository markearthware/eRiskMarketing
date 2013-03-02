using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MarketingWebsite.Models
{
    public class Task
    {
        public int TaskId { get; set; }

        public DateTime DateFinished { get; set; }

        public DateTime DateStarted { get; set; }

        public string Name { get; set; }

        public string Site { get; set; }

        public virtual ICollection<Assessment> Assessments { get; set; }

        public virtual User User { get; set; }
    }
}