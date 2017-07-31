using System.Threading.Tasks;
using DDDSouthWest.Domain.Features.Account.ManageEvents.ListEvents;
using DDDSouthWest.Domain.Features.Account.ManageTalks.ListTalks;
using DDDSouthWest.Website.Framework;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DDDSouthWest.Website.Features.Public.Account.ManageTalks
{
    [Authorize(Policy = AccessPolicies.OrganiserAccessPolicy)]
    public class ManageTalksController : Controller
    {
        private readonly IMediator _mediator;

        public ManageTalksController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("/account/talks/", Name = RouteNames.TalksManage)]
        public async Task<IActionResult> Index()
        {
            var result = await _mediator.Send(new ListAllTalks.Query());

            return View(new TalkListViewModel
            {
                Talks = result.Talks
            });
        }

        [Route("/account/talks/create", Name = RouteNames.TalkCreate)]
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