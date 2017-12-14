using System.Linq;
using System.Threading.Tasks;
using DDDSouthWest.Domain;
using DDDSouthWest.Domain.Features.Account.ManageNews.CreateNews;
using DDDSouthWest.Domain.Features.Account.ManageNews.DeleteNews;
using DDDSouthWest.Domain.Features.Account.ManageNews.ListNews;
using DDDSouthWest.Domain.Features.Account.ManageNews.UpdateExistingNews;
using DDDSouthWest.Domain.Features.Account.ManageNews.ViewNewsDetail;
using DDDSouthWest.Domain.Features.Account.ManagePages.CreatePage;
using DDDSouthWest.Website.Features.Public.Account.ManageEvents;
using DDDSouthWest.Website.Features.Public.Account.ManagePages;
using DDDSouthWest.Website.Framework;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DDDSouthWest.Website.Features.Public.Account.ManageNews
{
    [Authorize(Policy = AccessPolicies.OrganiserAccessPolicy)]
    public class ManageNewsController : Controller
    {
        private readonly IMediator _mediator;
        private readonly MarkdownTransformer _transformer;

        public ManageNewsController(IMediator mediator, MarkdownTransformer transformer)
        {
            _mediator = mediator;
            _transformer = transformer;
        }

        [Route("/account/news/", Name = RouteNames.NewsManage)]
        public async Task<IActionResult> Index()
        {
            var result = await _mediator.Send(new ListAllNews.Query());
            
            return View(new NewsListViewModel
            {
                News = result.News
            });
        }

        [Route("/account/news/create", Name = RouteNames.NewsCreate)]
        public IActionResult Create()
        {
            return View(new ManageNewsViewModel());
        }

        [HttpPost]
        [Route("/account/news/create", Name = RouteNames.NewsCreate)]
        public async Task<IActionResult> Create(CreateNews.Command command)
        {
            CreateNews.Response result;

            command.BodyHtml = _transformer.ToHtml(command.BodyMarkdown);
            
            try
            {
                result = await _mediator.Send(command);
            }
            catch (ValidationException e)
            {
                return View(new ManageNewsViewModel
                {
                    Errors = e.Errors.ToList(),
                    Title = command.Title,
                    Filename = command.Filename,
                    BodyMarkdown = command.BodyMarkdown,
                    IsLive = command.IsLive
                });
            }

            return RedirectToRoute(RouteNames.NewsEdit, new { Id = result.Id });
        }

        [Route("/account/news/edit/{id}", Name = RouteNames.NewsEdit)]
        public async Task<IActionResult> Edit(ViewNewsDetail.Query query)
        {
            var model = await _mediator.Send(query);

            return View(new ManageNewsViewModel
            {
                Id = model.Id,
                Filename = model.Filename,
                Title = model.Title,
                BodyMarkdown = model.BodyMarkdown,
                DatePosted = model.DatePosted,
                IsLive = model.IsLive
            });
        }

        [HttpPost]
        [Route("/account/news/edit")]
        public async Task<IActionResult> Update(UpdateExistingNews.Command command)
        {
            try
            {
                await _mediator.Send(command);
            }
            catch (ValidationException e)
            {
                return View("Edit", new ManageNewsViewModel
                {
                    Errors = e.Errors.ToList(),
                    Title = command.Title,
                    Filename = command.Filename,
                    DatePosted = command.DatePosted,
                    BodyMarkdown = command.BodyMarkdown,
                    IsLive = command.IsLive
                });
            }

            return RedirectToRoute(RouteNames.NewsManage);
        }
        
        [HttpGet]
        [Route("/account/news/delete/{id}", Name = RouteNames.NewsDelete)]
        public async Task<IActionResult> Delte(DeleteNews.Command command)
        {
            try
            {
                await _mediator.Send(command);
            }
            catch (ValidationException e)
            {
                return View(new ManageNewsViewModel
                {
                    Errors = e.Errors.ToList()
                });
            }

            return RedirectToRoute(RouteNames.NewsManage);
        }

    }
}