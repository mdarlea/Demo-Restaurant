using System;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.DataProtection;
using Microsoft.Owin.Security.Facebook;
using Microsoft.Owin.Security.Google;
using Microsoft.Owin.Security.Infrastructure;
using Microsoft.Owin.Security.OAuth;
using Microsoft.Owin.Security.Twitter;
using Owin;
using Restaurant.Host.Authorization;
using Restaurant.Host.Models;
using Restaurant.Host.Providers;
using Swaksoft.Configuration.Social;
using Swaksoft.Core.External;

namespace Restaurant.Host
{
    public partial class Startup
    {
        public static OAuthBearerAuthenticationOptions OAuthBearerOptions { get; private set; }
        public static GoogleOAuth2AuthenticationOptions GoogleAuthOptions { get; private set; }
        public static FacebookAuthenticationOptions FacebookAuthOptions { get; private set; }
        public static TwitterAuthenticationOptions TwitterAuthOptions { get; private set; }

        private static readonly object thisObject = new object();

        // Enable the application to use OAuthAuthorization. You can then secure your Web APIs
        static Startup()
        {
            PublicClientId = "web";

            var provider = DependencyResolver.Current.GetService<OAuthAuthorizationServerProvider>();
            var oauthRefreshTokenProvider = DependencyResolver.Current.GetService<IAuthenticationTokenProvider>();

            OAuthOptions = new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/Token"),
                AuthorizeEndpointPath = new PathString("/Account/Authorize"),
                Provider = provider,
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(14),
                AllowInsecureHttp = true,
                AccessTokenProvider = oauthRefreshTokenProvider
            };
        }

        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }

        public static string PublicClientId { get; private set; }

        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            var googleProvider = DependencyResolver.Current.GetService<IGoogleOAuth2AuthenticationProvider>();
            var facebookProvider = DependencyResolver.Current.GetService<FacebookAuthenticationProvider>();
            var twitterProvider = DependencyResolver.Current.GetService<TwitterAuthenticationProvider>();

            // Enable the application to use a cookie to store information for the signed in user
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                Provider = new CookieAuthenticationProvider
                {
                    // Enables the application to validate the security stamp when the user logs in.
                    // This is a security feature which is used when you change a password or add an external login to your account.  
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<UserManager<ApplicationUser>, ApplicationUser>(
                        validateInterval: TimeSpan.FromMinutes(20),
                        regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager))
                }
            });
            // Use a cookie to temporarily store information about a user logging in with a third party login provider
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Enables the application to temporarily store user information when they are verifying the second factor in the two-factor authentication process.
            app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(5));

            // Enables the application to remember the second login verification factor such as phone or email.
            // Once you check this option, your second step of verification during the login process will be remembered on the device where you logged in from.
            // This is similar to the RememberMe option when you log in.
            app.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);

            // Enable the application to use bearer tokens to authenticate users
            app.UseOAuthBearerTokens(OAuthOptions);

            //get the configuration settings for external providers
            var configSection = ConfigurationSettings.Current;

            var twitter = configSection.ProvidersCollection[ExternalProvider.Twitter];
            var facebook = configSection.ProvidersCollection[ExternalProvider.Facebook];
            var google = configSection.ProvidersCollection[ExternalProvider.Google];

            var oauthRefreshTokenProvider = DependencyResolver.Current.GetService<IAuthenticationTokenProvider>();
            lock (thisObject)
            {
                OAuthBearerOptions = new OAuthBearerAuthenticationOptions()
                {
                    AccessTokenProvider = oauthRefreshTokenProvider
                };

                //Configure Facebook External Login
                FacebookAuthOptions = new FacebookAuthenticationOptions
                {
                    AppId = facebook.ConsumerKey,
                    AppSecret = facebook.ConsumerSecret,
                    Provider = facebookProvider
                };

                TwitterAuthOptions = new TwitterAuthenticationOptions
                {
                    ConsumerKey = twitter.ConsumerKey,
                    ConsumerSecret = twitter.ConsumerSecret,
                    Provider = twitterProvider
                };

                GoogleAuthOptions = new GoogleOAuth2AuthenticationOptions
                {
                    ClientId = google.ConsumerKey,
                    ClientSecret = google.ConsumerSecret,
                    Provider = googleProvider
                };
            }

            app.UseOAuthBearerAuthentication(OAuthBearerOptions);
            

            // Uncomment the following lines to enable logging in with third party login providers
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            app.UseTwitterAuthentication(TwitterAuthOptions);
            app.UseFacebookAuthentication(FacebookAuthOptions);
            app.UseGoogleAuthentication(GoogleAuthOptions);
        }
    }
}
