using Microsoft.AspNet.Identity;
using Restaurant.Host.Models;
using Swaksoft.Infrastructure.Crosscutting.Authorization.Repositories;

namespace Restaurant.Host.Authorization
{
    public class ApplicationUserRepository 
        : IdentityRepository<ApplicationUser>
    {
        public ApplicationUserRepository(
            ApplicationUserDbContext context, 
            UserManager<ApplicationUser> userManager) 
            : base(context, userManager)
        {
        }
    }
}