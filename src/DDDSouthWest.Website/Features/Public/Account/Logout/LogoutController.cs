using Microsoft.AspNetCore.Mvc;

namespace DDDSouthWest.Website.Features.Public.Account.Logout
{
    public class LogoutController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return
            View();
        }
    }
}