using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MarketingWebsite.Models.FormModels;

namespace MarketingWebsite.Models.ViewModels
{
    public class RegisterViewModel
    {
        public RegisterFormModel FormModel { get; set; }

        public RegisterViewModel()
        {
            this.FormModel = new RegisterFormModel();
        }
    }
}