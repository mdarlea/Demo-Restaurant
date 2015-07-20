using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Restaurant.Host.Authorization;
using Restaurant.Host.Models;

namespace Restaurant.Host.Controllers
{
    [Authorize]
    public class MeController : ApiController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        
        public MeController(UserManager<ApplicationUser> userManager)
        {
            if (userManager == null) throw new ArgumentNullException("userManager");
            _userManager = userManager;
        }

        // GET api/Me
        public GetViewModel Get()
        {
            var user = _userManager.FindById(User.Identity.GetUserId());
            return new GetViewModel() { Hometown = user.Hometown };
        }
    }
}