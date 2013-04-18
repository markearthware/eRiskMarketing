using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MarketingWebsite.FormModels
{
    public class ContactUsFormModel
    {
        [Required]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Required]
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }

        public string Telephone { get; set; }

        [Display(Name = "Enquiry")]
        [Required]
        public string Message { get; set; }

        public string Company { get; set; }
    }
}