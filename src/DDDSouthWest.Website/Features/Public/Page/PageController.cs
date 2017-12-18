using System.Net;
using System.Threading.Tasks;
using DDDSouthWest.Domain;
using DDDSouthWest.Domain.Features.Public.Page;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DDDSouthWest.Website.Features.Public.Page
{
    public class PageController : Controller
    {
        private readonly IMediator _mediator;

        public PageController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        public async Task<IActionResult> Index(GetPage.Query query)
        {
            /* TODO: Check to see if view exists on disk, if not then load from DB
             * return View($"~/Features/Public/Page/{filename}.cshtml");
             */

            GetPage.Response response;
            try
            {
                response = await _mediator.Send(query);
            }
            catch (RecordNotFoundException)
            {
                HttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
                return View("~/Features/Public/Page/notfound.cshtml");
            }
            
            return View(new PageDetailViewModel
            {
                PageDetail = response?.PageDetail
            });
        }
    }
}