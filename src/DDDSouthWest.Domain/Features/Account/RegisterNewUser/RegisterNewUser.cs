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
        }

        public class Handler : IAsyncNotificationHandler<Command>
        {
            private readonly RegisterNewUserValidator _validator;
            private readonly CreateNewRegisteredUser _createNewRegisteredUser;

            public Handler(RegisterNewUserValidator validator, CreateNewRegisteredUser createNewRegisteredUser)
            {
                _validator = validator;
                _createNewRegisteredUser = createNewRegisteredUser;
            }

            public async Task Handle(Command message)
            {
                _validator.ValidateAndThrow(message);

                await _createNewRegisteredUser.Invoke(message);
            }
        }
    }
}