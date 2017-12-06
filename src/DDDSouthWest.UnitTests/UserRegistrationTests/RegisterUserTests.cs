using System.Threading.Tasks;
using DDDSouthWest.Domain.Features.Account.RegisterNewUser;
using Shouldly;
using Xunit;

namespace DDDSouthWest.UnitTests.UserRegistrationTests
{
    public class RegisterUserTests
    {
        [Fact]
        public async Task Register_new_user()
        {
            var mediator = ResolveContainer.MediatR;

            Should.NotThrow(async () =>
            {
                await mediator.Publish(new RegisterNewUser.Command
                {
                    EmailAddress = "test@test.com",
                    Password = "letmein"
                });
            });
        }
    }
}