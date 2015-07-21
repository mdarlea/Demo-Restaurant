using System;
using System.Threading.Tasks;
using System.Web.Http;
using Restaurant.Host.Authorization;
using Restaurant.Host.Helpers;
using Restaurant.Host.Models;
using Swaksoft.Infrastructure.Crosscutting.Authorization.Repositories;
using Swaksoft.Infrastructure.Crosscutting.Authorization.Token;

namespace Restaurant.Host.Controllers
{
    [RoutePrefix("api/Account")]
    public class AuthController : RestaurantApiController
    {
        private readonly ApplicationSignInManager _signInManager;
        private readonly IIdentityRepository<ApplicationUser> _repository;
        private readonly IAccessTokenGenerator<ApplicationUser> _oauthTokenGenerator;

        public AuthController(
            ApplicationSignInManager signInManager, 
            IIdentityRepository<ApplicationUser> repository,
            IAccessTokenGenerator<ApplicationUser> oauthTokenGenerator)
        {
            if (signInManager == null) throw new ArgumentNullException("signInManager");
            if (repository == null) throw new ArgumentNullException("repository");
            if (oauthTokenGenerator == null) throw new ArgumentNullException("oauthTokenGenerator");

            _signInManager = signInManager;
            _repository = repository;
            _oauthTokenGenerator = oauthTokenGenerator;
        }

        // POST api/Account/Register
        [AllowAnonymous]
        [Route("Register")]
        public async Task<IHttpActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser {UserName = model.Email, Email = model.Email, Hometown = model.Hometown};
                
                var result = await _repository.RegisterUser(user, model.Password);
                var errorResult = GetErrorResult(result);

                if (errorResult != null)
                {
                    return errorResult;
                }

                if (!result.Succeeded) return BadRequest(ModelState);

                await _signInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                //generate access token response
                var accessTokenResponse = await _oauthTokenGenerator.GenerateLocalAccessToken(user, model.ClientId);

                return Ok(accessTokenResponse);
            }
            return BadRequest(ModelState);
        }
    }
}