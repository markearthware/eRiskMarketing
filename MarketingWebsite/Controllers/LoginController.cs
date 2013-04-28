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
            if (this.ModelState.IsValid)
            {
                try
                {
                    this.accountService.LogUserIn(formModel);
                }
                catch (Exception)
                {
                    return RedirectToAction("Index");
                }

                // todo if not in administrator role go to "UserDashboard"
                return RedirectToAction("Index", "Dashboard");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
    }
}
