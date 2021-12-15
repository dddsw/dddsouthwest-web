using System.Net;
using System.Threading.Tasks;
using DDDSouthWest.Domain;
using DDDSouthWest.Domain.Features.Public.Page;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;

namespace DDDSouthWest.Website.Features.Public.Page
{
    public class PageController : Controller
    {
        private readonly IMediator _mediator;
        private readonly ICompositeViewEngine _compositeViewEngine;

        public PageController(IMediator mediator, ICompositeViewEngine compositeViewEngine)
        {
            _mediator = mediator;
            _compositeViewEngine = compositeViewEngine;
        }
        
        public async Task<IActionResult> Index(GetPage.Query query)
        {
            string path = $"~/Features/Public/Page/{query.Filename}.cshtml";
            if (_compositeViewEngine.GetView("", path, false).Success)
            {
                return View(path);   
            }
            
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