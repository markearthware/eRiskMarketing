using Mvc.Mailer;

namespace MarketingWebsite.Mailers
{ 
    public interface IUserMailer
    {
        MvcMailMessage NewPassword(string newPassword, string emailAddress);
	}
}