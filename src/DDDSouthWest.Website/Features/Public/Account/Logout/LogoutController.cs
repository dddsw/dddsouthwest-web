using System.Threading.Tasks;
using DDDSouthWest.Website.Framework;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DDDSouthWest.Website.Features.Public.Account.Logout
{
    public class LogoutController : Controller
    {
        [Route("/account/logout/", Name = RouteNames.AccountLogout)]
        public async Task<IActionResult> Index()
        {
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
            
            return View();
        }
    }
}