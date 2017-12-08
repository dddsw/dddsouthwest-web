using System.Threading.Tasks;
using DDDSouthWest.Domain.Features.Account.ManagePages.CreatePage;
using DDDSouthWest.Website.Framework;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DDDSouthWest.Website.Features.Public.Account.ManageProfile
{
    [Authorize(Policy = AccessPolicies.SpeakerAccessPolicy)]
    public class ManageProfileController : Controller
    {
        private readonly IMediator _mediator;

        public ManageProfileController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [Route("/account/profile/", Name = RouteNames.ProfileManage)]
        public IActionResult Edit(int id)
        {
            return View("Edit", new ManageProfileViewModel
            {
                Id = id
            });
        }

        [HttpPost]
        [Route("/account/profile/edit")]
        public async Task<IActionResult> Edit(CreatePage.Command command)
        {
            var result = await _mediator.Send(command);

            return RedirectToAction("Edit", result.Id);
        }
    }
}