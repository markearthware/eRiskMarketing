using Mvc.Mailer;

namespace MarketingWebsite.Mailers
{ 
    public interface IUserMailer
    {
			MvcMailMessage Welcome();
			MvcMailMessage GoodBye();
	}
}