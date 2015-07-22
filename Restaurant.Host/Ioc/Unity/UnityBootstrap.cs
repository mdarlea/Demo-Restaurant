using System;
using System.Web;
using Application.Restaurant.ReservationModule.Services;
using Domain.Restaurant.ReservationModule.Aggregates.ReservationAgg;
using Infrastructure.Data.Restaurant.UnitOfWork;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Facebook;
using Microsoft.Owin.Security.Google;
using Microsoft.Owin.Security.Infrastructure;
using Microsoft.Owin.Security.OAuth;
using Microsoft.Owin.Security.Twitter;
using Microsoft.Practices.Unity;
using Restaurant.Host.Authorization;
using Restaurant.Host.Models;
using Restaurant.Host.Providers;
using Swaksoft.Domain.Seedwork;
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

        /// <summary>
        /// Registers OWIN authorization, application services and the repository interfaces
        /// </summary>
        public void Register()
        {
            RegisterAuthorization();
            RegisterAppServices();
            RegisterRepositories();
        }

        /// <summary>
        /// Registers Owin Authorzation
        /// </summary>
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

        private void RegisterProviders()
        {
            _container.RegisterType<OAuthAuthorizationServerProvider, ApplicationOAuthProvider>();

            _container.RegisterType<IAuthenticationTokenProvider, OAuthRefreshTokenProvider>();

            _container.RegisterType<IGoogleOAuth2AuthenticationProvider, GoogleAuthProvider>();
            _container.RegisterType<FacebookAuthenticationProvider, FacebookAuthProvider>();
            _container.RegisterType<TwitterAuthenticationProvider, TwitterAuthProvider>();
        }

        /// <summary>
        /// Registers the application services
        /// </summary>
        private void RegisterAppServices()
        {
            _container.RegisterType<IReservationAppService, ReservationAppService>();
        }

        /// <summary>
        /// Registers repository contracts
        /// </summary>
        private void RegisterRepositories()
        {
            _container.RegisterType<IReservationRepository>();
        }

        /// <summary>
        /// Registers the restaurant unit of work
        /// </summary>
        private void RegisterUnitOfWorks()
        {
            _container.RegisterType<ITransactionUnitOfWork, RestaurantUnitOfWorkMySql>(
                new PerResolveLifetimeManager(),
                new InjectionConstructor("RestaurantDataSource"));
        }

    }
}