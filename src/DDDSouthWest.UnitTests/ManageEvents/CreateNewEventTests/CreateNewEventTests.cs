using System;
using System.Threading.Tasks;
using DDDSouthWest.Domain;
using DDDSouthWest.Domain.Features.Account.ManageEvents.CreateNewEvent;
using Shouldly;
using Xunit;
using ValidationException = FluentValidation.ValidationException;

namespace DDDSouthWest.UnitTests.ManageEvents.CreateNewEventTests
{
    public class CreateNewEventTests
    {
        [Fact]
        public async Task Create_new_event()
        {
            var mediator = ResolveContainer.MediatR;

            var result = await mediator.Send(new CreateNewEvent.Command
            {
                EventDate = DateTime.Now,
                EventFilename = Guid.NewGuid().ToString(),
                EventName = "Event name"
            });

            result.Id.ShouldBeGreaterThan(0);
        }
        
        [Fact]
        public async Task Prevent_duplicate_event()
        {
            var mediator = ResolveContainer.MediatR;

            var command = new CreateNewEvent.Command
            {
                EventDate = DateTime.Now,
                EventFilename = Guid.NewGuid().ToString(),
                EventName = "Event name"
            };

            mediator.Send(command).Wait();

            await Should.ThrowAsync<DuplicateRecordException>(mediator.Send(command));
        }

        [Fact]
        public async Task Prevent_incomplete_event_submissions()
        {
            var mediator = ResolveContainer.MediatR;

            var invalidEvent = new CreateNewEvent.Command
            {
                EventDate = DateTime.Now
            };
            
            await Should.ThrowAsync<ValidationException>(mediator.Send(invalidEvent));
        }
    }
}