using System.Net;
using System.Threading.Tasks;
using DDDSouthWest.Domain;
using DDDSouthWest.Domain.Features.Public.ProposedTalkDetail;
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
                ProposedTalk = response.ProposedTalks
            });
        }
        
        [Route("/proposed-talks/{id}/", Name = RouteNames.ProposedTalkDetail)]
        public async Task<IActionResult> Detail(int id)
        {
            var response = await _mediator.Send(new ProposedTalkDetail.Query(id));

            return View(new ProposedTalkDetailViewModel
            {
                ProposedTalk = response.ProposedTalkDetail
            });
        }
    }
}