using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MarketingWebsite.Models;
using System.Web.Security;
using MarketingWebsite.Models.FormModels;
using MarketingWebsite.Models.DatabaseContext;
using MarketingWebsite.CustomExceptions;
using eRiskAssessment.Services.Enums;

namespace eRiskAssessment.Services
{
    public class AccountService
    {
        public void CreateUser(RegisterFormModel formModel, MembershipRoles selectedRole)
        {
            var dal = new EriskDatabase();

            //get company
            var company = dal.Companies.FirstOrDefault(x => x.CompanyName == formModel.CompanyName);

            if (company == null)
            {
                var userId = Guid.NewGuid();
                
                // create membership user
                var membershipUser = Membership.CreateUser(userId.ToString(), formModel.Password, formModel.EmailAddress);

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
                
                dal.Users.SaveChanges(user);
            }
            else
            {
                throw new CompanyExistsException();
            }
        }
    }
}
