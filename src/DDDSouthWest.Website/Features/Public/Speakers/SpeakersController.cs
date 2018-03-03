using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DDDSouthWest.Domain.Features.Public.ProposedTalks;
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
                ProposedTalks = ToViewModel(response.ProposedTalks).ToList()
            });
        }
        
        private static IEnumerable<ProposedTalkViewModel> ToViewModel(IEnumerable<ProposedTalksModel> model)
        {
            return model.Select(x => new ProposedTalkViewModel
            {
                SpeakerFamilyName = x.SpeakerFamilyName,
                SpeakerGivenName = x.SpeakerGivenName,
                SpeakerId = x.SpeakerId,
                TalkSummary = x.TalkSummary,
                TalkId = x.TalkId,
                TalkTitle = x.TalkTitle
            });
        }

    }
}