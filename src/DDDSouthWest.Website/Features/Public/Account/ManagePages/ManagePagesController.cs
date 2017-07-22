using System.Linq;
using System.Threading.Tasks;
using DDDSouthWest.Domain.Features.Account.ManagePages.CreatePage;
using DDDSouthWest.Website.Framework;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DDDSouthWest.Website.Features.Public.Account.ManagePages
{
    [Authorize(Policy = AccessPolicies.OrganiserAccessPolicy)]
    public class ManagePagesController : Controller
    {
        private readonly IMediator _mediator;

        public ManagePagesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("/account/pages/", Name = RouteNames.PagesManage)]
        public IActionResult Index()
        {
            return View();
        }

        [Route("/account/pages/create", Name = RouteNames.PageCreate)]
        public IActionResult Create()
        {
            return View(new ManagePagesViewModel());
        }

        [HttpPost]
        [Route("/account/pages/create")]
        public async Task<IActionResult> Create(CreatePage.Command command)
        {
            CreatePage.Response result;

            try
            {
                result = await _mediator.Send(command);
            }
            catch (ValidationException e)
            {
                return View(new ManagePagesViewModel
                {
                    Errors = e.Errors.ToList(),
                    PageTitle = command.PageTitle,
                    PageFilename = command.PageFilename,
                    PageBody = command.PageBody
                });
            }

            /*return RedirectToAction("Edit", new BlogPostEdit.Query { Id = id.Id });*/
            return RedirectToAction("Edit", result.Id);
        }

        [Route("/account/pages/edit/{id}", Name = RouteNames.PageEdit)]
        public IActionResult Edit(int id)
        {
            // TODO: Pull data from database

            return View(new ManagePagesViewModel
            {
                Id = id
            });
        }

        [HttpPost]
        [Route("/account/pages/edit")]
        public async Task<IActionResult> Edit(CreatePage.Command command)
        {
            var result = await _mediator.Send(command);

            /*return RedirectToAction("Edit", new BlogPostEdit.Query { Id = id.Id });*/
            return RedirectToAction("Edit", result.Id);
        }
    }
}