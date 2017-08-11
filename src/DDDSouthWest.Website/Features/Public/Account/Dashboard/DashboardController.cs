using DDDSouthWest.Website.Framework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DDDSouthWest.Website.Features.Public.Account.Dashboard
{
    [Route("account/", Name = RouteNames.AccountDashboard)]
    [Authorize(Policy = AccessPolicies.RegisteredAccessPolicy)]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            var viewModel = new DashboardViewModel
            {
                IsOrganiser = User.HasClaim("role", "organiser"),
                IsRegistered = User.HasClaim("role", "registered")
            };

            return View(viewModel);
        }
    }

    public class DashboardViewModel
    {
        public bool IsOrganiser { get; set; }

        public bool IsRegistered { get; set; }
    }
}