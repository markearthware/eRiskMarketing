using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace MarketingWebsite.Models.DatabaseContext
{
    public class EriskDatabase : DbContext
    {
        public EriskDatabase()
            : base("Name=eriskdatabase")
        {

        }

        public DbSet<Company> Companies { get; set; }

        public DbSet<Assessment> Assessments { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Task> Tasks { get; set; }

        public DbSet<Database> Databases { get; set; }

        public DbSet<Hazard> Hazards { get; set; }

        public DbSet<How> Hows { get; set; }

        public DbSet<Who> Whos { get; set; }

        public DbSet<Control> Controls { get; set; }

        public DbSet<Subscription> Subscriptions { get; set; }

        public DbSet<LikelihoodDropDownItem> LikelihoodDropDownItems { get; set; }

        public DbSet<SeverityDropDownItem> SeverityDropDownItems { get; set; }
    }
}