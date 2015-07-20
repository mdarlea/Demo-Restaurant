using Restaurant.Host.Models;
using Swaksoft.Infrastructure.Crosscutting.Authorization.EntityFramework;

namespace Restaurant.Host.Authorization
{
    public class ApplicationUserDbContext : AuthorizationDbContext<ApplicationUser>
    {
        public ApplicationUserDbContext()
            : base("RestaurantDataSource")
        {
        }

        public ApplicationUserDbContext(string nameOrConnectionString) 
            : base(nameOrConnectionString)
        {
        }
    }
}