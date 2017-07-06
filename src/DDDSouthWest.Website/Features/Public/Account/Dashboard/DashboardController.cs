using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DDDSouthWest.Website.Features.Public.Account.Dashboard
{
    [Authorize]
    [Route("account/dashboard", Name = "account_dashboard")]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }        
    }
}