using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using log4net.Config;
using Swaksoft.Application.Seedwork.Logging;
using Swaksoft.Application.Seedwork.TypeMapping;
using Swaksoft.Application.Seedwork.Validation;
using Swaksoft.Infrastructure.Crosscutting.Logging;
using Swaksoft.Infrastructure.Crosscutting.TypeMapping;
using Swaksoft.Infrastructure.Crosscutting.Validation;

namespace Restaurant.Host
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            GlobalConfiguration.Configure(UnityConfig.RegisterComponents);

            //log4net configuration
            XmlConfigurator.Configure();

            RegisterSingletons();
        }

        private static void RegisterSingletons()
        {
            TypeAdapterLocator.SetCurrent(new AutoMapperTypeAdapterFactory());
            EntityValidatorLocator.SetCurrent(new DataAnnotationsEntityValidatorFactory());
            LoggerLocator.SetCurrent(new Log4NetLoggerFactory());
        }
    }
}
