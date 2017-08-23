using System;
using System.Threading.Tasks;
using DDDSouthWest.Domain.Features.Public.News.ListNews;
using DDDSouthWest.Domain.Features.Public.News.NewsDetail;
using DDDSouthWest.Website.Framework;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DDDSouthWest.Website.Features.Public.News
{
    public class NewsController : Controller
    {
        private readonly IMediator _mediator;

        public NewsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("/news/", Name = RouteNames.NewsList)]
        public async Task<IActionResult> Index()
        {
            var result = await _mediator.Send(new ListAllNews.QueryByLimit(4));

            return View(new NewsListViewModel
            {
                News = result.News
            });
        }
        
        [Route("/news/{id}/{filename}", Name = RouteNames.NewsDetail)]
        public async Task<IActionResult> Detail(ViewLiveNewsDetail.Query query)
        {
            var result = await _mediator.Send(query);

            return View(new NewsDetailViewModel
            {
                Id = result.Id,
                Title = result.Title,
                Body = result.Body,
                DatePosted = result.DatePosted,
                CanonicalFilename = result.Filename,
            });
        }

    }
}