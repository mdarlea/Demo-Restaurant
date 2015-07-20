using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Owin.Security.Twitter;

namespace Restaurant.Host.Providers
{
    public class TwitterAuthProvider : TwitterAuthenticationProvider
    {
        public const string ExternalAccessToken = "ExternalAccessToken";
        public const string ScreenName = "ScreenName";
        public const string UserId = "UserId";
        public const string AccessTokenSecret = "AccessTokenSecret";
        
        public override Task Authenticated(TwitterAuthenticatedContext context)
        {
            context.Identity.AddClaim(new Claim(ExternalAccessToken, context.AccessToken));
            context.Identity.AddClaim(new Claim(ScreenName, context.ScreenName));
            context.Identity.AddClaim(new Claim(UserId, context.UserId));
            context.Identity.AddClaim(new Claim(AccessTokenSecret, context.AccessTokenSecret));

            return Task.FromResult<object>(null);
        }

    }
}