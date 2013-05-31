using Mvc.Mailer;

namespace MarketingWebsite.Mailers
{ 
    public class UserMailer : MailerBase, IUserMailer 	
	{
		public UserMailer()
		{
			MasterName="_Layout";
		}
		
		public virtual MvcMailMessage NewPassword(string newPassword, string emailAddress)
		{
			ViewBag.NewPassword = newPassword;
			return Populate(x =>
			{
				x.Subject = "New Password";
				x.ViewName = "NewPassword";
				x.To.Add(emailAddress);
			});
		}
 	}
}