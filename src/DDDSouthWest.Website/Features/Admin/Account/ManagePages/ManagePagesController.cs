using System.Linq;
using System.Threading.Tasks;
using DDDSouthWest.Domain.Features.Account.Admin.ManagePages.CreatePage;
using DDDSouthWest.Domain.Features.Account.Admin.ManagePages.ListPages;
using DDDSouthWest.Domain.Features.Account.Admin.ManagePages.UpdateExistingPage;
using DDDSouthWest.Domain.Features.Account.Admin.ManagePages.ViewPageDetail;
using DDDSouthWest.Website.Framework;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DDDSouthWest.Website.Features.Admin.Account.ManagePages
{
    [Authorize(Policy = AccessPolicies.OrganiserAccessPolicy)]
    public class ManagePagesController : Controller
    {
        private readonly IMediator _mediator;
        private readonly MarkdownTransformer _transformer;

        public ManagePagesController(IMediator mediator, MarkdownTransformer transformer)
        {
            _mediator = mediator;
            _transformer = transformer;
        }

        [Route("/account/pages/", Name = RouteNames.PagesManage)]
        public async Task<IActionResult> Index()
        {
            var result = await _mediator.Send(new ListAllPages.Query());
            return View(new ManagePagesListViewModel
            {
                Pages = result.Pages
            });
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
            command.BodyHtml = _transformer.ToHtml(command.BodyMarkdown);

            try
            {
                await _mediator.Send(command);
            }
            catch (ValidationException e)
            {
                return View(new ManagePagesViewModel
                {
                    Errors = e.Errors.ToList(),
                    Title = command.Title,
                    Filename = command.Filename,
                    BodyMarkdown = command.BodyMarkdown
                });
            }

            /*return RedirectToAction("Edit", new BlogPostEdit.Query { Id = id.Id });*/
            return RedirectToAction("Index");
        }    

        [Route("/account/pages/edit/{id}", Name = RouteNames.PageEdit)]
        public async Task<IActionResult> Edit(ViewPageDetail.Query query)
        {
            var response = await _mediator.Send(query);

            return View(new ManagePagesViewModel
            {
                Id = response.Id,
                BodyMarkdown = response.BodyMarkdown,
                Filename = response.Filename,
                IsLive = response.IsLive,
                Title = response.Title
            });
        }

        [HttpPost]
        [Route("/account/pages/edit/{id}")]
        public async Task<IActionResult> Edit(UpdateExistingPage.Command command)
        {
            command.BodyHtml = _transformer.ToHtml(command.BodyMarkdown);

            try
            {
                await _mediator.Send(command);
            }
            catch (ValidationException e)
            {
                return View(new ManagePagesViewModel
                {
                    Errors = e.Errors.ToList(),
                    Title = command.Title,
                    Filename = command.Filename,
                    IsLive = command.IsLive,
                    Id = command.Id,
                    BodyMarkdown = command.BodyMarkdown
                });
            }

            return RedirectToAction("Index");
        }
    }
}