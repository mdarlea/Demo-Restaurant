using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Restaurant.Host.Models;

namespace Restaurant.Host.Authorization
{
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store) 
            : base(store)
        {
        }
    }

    public class ApplicationUserStore : UserStore<ApplicationUser>
    {
        public ApplicationUserStore(ApplicationUserDbContext context)
            :base(context)
        {
        }
    }
}