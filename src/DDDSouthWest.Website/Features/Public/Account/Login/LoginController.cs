using DDDSouthWest.Website.Framework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DDDSouthWest.Website.Features.Public.Account.Login
{
    [Route("account/login", Name = RouteNames.AccountLogin)]
    public class LoginController : Controller
    {
        [Authorize]
        public IActionResult Login()
        {
            return RedirectToRoute(RouteNames.AccountDashboard);
        }
    }
}