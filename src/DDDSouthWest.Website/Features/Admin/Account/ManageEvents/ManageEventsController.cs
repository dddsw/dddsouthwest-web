using System.Linq;
using System.Threading.Tasks;
using DDDSouthWest.Domain.Features.Account.Admin.ManageEvents.CreateNewEvent;
using DDDSouthWest.Domain.Features.Account.Admin.ManageEvents.ListEvents;
using DDDSouthWest.Domain.Features.Account.Admin.ManageEvents.UpdateExistingEvent;
using DDDSouthWest.Domain.Features.Account.Admin.ManageEvents.ViewEventDetail;
using DDDSouthWest.Website.Framework;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DDDSouthWest.Website.Features.Admin.Account.ManageEvents
{
    [Authorize(Policy = AccessPolicies.OrganiserAccessPolicy)]
    public class ManageEventsController : Controller
    {
        private readonly IMediator _mediator;

        public ManageEventsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [Route("/account/events/", Name = RouteNames.EventsManage)]
        public async Task<IActionResult> Index()
        {
            var result = await _mediator.Send(new ListAllEvents.Query());
            
            return View(new EventListViewModel
            {
                Events = result.Events
            });
        }
        
        [Route("/account/events/create", Name = RouteNames.EventCreate)]
        public IActionResult Create()
        {
            return View(new ManageEventsViewModel());
        }
        
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
                return View("Edit", new ManageEventsViewModel
                {
                    Errors = e.Errors.ToList(),
                    EventDate = command.EventDate,
                    EventFilename = command.EventFilename,
                    EventName = command.EventName
                });
            }

            return RedirectToRoute(RouteNames.EventsManage);
        }
    }
}