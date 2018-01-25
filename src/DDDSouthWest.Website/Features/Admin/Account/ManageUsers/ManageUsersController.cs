using System.Linq;
using System.Threading.Tasks;
using DDDSouthWest.Domain.Features.Account.Admin.ManagePages.CreatePage;
using DDDSouthWest.Domain.Features.Account.Admin.ManageUsers.ListUsers;
using DDDSouthWest.Website.Framework;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DDDSouthWest.Website.Features.Admin.Account.ManageUsers
{
    [Authorize(Policy = AccessPolicies.OrganiserAccessPolicy)]
    public class ManageUsersController : Controller
    {
        private readonly IMediator _mediator;

        public ManageUsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("/account/users/", Name = RouteNames.UsersManage)]
        public async Task<IActionResult> Index()
        {
            var result = await _mediator.Send(new ListAllUsers.Query());

            return View(new ManageUsersViewModel
            {
                Users = result.Users
            });
        }

        [Route("/account/users/edit/{id}", Name = RouteNames.UserEdit)]
        public IActionResult Edit(int id)
        {
            // TODO: Pull data from database

            return View();
        }

        [HttpPost]
        [Route("/account/users/edit")]
        public async Task<IActionResult> Edit(CreatePage.Command command)
        {
            try
            {
                await _mediator.Send(command);
            }
            catch (ValidationException e)
            {
                return View(new ManageUsersViewModel
                {
                    /*Errors = e.Errors.ToList(),
                    SpeakerFirstName = command.Title,
                    SpeakerFamilyName = command.Filename,
                    SpeakerBio = command.BodyMarkdown*/
                });
            }
            
            /*return RedirectToAction("Edit", new BlogPostEdit.Query { Id = id.Id });*/
            return RedirectToAction("Edit", command.Id);
        }
    }
}