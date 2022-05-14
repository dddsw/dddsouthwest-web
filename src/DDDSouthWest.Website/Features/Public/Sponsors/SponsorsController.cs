using DDDSouthWest.Website.Framework;
using Microsoft.AspNetCore.Mvc;

namespace DDDSouthWest.Website.Features.Public.Sponsors
{
    public class SponsorsController : Controller
    {
        [Route("/sponsors/", Name = RouteNames.Sponsors)]
        public IActionResult Index()
        {
            return View(new SponsorsViewModel());
        }
    }
}
