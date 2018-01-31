using System.Linq;
using System.Threading.Tasks;
using DDDSouthWest.Domain.Features.Account.Admin.ManageEvents.CreateNewEvent;
using DDDSouthWest.Domain.Features.Account.Admin.ManageNews.UpdateExistingNews;
using DDDSouthWest.Domain.Features.Account.Admin.ManageNews.ViewNewsDetail;
using DDDSouthWest.Domain.Features.Account.Admin.ManageTalks.ListTalks;
using DDDSouthWest.Domain.Features.Account.Admin.ManageTalks.UpdateExistingTalk;
using DDDSouthWest.Domain.Features.Account.Admin.ManageTalks.ViewTalkDetail;
using DDDSouthWest.Website.Features.Admin.Account.ManageEvents;
using DDDSouthWest.Website.Features.Admin.Account.ManageNews;
using DDDSouthWest.Website.Framework;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DDDSouthWest.Website.Features.Admin.Account.ManageTalks
{
    [Authorize(Policy = AccessPolicies.OrganiserAccessPolicy)]
    public class AdminManageTalksController : Controller
    {
        private readonly IMediator _mediator;
        private readonly MarkdownTransformer _transformer;

        public AdminManageTalksController(IMediator mediator, MarkdownTransformer transformer)
        {
            _mediator = mediator;
            _transformer = transformer;
        }

        [Route("/account/admin/talks/", Name = RouteNames.AdminTalkManage)]
        public async Task<IActionResult> List()
        {
            var result = await _mediator.Send(new ListAllTalks.Query());

            return View("List", new ListViewModel
            {
                Talks = result.Talks
            });
        }

        [Route("/account/admin/talks/edit/{id}", Name = RouteNames.AdminTalkEdit)]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await _mediator.Send(new ViewTalkDetail.Query(id));

            var viewModel = new ManageTalksViewModel
            {
                Id = model.ViewTalkDetailModel.Id,
                SpeakerFamilyName = model.ViewTalkDetailModel.SpeakerFamilyName,
                SpeakerGivenName = model.ViewTalkDetailModel.SpeakerGivenName,
                TalkSummary = model.ViewTalkDetailModel.TalkSummary,
                TalkBodyHtml = model.ViewTalkDetailModel.TalkBodyHtml,
                TalkBodyMarkdown = model.ViewTalkDetailModel.TalkBodyMarkdown,
                TalkTitle = model.ViewTalkDetailModel.TalkTitle,
                IsApproved = model.ViewTalkDetailModel.IsApproved,
                IsSubmitted = model.ViewTalkDetailModel.IsSubmitted
            };

            return View(viewModel);
        }
        
        [HttpPost]
        [Route("/account/admin/talks/edit/{id}", Name = RouteNames.AdminTalkUpdate)]
        public async Task<IActionResult> Update(UpdateExistingTalk.Command command)
        {
            command.TalkBodyHtml = _transformer.ToHtml(command.TalkBodyMarkdown);
            
            try
            {
                await _mediator.Send(command);
            }
            catch (ValidationException e)
            {
                return View("Edit", new ManageTalksViewModel
                {
                    Errors = e.Errors.ToList(),
                    Id = command.Id,
                    TalkSummary = command.TalkSummary,
                    TalkBodyHtml = command.TalkBodyHtml,
                    TalkBodyMarkdown = command.TalkBodyMarkdown,
                    TalkTitle = command.TalkTitle,
                    IsApproved = command.IsApproved
                });
            }

            return RedirectToRoute(RouteNames.AdminTalkManage);
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