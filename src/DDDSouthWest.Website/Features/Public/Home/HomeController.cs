using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DDDSouthWest.Domain.Features.Public.News.ListNews;
using InfluxDB.Collector;
using InfluxDB.Collector.Diagnostics;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DDDSouthWest.Website.Features.Public.Home
{
    public class HomeController : Controller
    {
        private readonly IMediator _mediator;

        public HomeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> Index()
        {
            /*CollectorLog.RegisterErrorHandler((m, exception) =>
            {
                Console.WriteLine($"{m}: {exception}");
            });*/
            
            var result = await _mediator.Send(new ListAllNews.QueryByLimit(4));

            return View(new HomepageViewModel
            {
                News = result.News
            });
        }

        public async Task<IActionResult> About()
        {
            return View();
        }

        public async Task<IActionResult> Contact()
        {
            return View();
        }

        [Authorize]
        public IActionResult Secure()
        {
            ViewData["Message"] = "Secure page.";

            return View();
        }
    }
}