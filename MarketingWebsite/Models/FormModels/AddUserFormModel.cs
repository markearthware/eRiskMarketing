using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MarketingWebsite.Enums;
using MarketingWebsite.Models.ViewModels;

namespace MarketingWebsite.Models.FormModels
{
    public class AddUserFormModel : UserViewModel
    {
        public MembershipRoles MembershipRole { get; set; }
    }
}