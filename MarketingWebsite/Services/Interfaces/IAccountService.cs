using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MarketingWebsite.Models;
using System.Web.Security;
using MarketingWebsite.Models.FormModels;
using MarketingWebsite.Models.DatabaseContext;
using MarketingWebsite.CustomExceptions;
using MarketingWebsite.Enums;
using System.Security.Principal;

namespace MarketingWebsite.Services.Interfaces
{
    public interface IAccountService
    {
        void CreateUser(RegisterFormModel formModel, MembershipRoles selectedRole);

        void LogUserIn(LoginFormModel formModel);

        void LogUserIn(RegisterFormModel formModel);
        
        IPrincipal LoggedInUser();
    }
}
