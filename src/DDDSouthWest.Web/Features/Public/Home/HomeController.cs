using Microsoft.AspNetCore.Mvc;

namespace DDDSouthWest.Web.Features.Public.Home
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}