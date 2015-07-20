using Swaksoft.Infrastructure.Crosscutting.Authorization.EntityFramework;

namespace Restaurant.Host.Authorization
{
    public class ApplicationUserDbContext : AuthorizationDbContext<ApplicationUser>
    {
        public ApplicationUserDbContext(string nameOrConnectionString) 
            : base(nameOrConnectionString)
        {
        }
    }
}