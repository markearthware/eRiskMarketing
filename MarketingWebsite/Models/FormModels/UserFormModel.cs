using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MarketingWebsite.Models.ViewModels;

namespace MarketingWebsite.Models.FormModels
{
    public class UserFormModel : UserViewModel
    {
        public string OldPassword { get; set; }

        public string NewPassword { get; set; }
    }
}