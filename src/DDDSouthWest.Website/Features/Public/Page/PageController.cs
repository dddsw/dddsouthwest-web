using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DDDSouthWest.Website.Features.Public.Page
{
    public class PageController : Controller
    {
        public IActionResult Index(string filename)
        {
            return View($"~/Features/Public/Page/{filename}.cshtml");
        }
    }
}