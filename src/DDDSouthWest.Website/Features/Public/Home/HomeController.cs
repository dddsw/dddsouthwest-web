using Microsoft.AspNetCore.Mvc;

namespace DDDSouthWest.Website.Features.Public.Home
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}