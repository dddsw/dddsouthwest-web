using System.Threading.Tasks;
using DDDSouthWest.Website.Features.Public.ProposedTalks;
using DDDSouthWest.Website.Framework;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DDDSouthWest.Website.Features.Public.Speakers
{
    public class SpeakersController : Controller
    {
        private readonly IMediator _mediator;

        public SpeakersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("/speaker/", Name = RouteNames.SpeakerDetail)]
        public async Task<IActionResult> Index()
        {
            var response = await _mediator.Send(new Domain.Features.Public.ProposedTalks.ProposedTalks.Query());

            return View(new ProposedTalksViewModel
            {
                ProposedTalk = response.ProposedTalks
            });
        }
    }
}