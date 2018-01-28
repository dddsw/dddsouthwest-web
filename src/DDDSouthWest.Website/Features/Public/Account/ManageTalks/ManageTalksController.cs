using System.Linq;
using System.Threading.Tasks;
using DDDSouthWest.Domain.Features.Account.Speaker.ManageTalks.AddNewTalk;
using DDDSouthWest.Domain.Features.Account.Speaker.ManageTalks.ListTalks;
using DDDSouthWest.Domain.Features.Account.Speaker.ManageTalks.UpdateExistingTalk;
using DDDSouthWest.Domain.Features.Account.Speaker.ManageTalks.ViewTalkDetail;
using DDDSouthWest.Website.Features.Admin.Account.ManageEvents;
using DDDSouthWest.Website.Framework;
using FluentValidation;
using IdentityServer4.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DDDSouthWest.Website.Features.Public.Account.ManageTalks
{
    [Authorize(Policy = AccessPolicies.RegisteredAccessPolicy)]
    public class ManageTalksController : Controller
    {
        private readonly IMediator _mediator;
        private readonly MarkdownTransformer _transformer;

        public ManageTalksController(IMediator mediator, MarkdownTransformer transformer)
        {
            _mediator = mediator;
            _transformer = transformer;
        }

        [Route("/account/talks/", Name = RouteNames.SpeakerTalkManage)]
        public async Task<IActionResult> Index()
        {
            var userId = User.Identity.GetSubjectId();
            var result = await _mediator.Send(new ListAllTalks.Query(int.Parse(userId)));

            return View("Index", new TalkListViewModel
            {
                Talks = result.Talks
            });
        }

        [Route("/account/talks/create", Name = RouteNames.SpeakerTalkCreate)]
        public IActionResult Create()
        {
            return View(new ManageTalksViewModel());
        }
        
        [HttpPost]
        [Route("/account/talks/create")]
        public async Task<IActionResult> Create(AddNewTalk.Command command)
        {
            try
            {
                command.TalkBodyHtml = _transformer.ToHtml(command.TalkBodyMarkdown);

                var userId = User.Identity.GetSubjectId();
                command.UserId = int.Parse(userId);
                
                await _mediator.Send(command);
            }
            catch (ValidationException e)
            {
                return View(new ManageTalksViewModel
                {
                    Errors = e.Errors.ToList(),
                    TalkBodyMarkdown = command.TalkBodyMarkdown,
                    TalkTitle = command.TalkTitle,
                    TalkSummary = command.TalkSummary,
                    IsSubmitted = command.IsSubmitted
                });
            }

            return RedirectToRoute(RouteNames.SpeakerTalkManage);
        }

        [Route("/account/talks/edit/{id}", Name = RouteNames.SpeakerTalkEdit)]
        public async Task<IActionResult> Edit(ViewTalkDetail.Query query)
        {
            var userId = User.Identity.GetSubjectId();
            query.UserId = int.Parse(userId);

            var talk = await _mediator.Send(query);

            return View(new ManageTalksViewModel
            {
                Id = talk.Id,
                TalkTitle = talk.TalkTitle,
                TalkSummary = talk.TalkSummary,
                TalkBodyMarkdown = talk.TalkBodyMarkdown,
                IsSubmitted = talk.IsSubmitted
            });
        }

        [HttpPost]
        [Route("/account/talks/edit/{id}")]
        public async Task<IActionResult> Update(UpdateExistingTalk.Command command)
        {
            try
            {
                command.TalkBodyHtml = _transformer.ToHtml(command.TalkBodyMarkdown);

                var userId = User.Identity.GetSubjectId();
                command.UserId = int.Parse(userId);
                
                await _mediator.Send(command);
            }
            catch (ValidationException e)
            {
                return View(new ManageTalksViewModel
                {
                    Id = command.Id,
                    TalkTitle = command.TalkTitle,
                    TalkSummary = command.TalkSummary,
                    TalkBodyMarkdown = command.TalkBodyMarkdown,
                    IsSubmitted = command.IsSubmitted,
                    Errors = e.Errors.ToList()
                });
            }

            return RedirectToRoute(RouteNames.SpeakerTalkManage);
        }
    }
}