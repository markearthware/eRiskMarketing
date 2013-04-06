using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MarketingWebsite.Models.DatabaseContext;
using MarketingWebsite.Models.FormModels;
using MarketingWebsite.Services;
using MarketingWebsite.Services.Interfaces;
using MarketingWebsite.Enums;
using MarketingWebsite.CustomExceptions;

namespace MarketingWebsite.Controllers
{
    public class RegisterController : Controller
    {
        private readonly IAccountService accountService;

        public RegisterController(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Index(RegisterFormModel formModel)
        {
            if (this.ModelState.IsValid)
            {
                try
                {
                    this.accountService.CreateUser(formModel, MembershipRoles.CompanyAdministrator);
                }
                catch (CompanyExistsException ex)
                {

                }
            }
            return RedirectToAction("Index");
        }
    }
}
