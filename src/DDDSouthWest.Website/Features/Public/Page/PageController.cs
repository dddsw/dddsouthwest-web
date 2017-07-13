using System.Linq;
using DDDSouthWest.Website.Framework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DDDSouthWest.Website.Features.Public.Page
{
    [Authorize]
    public class PageController : Controller
    {
        public IActionResult Index(string filename)
        {
            return View($"~/Features/Public/Page/{filename}.cshtml");
        }
        
        [HttpGet]
        public IActionResult Claims()
        {
            return new JsonResult(from c in User.Claims select new { c.Type, c.Value });
        }
    }
}