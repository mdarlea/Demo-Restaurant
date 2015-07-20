using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.Unity;
using Unity.Mvc5;

namespace Restaurant.Host.Ioc.Unity
{
    public class UnityDependencyResolverFactory : IDependencyResolverFactory
    {
        private readonly IUnityContainer _container;

        public UnityDependencyResolverFactory(IUnityContainer container)
        {
            _container = container;
        }

        public System.Web.Http.Dependencies.IDependencyResolver CreateApiResolver()
        {
            return new UnityApiResolver(_container);
        }

        public System.Web.Mvc.IDependencyResolver CreateMvcResolver()
        {
            var resolver = new UnityDependencyResolver(_container);
            _container.RegisterInstance<System.Web.Mvc.IDependencyResolver>(resolver);
            return resolver;
        }
    }
}