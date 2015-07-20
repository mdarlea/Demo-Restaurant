using System;

namespace Restaurant.Host.Ioc
{
    public interface IDependencyResolverFactory
    {
        System.Web.Http.Dependencies.IDependencyResolver CreateApiResolver();
        System.Web.Mvc.IDependencyResolver CreateMvcResolver();
    }
}
