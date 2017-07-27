using System;
using System.Threading.Tasks;
using DDDSouthWest.Domain.Features.Account.ManageEvents.CreateNewEvent;
using DDDSouthWest.Domain.Features.Public.Page;
using Microsoft.Extensions.DependencyInjection;
using TestStack.BDDfy;
using Xunit;

namespace DDDSouthWest.Tests.PageTests
{
    public class CreateNewEventTests
    {

        private IServiceProvider _serviceProvider;
        
        public CreateNewEventTests()
        {
            var services = new ServiceCollection();

            services.AddTransient<CreateNewEvent>();

            _serviceProvider = services.BuildServiceProvider();
        }

        [Given("Given the page handler exits")]
        private void GivenTheHandlerExists()
        {
            _handler = new GetPage.Handler();
        }

        private async Task WhenTheUserRequestsAPage()
        {
            _response = await _handler.Handle(new GetPage.Query {Filename = Filename});
        }

        private void ThenThePageShouldBeLoaded()
        {
            _response.Filename.ShouldBe(Filename);
            _response.BodyContent.ShouldNotBeNullOrEmpty();
        }

        [Fact]
        public void Execute()
        {
            this.BDDfy();
        }
    }
}