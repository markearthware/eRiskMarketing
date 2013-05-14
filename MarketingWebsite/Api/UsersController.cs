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
    using System.Web.Mvc;
    using MarketingWebsite.Models.ViewModels;
    using MarketingWebsite.Services.Interfaces;

    public class UsersController : ApiController
    {
        private readonly IAccountService accountService;

        public UsersController(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        [HttpGet]
        public IEnumerable<UserViewModel> Get()
        {
            using (var ctx = new EriskDatabase())
            {
                return ctx.Users.Select(x => new UserViewModel
                                            { 
                                                Id = x.UserId,
                                                FirstName = x.FirstName, 
                                                Surname = x.Surname 
                                            }
                                        ).ToList();

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
                                                                                    IsLoggedInUser = isLoggedInUser(x)
                                                                                }
                                                                            ).FirstOrDefault();

            }
        }

        private bool isLoggedInUser(User user)
        {
            return user.UserId.ToString() == accountService.LoggedInUser().Identity.Name;
        }
    }
}
