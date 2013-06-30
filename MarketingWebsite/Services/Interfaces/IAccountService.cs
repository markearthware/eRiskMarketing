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
        void CreateUserFromRegisterForm(RegisterFormModel formModel, MembershipRoles selectedRole);

        void CreateMembershipUserFromAngularApp(string userId, string emailAddress, MembershipRoles selectedRole);

        void LogUserIn(LoginFormModel formModel);

        void LogUserIn(RegisterFormModel formModel);
        
        bool IsUserAuthenticated(string emailAddress, string password);
        
        IPrincipal LoggedInUser();

        MembershipUser GetUserById(Guid Id);

        MembershipRoles GetUserRole(Guid Id);

        void ChangeRole(Guid userId, MembershipRoles newRole);

        bool ResetLoggedInUsersPassword(string oldPassword, string newPassword);

        string ResetUsersPassword(Guid userId);

        bool ChangeEmailAddress(Guid UserId, string newEmailAddress);

        bool IsUserAdministrator();
    }
}
