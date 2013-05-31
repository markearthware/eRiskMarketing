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
using MarketingWebsite.Services.Interfaces;
using System.Web;
using System.Security.Principal;
using System.Text.RegularExpressions;

namespace MarketingWebsite.Services
{
    public class AccountService : IAccountService
    {
        public void CreateUser(RegisterFormModel formModel, MembershipRoles selectedRole)
        {
            var dal = new EriskDatabase();

            //get company
            var company = dal.Companies.FirstOrDefault(x => x.CompanyName == formModel.CompanyName);

            if (company == null)
            {
                var userId = Guid.NewGuid();
                
                MembershipCreateStatus status;
                // create membership user
                var membershipUser = Membership.CreateUser(userId.ToString(), formModel.Password, formModel.EmailAddress, "THING", "THING", true, out status);
               

                // add user to Company Admin Role
                if (!Roles.RoleExists(selectedRole.ToString())){
                    Roles.CreateRole(selectedRole.ToString());
                }

                // add user to role
                Roles.AddUserToRole(userId.ToString(), selectedRole.ToString());

                // create company
                var newCompany = new Company
                                        {
                                            CompanyName = formModel.CompanyName, 
                                            CompanyLogoUrl = string.Empty,
                                            Databases = null,
                                            Subscriptions = null,
                                            Users = null
                                        };
                var user = new User
                            {
                                FirstName = formModel.FirstName,
                                Surname = formModel.Surname,
                                Company = newCompany,
                                Database = null,
                                UserId = userId,
                                JobTitle = formModel.JobTitle,
                                Tasks = null
                            };

                dal.Companies.Add(newCompany);
                dal.Users.Add(user);
                
                dal.SaveChanges();
            }
            else
            {
                throw new CompanyExistsException();
            }
        }

        public void LogUserIn(LoginFormModel formModel)
        {
            var username = Membership.GetUserNameByEmail(formModel.EmailAddress);
            
            if (username != null)
            {
                var isAuthenticated = Membership.ValidateUser(username, formModel.Password);

                if (!isAuthenticated)
                {
                    throw new Exception();
                }
                else
                {
                    FormsAuthentication.SetAuthCookie(username, formModel.RememberMe);
                }
            }
            else
            {
                throw new Exception();
            }
        }

        public bool IsUserAuthenticated(string emailAddress, string password)
        {
            var username = Membership.GetUserNameByEmail(emailAddress);
            if (username != null)
            {
                return Membership.ValidateUser(username, password);
            }
            else
            {
                return false;
            }
        }

        public MembershipUser GetUserById(Guid Id)
        {
            var users = Membership.GetAllUsers();
            return users[Id.ToString()];
        }

        public bool ResetLoggedInUsersPassword(string oldPassword, string newPassword)
        {
            var user = Membership.GetUser();
            user.ChangePassword(oldPassword, newPassword);
            return true;
        }

        public string ResetUsersPassword(Guid userId)
        {
            var user = this.GetUserById(userId);
            var newPassword = user.ResetPassword();

            //generate easier to type password 
            var alteredPassword = Guid.NewGuid().ToString("N").Substring(0, 8); 

            //set altered password as current password
            user.ChangePassword(newPassword, alteredPassword);

            return alteredPassword;
        }

        public bool ChangeEmailAddress(Guid userId, string newEmailAddress)
        {
            var user = this.GetUserById(userId);

            var canResetEmailAddress = Membership.GetUserNameByEmail(newEmailAddress) == null;

            if (canResetEmailAddress)
            {
                user.Email = newEmailAddress;
                Membership.UpdateUser(user);
                return true;
            }
            else
            {
                return false;
            }
        }

        public void LogUserIn(RegisterFormModel formModel)
        {
            var username = Membership.GetUserNameByEmail(formModel.EmailAddress);

            if (username != null)
            {
                var isAuthenticated = Membership.ValidateUser(username, formModel.Password);

                if (!isAuthenticated)
                {
                    throw new Exception();
                }
                else
                {
                    FormsAuthentication.SetAuthCookie(username, false);
                }
            }
            else
            {
                throw new Exception();
            }
        }

        public IPrincipal LoggedInUser()
        {
            return HttpContext.Current.User;
        }

        public bool IsUserAdministrator()
        {
            var loggedInUser = this.LoggedInUser();

            return loggedInUser.IsInRole(MembershipRoles.CompanyAdministrator.ToString())
                || loggedInUser.IsInRole(MembershipRoles.Administrator.ToString());
        }
    }
}
