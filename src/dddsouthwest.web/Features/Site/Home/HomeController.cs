using Microsoft.AspNetCore.Mvc;

namespace DDDSouthWest.Web.Features.Site.Home
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}