using System.Linq;
using System.Threading.Tasks;
using DDDSouthWest.Domain.Features.Public.Volunteer;
using DDDSouthWest.Website.Framework;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DDDSouthWest.Website.Features.Public.Volunteer
{
    public class VolunteerController : Controller
    {
        private readonly IMediator _mediator;

        public VolunteerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("/volunteer/", Name = RouteNames.Volunteer)]
        public IActionResult Index() 
            => View(new VolunteerViewModel());

        [Route("/volunteer/thank-you", Name = RouteNames.VolunteerSubmitted)]
        public IActionResult Complete()
            => View();

        [HttpPost]
        [Route("/volunteer/", Name = RouteNames.VolunteerSubmit)]
        public async Task<IActionResult> Create(VolunteerViewModel viewModel)
        {
            var result = await _mediator.Send(new VolunteerRegistration.Command
            {
                FullName = viewModel.FullName,
                EmailAddress = viewModel.EmailAddress,
                HelpSetup = viewModel.HelpSetup,
                PhoneNumber = viewModel.PhoneNumber
            });

            if (!result.Errors.Any())
                return RedirectToRoute(RouteNames.VolunteerSubmitted);

            return View("Index", new VolunteerViewModel
            {
                Errors = result.Errors,
                FullName = viewModel.FullName,
                EmailAddress = viewModel.EmailAddress,
                HelpSetup = viewModel.HelpSetup,
                PhoneNumber = viewModel.PhoneNumber
            });
        }
    }
}