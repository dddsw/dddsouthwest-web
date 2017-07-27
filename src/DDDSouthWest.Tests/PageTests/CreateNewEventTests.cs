using System;
using System.Threading.Tasks;
using DDDSouthWest.Domain.Features.Account.ManageEvents.CreateNewEvent;
using DDDSouthWest.Tests.Framework;
using Shouldly;
using Xunit;

namespace DDDSouthWest.Tests.PageTests
{
    public class CreateNewEventTests
    {
        [Fact]
        public async Task Should_create_new_event()
        {
            var mediator = ResolveContainer.GetMediator();

            var result = await mediator.Send(new CreateNewEvent.Command
            {
                EventDate = DateTime.Now,
                EventFilename = "Event filename",
                EventName = "Event name"
            });
            
            result.Id.ShouldBe(5);
        }
    }
}