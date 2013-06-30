using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MarketingWebsite.Enums;

namespace MarketingWebsite.Models.ViewModels
{
    public class UserViewModel
    {
        public Guid Id { get; set; }

        public string EmailAddress { get; set; }

        public string FirstName { get; set; }

        public string Surname { get; set; }

        public string JobTitle { get; set; }

        public bool IsLoggedInUser { get; set; }

        public string AssignedDatabase { get; set; }

        public bool IsAdminUser { get; set; }

        public MembershipRoles MembershipRole { get; set; }

        public int CompanyId { get; set; }

        public string CompanyName { get; set; }
    }
}