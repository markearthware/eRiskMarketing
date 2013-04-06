using System.Web.Mvc;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using MarketingWebsite.Services.Interfaces;
using MarketingWebsite.Services;

public class ControllersInstaller : IWindsorInstaller
{
    public void Install(IWindsorContainer container, IConfigurationStore store)
    {
        container.Register(Classes.FromThisAssembly()
                            .BasedOn<IController>()
                            .LifestyleTransient());
        container.Register(Component.For<IAccountService>().ImplementedBy<AccountService>().LifeStyle.Transient);
    }
}