using System;
using System.Threading.Tasks;
using DDDSouthWest.Domain.Features.Account.ManageEvents.CreateNewEvent;
using DDDSouthWest.Domain.Features.Account.ManageEvents.ListEvents;
using Shouldly;
using Xunit;

namespace DDDSouthWest.UnitTests.ManageEvents.ListEventsTests
{
    public class ListEvents
    {
        [Fact]
        public async Task View_list_of_events()
        {
            var mediator = ResolveContainer.MediatR;

            mediator.Send(new CreateNewEvent.Command
            {
                EventDate = DateTime.Now,
                EventFilename = Guid.NewGuid().ToString(),
                EventName = "Event name"
            }).Wait();

            var response = await mediator.Send(new ListAllEvents.Query());

            response.Events.ShouldNotBeEmpty();
        }
    }
}