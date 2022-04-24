using DDDSouthWest.Domain.Features.Public.MeetTheTeam;
using DDDSouthWest.Website.Framework;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DDDSouthWest.Website.Features.Public.MeetTheTeam
{
    public class MeetTheTeamController : Controller
    {
        private readonly IMediator _mediator;

        public MeetTheTeamController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("/team/", Name = RouteNames.MeetTheTeam)]
        public async Task<IActionResult> Index()
        {
            var response = await _mediator.Send(new Team.Query());

            return View(new TeamMembersViewModel
            {
                TeamMembers = ToViewModel(response.TeamMembers).ToList()
            });
        }

        private static IEnumerable<TeamMemberViewModel> ToViewModel(IEnumerable<TeamMemberModel> model)
        {
            return model.Select(x => new TeamMemberViewModel
            {
                MemberId = x.MemberId,
                FullName = x.FullName,
                EmailAddress = x.EmailAddress,
                PicturePath = x.PicturePath ?? "/images/profile_default.png",
                YearJoined = x.YearJoined,
                Twitter = x.Twitter
            });
        }
    }
}