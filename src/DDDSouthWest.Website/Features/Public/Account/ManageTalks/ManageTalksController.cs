using System.Linq;
using System.Threading.Tasks;
using DDDSouthWest.Domain.Features.Account.Speaker.ManageTalks.AddNewTalk;
using DDDSouthWest.Domain.Features.Account.Speaker.ManageTalks.ListTalks;
using DDDSouthWest.Website.Framework;
using FluentValidation;
using IdentityServer4.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DDDSouthWest.Website.Features.Public.Account.ManageTalks
{
    [Authorize(Policy = AccessPolicies.SpeakerAccessPolicy)]
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

            return View(new TalkListViewModel
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
                    TalkSummary = command.TalkSummary
                });
            }

            return RedirectToRoute(RouteNames.SpeakerTalkManage);
        }

        /*
        [Route("/account/events/edit/{id}", Name = RouteNames.EventEdit)]
        public async Task<IActionResult> Edit(ViewEventDetail.Query query)
        {
            var eventModel = await _mediator.Send(query);

            return View(new ManageEventsViewModel
            {
                Id = eventModel.Id,
                EventFilename = eventModel.EventFilename,
                EventDate = eventModel.EventDate,
                EventName = eventModel.EventName
            });
        }

        [HttpPost]
        [Route("/account/events/edit")]
        public async Task<IActionResult> Update(UpdateExistingEvent.Command command)
        {
            try
            {
                await _mediator.Send(command);
            }
            catch (ValidationException e)
            {
                return View(new ManageEventsViewModel
                {
                    Errors = e.Errors.ToList(),
                    EventDate = command.EventDate,
                    EventFilename = command.EventFilename,
                    EventName = command.EventName
                });
            }

            return RedirectToRoute(RouteNames.EventsManage);
        }*/
    }
}