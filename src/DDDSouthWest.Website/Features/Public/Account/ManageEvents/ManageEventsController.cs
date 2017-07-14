using DDDSouthWest.Website.Framework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DDDSouthWest.Website.Features.Public.Account.ManageEvents
{
    [Authorize(Policy = AccessPolicies.OrganiserAccessPolicy)]
    public class ManageEventsController : Controller
    {
        [Route("/account/events/", Name = "events_manage")]
        public IActionResult Index()
        {
            return View();
        }
        
        [Route("/account/events/create", Name="events_create")]
        public IActionResult Create()
        {
            return View();
        }
    }
}