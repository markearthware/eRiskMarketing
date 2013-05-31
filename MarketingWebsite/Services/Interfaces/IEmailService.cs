using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MarketingWebsite.Services.Interfaces
{
    public interface IEmailService
    {
        //todo eamil password reset link instead
        void EmailNewPasswordToUser(string emailAddress, string newPassword);
    }
}