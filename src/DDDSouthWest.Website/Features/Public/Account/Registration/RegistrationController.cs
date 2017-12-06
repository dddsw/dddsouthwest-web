using System;
using System.Linq;
using System.Threading.Tasks;
using DDDSouthWest.Domain;
using DDDSouthWest.Domain.Features.Account.ManageEvents.CreateNewEvent;
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
        private readonly WebsiteSettings _settings;

        public RegistrationController(IMediator mediator, WebsiteSettings settings)
        {
            _mediator = mediator;
            _settings = settings;
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
                var res = e.Errors.Any(x => x.ErrorCode == RegisterNewUserValidator.NotUniqueErrorCode);
                
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
        public IActionResult ConfirmRegistration()
        {
            return View(new RegistrationConfirmationViewModel
            {
                RequireEmailConfirmation = _settings.RequireNewAccountConfirmation
            });
        }
    }
}