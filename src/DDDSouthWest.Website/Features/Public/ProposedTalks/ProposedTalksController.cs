using System.Net;
using System.Threading.Tasks;
using DDDSouthWest.Domain;
using DDDSouthWest.Website.Framework;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DDDSouthWest.Website.Features.Public.ProposedTalks
{
    public class ProposedTalksController : Controller
    {
        private readonly IMediator _mediator;

        public ProposedTalksController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("/proposed-talks/", Name = RouteNames.ProposedTalks)]
        public async Task<IActionResult> Index()
        {
            var response = await _mediator.Send(new Domain.Features.Public.ProposedTalks.ProposedTalks.Query());

            return View(new ProposedTalksViewModel
            {
                ProposedTalks = response.ProposedTalks
            });
        }
    }
}