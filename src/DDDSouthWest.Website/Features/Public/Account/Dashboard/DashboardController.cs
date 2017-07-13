using DDDSouthWest.Website.Framework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DDDSouthWest.Website.Features.Public.Account.Dashboard
{
    [Route("account/dashboard", Name = "account_dashboard")]
    [Authorize(Policy = AccessPolicies.OrganiserAccessPolicy)]
    public class DashboardController : Controller
    {
        public DashboardController()
        {
            var res = 1;
        }
        
        public IActionResult Index()
        {
            return View();
        }
    }
}