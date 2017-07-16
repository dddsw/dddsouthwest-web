using System.Threading.Tasks;
using DDDSouthWest.Domain.Features.Account.ManageEvents.CreateEvent;
using DDDSouthWest.Website.Framework;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DDDSouthWest.Website.Features.Public.Account.ManageEvents
{
    [Authorize(Policy = AccessPolicies.OrganiserAccessPolicy)]
    public class ManageEventsController : Controller
    {
        private readonly IMediator _mediator;

        public ManageEventsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [Route("/account/events/", Name = "events_manage")]
        public IActionResult Index()
        {
            return View();
        }
        
        [Route("/account/events/create", Name="events_create")]
        public IActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        [Route("/account/events/create", Name="events_create")]
        public async Task<IActionResult> Create(CreateEvent.Command command)
        {
            var result = await _mediator.Send(command);
            
            /*return RedirectToAction("Edit", new BlogPostEdit.Query { Id = id.Id });*/
            return RedirectToAction("Edit", result.Id);
        }
        
        [Route("/account/events/edit", Name="events_create")]
        public IActionResult Edit(int id)
        {
            return View();
        }
    }
}