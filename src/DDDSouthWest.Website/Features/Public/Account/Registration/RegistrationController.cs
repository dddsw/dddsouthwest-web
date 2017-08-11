using System;
using System.Linq;
using System.Threading.Tasks;
using DDDSouthWest.Domain.Features.Account.RegisterNewUser;
using DDDSouthWest.Website.Framework;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DDDSouthWest.Website.Features.Public.Account.Registration
{
    public class RegistrationController : Controller
    {
        private readonly IMediator _mediator;

        public RegistrationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("/account/register/", Name = RouteNames.AccountRegistration)]
        public IActionResult Index()
        {
            return View(new RegistrationViewModel());
        }

        [HttpPost]
        [Route("/account/register/create")]
        public async Task<IActionResult> Create(RegisterNewUser.Command register)
        {
            try
            {
                await _mediator.Publish(register);
            }
            catch (ValidationException e)
            {
                return View("Index", new RegistrationViewModel
                {
                    EmailAddress = register.EmailAddress,
                    Password = register.Password,
                    Errors = e.Errors.ToList()
                });    
            }
            
            return RedirectToAction(nameof(ConfirmRegistration));
        }
        
        [Route("/account/register/complete")]
        public async Task<IActionResult> ConfirmRegistration()
        {
            return View();
        }
    }
}