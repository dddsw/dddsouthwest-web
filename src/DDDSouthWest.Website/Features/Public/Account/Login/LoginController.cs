using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DDDSouthWest.Website.Features.Public.Account.Login
{
    [Route("account/login", Name = "account_login")]
    public class LoginController : Controller
    {
        [Authorize]
        public IActionResult Login()
        {
            return RedirectToRoute("account_dashboard");
        }
    }
}