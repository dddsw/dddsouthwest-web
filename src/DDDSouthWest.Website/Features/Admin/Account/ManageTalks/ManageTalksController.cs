using System.Threading.Tasks;
using DDDSouthWest.Domain.Features.Account.Admin.ManageNews.ViewNewsDetail;
using DDDSouthWest.Domain.Features.Account.Admin.ManageTalks.ListTalks;
using DDDSouthWest.Domain.Features.Account.Admin.ManageTalks.ViewTalkDetail;
using DDDSouthWest.Website.Features.Admin.Account.ManageNews;
using DDDSouthWest.Website.Framework;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DDDSouthWest.Website.Features.Admin.Account.ManageTalks
{
    [Authorize(Policy = AccessPolicies.OrganiserAccessPolicy)]
    public class ManageTalksController : Controller
    {
        private readonly IMediator _mediator;

        public ManageTalksController(IMediator mediator)
        {
            _mediator = mediator;
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
        public async Task<IActionResult> Edit(ViewTalkDetail.Query query)
        {
            var model = await _mediator.Send(query);

            return View(new ViewTalkDetailViewModel
            {
                Id = model.ViewTalkDetailModel.Id,
                SpeakerBioHtml = model.ViewTalkDetailModel.SpeakerBioHtml,
                SpeakerFamilyName = model.ViewTalkDetailModel.SpeakerFamilyName,
                SpeakerGivenName = model.ViewTalkDetailModel.SpeakerGivenName,
                SpeakerId = model.ViewTalkDetailModel.SpeakerId,
                TalkBodyHtml = model.ViewTalkDetailModel.TalkBodyHtml,
                TalkTitle = model.ViewTalkDetailModel.TalkTitle 
            });
        }
        
        [Route("/account/admin/talks/create", Name = RouteNames.AdminTalkCreate)]
        public IActionResult Create()
        {
            return View(new ManageTalksViewModel());
        }
        
        /*
        [HttpPost]
        [Route("/account/events/create")]
        public async Task<IActionResult> Create(CreateNewEvent.Command command)
        {
            CreateNewEvent.Response result;

            try
            {
                result = await _mediator.Send(command);
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

            return RedirectToAction(nameof(Index));
        }

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