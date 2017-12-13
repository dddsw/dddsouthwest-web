using System.Threading.Tasks;
using DDDSouthWest.Domain.Features.Account.ManagePages.CreatePage;
using DDDSouthWest.Domain.Features.Account.ManageProfile.ViewProfile;
using DDDSouthWest.Website.Framework;
using IdentityServer4.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DDDSouthWest.Website.Features.Public.Account.ManageProfile
{
    [Authorize(Policy = AccessPolicies.RegisteredAccessPolicy)]
    public class ManageProfileController : Controller
    {
        private readonly IMediator _mediator;

        public ManageProfileController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("/account/profile/", Name = RouteNames.ProfileManage)]
        public async Task<IActionResult> Index()
        {
            var userId = User.Identity.GetSubjectId();
            var response = await _mediator.Send(new ViewProfileDetail.Query(int.Parse(userId)));

            return View(new ProfileDetailViewModel
            {
                Profile = response.ProfileDetailModel
            });
        }
        
        [Route("/account/profile/edit", Name = RouteNames.ProfileEdit)]
        public async Task<IActionResult> Edit()
        {
            // TODO: Get ID from token
            var response = await _mediator.Send(new ViewProfileDetail.Query(1));
            
            return View("Edit", new ProfileEditViewModel
            {
                Profile = response.ProfileDetailModel
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