using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DDDSouthWest.Domain.Features.Public.ProposedTalkDetail;
using DDDSouthWest.Domain.Features.Public.ProposedTalks;
using DDDSouthWest.Website.Framework;
using DDDSouthWest.Website.ImageHandlers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DDDSouthWest.Website.Features.Public.ProposedTalks
{
    public class ProposedTalksController : Controller
    {
        private readonly IMediator _mediator;
        private readonly ProfileImageHandler _profileImageHandler;

        public ProposedTalksController(IMediator mediator, ProfileImageHandler profileImageHandler)
        {
            _mediator = mediator;
            _profileImageHandler = profileImageHandler;
        }

        [Route("/proposed-talks/", Name = RouteNames.ProposedTalks)]
        public async Task<IActionResult> Index()
        {
            var response = await _mediator.Send(new Domain.Features.Public.ProposedTalks.ProposedTalks.Query());

            return View(new ProposedTalksViewModel
            {
                ProposedTalks = ToViewModel(response.ProposedTalks).ToList()
            });
        }

        [Route("/proposed-talks/{id}/", Name = RouteNames.ProposedTalkDetail)]
        public async Task<IActionResult> Detail(int id)
        {
            var response = await _mediator.Send(new ProposedTalkDetail.Query(id));

            return View(new ProposedTalkDetailViewModel
            {
                ProposedTalk = response.ProposedTalkDetail,
                
            });
        }
        
        private IEnumerable<ProposedTalkViewModel> ToViewModel(IEnumerable<ProposedTalksModel> model)
        {
            return model.Select(x =>
            {
                var image = _profileImageHandler.ResolveProfilePicture(x.SpeakerId);
                return new ProposedTalkViewModel
                {
                    SpeakerFamilyName = x.SpeakerFamilyName,
                    SpeakerGivenName = x.SpeakerGivenName,
                    SpeakerId = x.SpeakerId,
                    TalkSummary = x.TalkSummary,
                    TalkId = x.TalkId,
                    TalkTitle = x.TalkTitle,
                    ProfileImage = image.Path,
                    ProfileImageExists = image.Exists
                };
            });
        }
    }
}