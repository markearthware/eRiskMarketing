using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using MarketingWebsite.Models.DatabaseContext;
using System.Data.Entity;
using Castle.Windsor;
using Castle.Windsor.Installer;
using System.Web.Http.Dispatcher;
using WebApiDependencyResolverSample.Windsor;

namespace MarketingWebsite
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();
            Database.SetInitializer<EriskDatabase>(null);
            BootstrapMvcContainer();
            BootstrapWebApiContainer();
        }

        private static IWindsorContainer container;

        private static void BootstrapMvcContainer()
        {
            container = new WindsorContainer()
                .Install(FromAssembly.This());
            var controllerFactory = new WindsorControllerFactory(container.Kernel);
            ControllerBuilder.Current.SetControllerFactory(controllerFactory);

        }

        private static void BootstrapWebApiContainer()
        {
            var container = new WindsorContainer()
                .Install(new WebWindsorInstaller());

            GlobalConfiguration.Configuration.DependencyResolver =
                new WindsorDependencyResolver(container);
        }

        protected void Application_End()
        {
            container.Dispose();
        }
    }
}