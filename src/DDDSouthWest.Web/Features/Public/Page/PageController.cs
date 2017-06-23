using System.Threading.Tasks;
using DDDSouthWest.Domain.Features.Public.Page;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DDDSouthWest.Web.Features.Public.Page
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
            var page = await _mediator.Send(query);
            
            return View(new PageViewModel
            {
                Reponse = page
            });
        }
    }
}