using System;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Facebook;
using Microsoft.Owin.Security.Google;
using Microsoft.Owin.Security.Twitter;
using Microsoft.Practices.Unity;
using Restaurant.Host.Authorization;
using Restaurant.Host.Providers;
using Swaksoft.Infrastructure.Crosscutting.Authorization.Repositories;
using Swaksoft.Infrastructure.Crosscutting.Authorization.Token;

namespace Restaurant.Host.Ioc.Unity
{
    public class UnityBootstrap
    {
        private readonly IUnityContainer _container;

        public UnityBootstrap(IUnityContainer container)
        {
            if (container == null) throw new ArgumentNullException("container");
            _container = container;
            Register();
        }

        public void Register()
        {
            //Todo: Registerr interfaces here
            RegisterAuthorization();
        }

        private void RegisterProviders()
        {
            _container.RegisterType<IGoogleOAuth2AuthenticationProvider, GoogleAuthProvider>();
            _container.RegisterType<FacebookAuthenticationProvider, FacebookAuthProvider>();
            _container.RegisterType<TwitterAuthenticationProvider, TwitterAuthProvider>();
        }

        private void RegisterAuthorization()
        {
            _container.RegisterType<ApplicationUserDbContext>(
                new HierarchicalLifetimeManager(),
                new InjectionConstructor("RestaurantDataSource"));

            _container.RegisterType<IUserStore<ApplicationUser>, ApplicationUserStore>(new HierarchicalLifetimeManager());

            _container.RegisterType<UserManager<ApplicationUser>, ApplicationUserManager>(new HierarchicalLifetimeManager());

            _container.RegisterType<IAuthenticationManager>(
                new InjectionFactory(o => HttpContext.Current.GetOwinContext().Authentication));

            _container.RegisterType<ApplicationSignInManager>();

            _container.RegisterType<ISecureDataFormat<AuthenticationTicket>>(
                new InjectionFactory(o => Startup.OAuthBearerOptions.AccessTokenFormat));

            _container.RegisterType<IIdentityRepository<ApplicationUser>, ApplicationUserRepository>();

            _container.RegisterType<IAuthenticationTicketFactory<ApplicationUser>, AuthenticationTicketFactory<ApplicationUser>>();
            _container.RegisterType<IAccessTokenGenerator<ApplicationUser>, AccessTokenGenerator<ApplicationUser>>();

            _container.RegisterType<IAuthenticationTokenFactory, AuthenticationTokenFactory<ApplicationUser>>();

            RegisterProviders();
        }
    }
}