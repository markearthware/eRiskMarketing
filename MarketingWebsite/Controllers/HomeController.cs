using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MarketingWebsite.FormModels;
using System.Net.Mail;
using System.Net;

namespace MarketingWebsite.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PrivacyPolicy()
        {
            return View();
        }

        public ActionResult TermsAndConditions()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ContactUs()
        {
            if (this.TempData["SuccessMessage"] != null)
            {
                ViewBag.SuccessMessage = TempData["SuccessMessage"].ToString();
            }

            var formModel = new ContactUsFormModel();

            return View(formModel);
        }

        [HttpPost]
        public ActionResult ContactUs(ContactUsFormModel formModel)
        {
            this.SendEmail(formModel);

            TempData["SuccessMessage"] = "Thank you, your enquiry will be dealt with shortly";

            return this.RedirectToAction("ContactUs");
        }

        private void SendEmail(ContactUsFormModel formModel)
        {
            MailAddress to = new MailAddress("info@eriskapp.co.uk");

            MailAddress from = new MailAddress("info@eriskapp.co.uk");

            MailMessage mail = new MailMessage(from, to);

            mail.Subject = "Enquiry from customer - eRisk.co.uk website";

            mail.Body = "Email Address: " + formModel.EmailAddress + " Name: " + formModel.FullName + " Company: " + formModel.Company + " Message: " + formModel.Message;

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.postmarkapp.com";
            smtp.Port = 25;

            var usernameAndPassword = "cbe37b71-f585-4509-9648-8a58147bf04f";
            smtp.Credentials = new NetworkCredential(
                usernameAndPassword, usernameAndPassword);

            smtp.Send(mail);
        }
    }
}
