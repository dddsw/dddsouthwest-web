using DDDSouthWest.Website.Framework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DDDSouthWest.Website.Features.Public.Account.Dashboard
{
    [Route("account/dashboard", Name = "account_dashboard")]
    [Authorize(Policy = AccessPolicies.RegisteredAccessPolicy)]
    public class DashboardController : Controller
    {        
        public IActionResult Index()
        {
            return View();
        }
    }
}