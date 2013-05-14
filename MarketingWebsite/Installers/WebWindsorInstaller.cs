using System.Web.Http.Controllers;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using MarketingWebsite.Services.Interfaces;
using MarketingWebsite.Services;
using MarketingWebsite.Api;

namespace WebApiDependencyResolverSample.Windsor
{
    internal class WebWindsorInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component
                .For<IAccountService>()
                .ImplementedBy<AccountService>()
                .LifestylePerWebRequest());

            container.Register(Classes
            .FromAssemblyContaining<UsersController>()
            .BasedOn<IHttpController>()
            .LifestylePerWebRequest());

        }
    }
}