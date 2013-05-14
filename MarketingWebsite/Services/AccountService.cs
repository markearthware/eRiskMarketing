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
    }
}
