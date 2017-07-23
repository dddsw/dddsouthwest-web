using System.Linq;
using System.Threading.Tasks;
using DDDSouthWest.Domain.Features.Account.ManagePages.CreatePage;
using DDDSouthWest.Website.Framework;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DDDSouthWest.Website.Features.Public.Account.ManageSpeakers
{
    [Authorize(Policy = AccessPolicies.OrganiserAccessPolicy)]
    public class ManageSpeakersController : Controller
    {
        private readonly IMediator _mediator;

        public ManageSpeakersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("/account/speakers/", Name = RouteNames.SpeakersManage)]
        public IActionResult Index()
        {
            return View();
        }

        [Route("/account/speakers/edit/{id}", Name = RouteNames.SpeakerEdit)]
        public IActionResult Edit(int id)
        {
            // TODO: Pull data from database

            return View(new ManageSpeakersViewModel
            {
                Id = id
            });
        }

        [HttpPost]
        [Route("/account/speakers/edit")]
        public async Task<IActionResult> Edit(CreatePage.Command command)
        {
            try
            {
                await _mediator.Send(command);
            }
            catch (ValidationException e)
            {
                return View(new ManageSpeakersViewModel
                {
                    Errors = e.Errors.ToList(),
                    SpeakerFirstName = command.PageTitle,
                    SpeakerFamilyName = command.PageFilename,
                    SpeakerBio = command.PageBody
                });
            }
            
            /*return RedirectToAction("Edit", new BlogPostEdit.Query { Id = id.Id });*/
            return RedirectToAction("Edit", command.Id);
        }
    }
}