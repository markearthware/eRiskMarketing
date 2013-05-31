using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MarketingWebsite.Api
{
    using MarketingWebsite.Models;
    using MarketingWebsite.Models.DatabaseContext;
    using System.Web.Http;
    using MarketingWebsite.Models.ViewModels;
    using MarketingWebsite.Services.Interfaces;
    using MarketingWebsite.Models.FormModels;
    using MarketingWebsite.Services;
    using MarketingWebsite.Mailers;
    using MarketingWebsite.Enums;

    public class UsersController : ApiController
    {
        private readonly IAccountService accountService;

        private readonly IUserMailer emailService;

        public UsersController(IAccountService accountService)
        {
            this.accountService = accountService;
            this.emailService = new UserMailer();
        }

        [HttpGet]
        public IEnumerable<UserViewModel> Get()
        {
            using (var ctx = new EriskDatabase())
            {
                return ctx.Users.ToList().Select(x => new UserViewModel
                                            { 
                                                Id = x.UserId,
                                                FirstName = x.FirstName, 
                                                Surname = x.Surname,
                                                EmailAddress = accountService.GetUserById(x.UserId).Email,
                                            }
                                        );

            }
        }

        [HttpGet]
        public UserViewModel GetLoggedInUser()
        {
            using (var ctx = new EriskDatabase())
            {
                var membershipUser = accountService.LoggedInUser();

                var userId =  new Guid(membershipUser.Identity.Name);

                return ctx.Users.Where(x => x.UserId == userId).ToList().Select(x => new UserViewModel
                {
                    Id = x.UserId,
                    FirstName = x.FirstName,
                    Surname = x.Surname,
                    JobTitle = x.JobTitle,
                    IsLoggedInUser = isLoggedInUser(x),
                    EmailAddress = accountService.GetUserById(x.UserId).Email,
                    IsAdminUser = this.accountService.IsUserAdministrator()
                }).FirstOrDefault();
            }
        }

        [HttpGet]
        public UserViewModel Get(Guid Id)
        {
            using (var ctx = new EriskDatabase())
            {
                return ctx.Users.Where(x => x.UserId == Id).ToList().Select(x => new UserViewModel
                                                                                {
                                                                                    Id = x.UserId,
                                                                                    FirstName = x.FirstName,
                                                                                    Surname = x.Surname,
                                                                                    JobTitle = x.JobTitle,
                                                                                    IsLoggedInUser = isLoggedInUser(x),
                                                                                    EmailAddress = accountService.GetUserById(x.UserId).Email
                                                                                }
                                                                            ).FirstOrDefault();

            }
        }

        [HttpPut]
        public HttpResponseMessage Update(UserFormModel user)
        {
            using (var ctx = new EriskDatabase())
            {
                var dbUser = ctx.Users.FirstOrDefault(x => x.UserId == user.Id);

                // domain changes
                dbUser.FirstName = user.FirstName;
                dbUser.Surname = user.Surname;
                dbUser.JobTitle = user.JobTitle;


                bool? resetPasswordSuccess = null;
                if (isLoggedInUser(dbUser) && (!string.IsNullOrEmpty(user.NewPassword) && !string.IsNullOrEmpty(user.OldPassword)))
                {
                    if (accountService.IsUserAuthenticated(user.EmailAddress, user.OldPassword))
                    {
                        // attempt to change password 
                        accountService.ResetLoggedInUsersPassword(user.OldPassword, user.NewPassword);
                        resetPasswordSuccess = true;
                    }
                    else
                    {
                        resetPasswordSuccess = false;
                    }
                }

                bool? emailChangeSuccess = null;
                if (user.EmailAddress.ToLower() != accountService.GetUserById(user.Id).Email.ToLower())
                {
                    // attempt to change email address
                    emailChangeSuccess = accountService.ChangeEmailAddress(user.Id, user.EmailAddress);
                }

                if ((resetPasswordSuccess == null || resetPasswordSuccess == true) && (emailChangeSuccess == null || emailChangeSuccess == true))
                {
                    // Both/one were success or no passowrd/email change attempted so commit all changes to db
                    ctx.SaveChanges();
                    return this.Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    var messages = new List<string>();
                    if (emailChangeSuccess == false)
                    {
                        messages.Add(string.Format("{0} is already being used, please try a different email address", user.EmailAddress));
                    }

                    if (resetPasswordSuccess == false)
                    {
                        messages.Add("Please ensure you type your old password correctly");
                    }

                    return this.Request.CreateResponse(HttpStatusCode.Forbidden, messages);
                }
            }
        }

        [HttpGet]
        public HttpResponseMessage ResetUsersPassword(Guid Id)
        {
            try
            {
                var newPassword = this.accountService.ResetUsersPassword(Id);
                var emailAddress = this.accountService.GetUserById(Id).Email;
                this.emailService.NewPassword(newPassword, emailAddress).Send();
                
                return this.Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return this.Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        private bool isLoggedInUser(User user)
        {
            return user.UserId.ToString() == accountService.LoggedInUser().Identity.Name;
        }
    }
}
