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

    public class UsersController : ApiController
    {
        [HttpGet]
        public IEnumerable<UserViewModel> Get()
        {
            using (var ctx = new EriskDatabase())
            {
                return ctx.Users.Take(30).Select(x => new UserViewModel
                                                                { 
                                                                    FirstName = x.FirstName, 
                                                                    Surname = x.Surname 
                                                                }
                                                      ).ToList();

            }
        }
    }
}
