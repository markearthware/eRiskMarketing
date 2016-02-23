using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MarketingWebsite.Models.DatabaseContext;
using MarketingWebsite.Models.FormModels;
using MarketingWebsite.Services.Interfaces;

namespace MarketingWebsite.Controllers
{
    public class LoginController : Controller
    {
        private readonly IAccountService accountService;

        public LoginController(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(LoginFormModel formModel)
        {
            var tempDataErrorMessage = string.Empty;
            if (this.ModelState.IsValid)
            {
                try
                {
                    this.accountService.LogUserIn(formModel);
                }
                catch (Exception e)
                {
                    tempDataErrorMessage = "The username or password was incorrect. Please try again. " + e.Message;
                    TempData["ErrorMessage"] = tempDataErrorMessage;
                    return RedirectToAction("Index");
                }

                return RedirectToAction("Index", "Dashboard");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
    }
}
