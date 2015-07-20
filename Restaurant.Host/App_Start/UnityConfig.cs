using System;
using System.Web.Http;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Restaurant.Host.Ioc;
using Restaurant.Host.Ioc.Unity;

namespace Restaurant.Host
{
    public static class UnityConfig
    {
        public static void RegisterComponents(HttpConfiguration config)
        {
			var container = new UnityContainer();
            var bootstrap = new UnityBootstrap(container);

            var factory = new UnityDependencyResolverFactory(container);

            ConfigureDependencyResolvers(config, factory);
        }

        private static void ConfigureDependencyResolvers(HttpConfiguration config, IDependencyResolverFactory factory)
        {
            if (config == null) throw new ArgumentNullException("config");
            if (factory == null) throw new ArgumentNullException("factory");

            //Web API resolver
            config.DependencyResolver = factory.CreateApiResolver();

            //MVC resolver
            DependencyResolver.SetResolver(factory.CreateMvcResolver());
        }
    }
}