using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;

namespace DDDSouthWest.Domain.Features.Account.RegisterNewUser
{
    public class RegisterNewUser
    {
        public class Command : INotification
        {
            public string EmailAddress { get; set; }

            public string Password { get; set; }

            public bool ReceiveNewsletter { get; set; }
        }

        public class Handler : INotificationHandler<Command>
        {
            private readonly RegisterNewUserValidator _validator;
            private readonly CreateNewRegisteredUser _createNewRegisteredUser;
            private readonly IRegistrationConfirmation _registrationConfirmation;

            public Handler(RegisterNewUserValidator validator, CreateNewRegisteredUser createNewRegisteredUser, IRegistrationConfirmation registrationConfirmation)
            {
                _validator = validator;
                _createNewRegisteredUser = createNewRegisteredUser;
                _registrationConfirmation = registrationConfirmation;
            }

            public async Task Handle(Command message, CancellationToken cancellationToken)
            {
                var validationResult = _validator.Validate(message);
                if (!validationResult.IsValid)
                    throw new ValidationException(validationResult.Errors);

                await _createNewRegisteredUser.Invoke(message);
                await _registrationConfirmation.Notify(message.EmailAddress);
            }
        }
    }
}