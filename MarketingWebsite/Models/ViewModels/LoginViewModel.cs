using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MarketingWebsite.Models.FormModels;

namespace MarketingWebsite.Models.ViewModels
{
    public class LoginViewModel
    {
        public LoginFormModel FormModel { get; set; }

        public LoginViewModel()
        {
            this.FormModel = new LoginFormModel();
        }
    }
}