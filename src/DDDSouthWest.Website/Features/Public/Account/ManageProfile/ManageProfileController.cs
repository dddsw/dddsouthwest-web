using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DDDSouthWest.Domain.Features.Account.Admin.ManageProfile.UpdateExistingProfile;
using DDDSouthWest.Domain.Features.Account.Admin.ManageProfile.ViewProfile;
using DDDSouthWest.Website.Framework;
using DDDSouthWest.Website.ImageHandlers;
using FluentValidation;
using FluentValidation.Results;
using IdentityServer4.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DDDSouthWest.Website.Features.Public.Account.ManageProfile
{
    [Authorize(Policy = AccessPolicies.RegisteredAccessPolicy)]
    public class ManageProfileController : Controller
    {
        private readonly IMediator _mediator;
        private readonly MarkdownTransformer _transformer;
        private readonly ProfileImageHandler _imageHandler;

        public ManageProfileController(IMediator mediator, MarkdownTransformer transformer, ProfileImageHandler imageHandler)
        {
            _mediator = mediator;
            _transformer = transformer;
            _imageHandler = imageHandler;
        }

        [Route("/account/profile/", Name = RouteNames.ProfileManage)]
        public async Task<IActionResult> Index()
        {
            var userId = int.Parse(User.Identity.GetSubjectId());
            var response = await _mediator.Send(new ViewProfileDetail.Query(userId));
            
            var result = _imageHandler.ResolveProfilePicture(userId);

            return View(new ProfileDetailViewModel
            {
                Profile = response.ProfileDetailModel,
                HasProfile = response.ProfileDetailModel != null,
                ProfilePicturePath = result.Path
            });
        }
        
        [Route("/account/profile/edit", Name = RouteNames.ProfileEdit)]
        public async Task<IActionResult> Edit()
        {
            var userId = int.Parse(User.Identity.GetSubjectId());
            var response = await _mediator.Send(new ViewProfileDetail.Query(userId));

            var result = _imageHandler.ResolveProfilePicture(userId);
            
            if (response.ProfileDetailModel == null)
                return View("Edit", new ProfileEditViewModel
                {
                    HasProfile = false
                });
            
            return View("Edit", new ProfileEditViewModel
            {
                HasProfile = true,
                Id = userId,
                BioHtml = response.ProfileDetailModel.BioHtml,
                BioMarkdown = response.ProfileDetailModel.BioMarkdown,
                FamilyName = response.ProfileDetailModel.FamilyName,
                GivenName = response.ProfileDetailModel.GivenName,
                LinkedIn = response.ProfileDetailModel.LinkedIn,
                Twitter = response.ProfileDetailModel.Twitter,
                Website = response.ProfileDetailModel.Website,
                ProfilePicturePath = result.Path
            });
        }

        [HttpPost]
        [Route("/account/profile/edit")]
        public async Task<IActionResult> Edit(UpsertSpeakerProfile.Command command, IFormFile file)
        {
            var userId = User.Identity.GetSubjectId();

            command.Id = int.Parse(userId);
            command.BioHtml = _transformer.ToHtml(command.BioMarkdown);
            
            var errors = new List<ValidationFailure>();
            
            try
            {
                await _mediator.Send(command);
            }
            catch (ValidationException e)
            {
                e.Errors.ToList().ForEach(x => errors.Add(x));
            }

            _imageHandler.SaveProfilePicture(file, userId).Errors.ForEach(x => errors.Add(x));

            if (errors.Any())
                return View(new ProfileEditViewModel
                {
                    Errors = errors,
                    BioMarkdown = command.Bio,
                    FamilyName = command.FamilyName,
                    GivenName = command.GivenName,
                    Id = command.Id,
                    LinkedIn = command.LinkedIn,
                    Twitter = command.Twitter,
                    Website = command.Website
                });

            return RedirectToAction("Index");
        }
    }
}