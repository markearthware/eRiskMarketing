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

        [HttpPost]
        public ActionResult Index(RegisterFormModel formModel)
        {
            var tempDataErrorMessage = string.Empty;
            if (this.ModelState.IsValid)
            {
                try
                {
                    this.accountService.CreateUserFromRegisterForm(formModel, MembershipRoles.CompanyAdministrator);
                    this.accountService.LogUserIn(formModel);
                    return RedirectToAction("Index", "Dashboard");
                }
                catch (CompanyExistsException ex)
                {
                    tempDataErrorMessage = formModel.CompanyName + " has already been added into the system, contact your health and safety manager for login details";
                }
                catch (Exception ex)
                {
                    tempDataErrorMessage = "An error has occured, please try again";
                }
            }

            TempData["ErrorMessage"] = tempDataErrorMessage;

            return RedirectToAction("Index");
        }
    }
}
