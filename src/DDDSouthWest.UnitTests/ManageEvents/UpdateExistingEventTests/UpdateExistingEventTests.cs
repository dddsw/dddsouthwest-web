using System;
using System.Threading.Tasks;
using DDDSouthWest.Domain;
using DDDSouthWest.Domain.Features.Account.ManageEvents.CreateNewEvent;
using DDDSouthWest.Domain.Features.Account.ManageEvents.UpdateExistingEvent;
using DDDSouthWest.Domain.Features.Account.ManageEvents.ViewEventDetail;
using FluentValidation;
using Shouldly;
using Xunit;

namespace DDDSouthWest.UnitTests.ManageEvents.UpdateExistingEventTests
{
    public class UpdateExistingEventTests
    {
        [Fact]
        public async Task Update_an_existing_event()
        {
            // Arrange
            var mediator = ResolveContainer.MediatR;

            var staticEventDate = DateTime.Now;
            var response = await mediator.Send(new CreateNewEvent.Command
            {
                EventDate = staticEventDate,
                EventFilename = Guid.NewGuid().ToString(),
                EventName = "Before event name"
            });

            // Act
            var afterFilename = Guid.NewGuid().ToString();
            await mediator.Send(new UpdateExistingEvent.Command
            {
                Id = response.Id,
                EventFilename = afterFilename,
                EventName = "After event name",
                EventDate = staticEventDate
            });
            
            // Assert
            var result = await mediator.Send(new ViewEventDetail.Query
            {
                Id = response.Id
            });

            result.EventName.ShouldBe("After event name");
            result.EventFilename.ShouldBe(afterFilename);
            /*
            TODO: Can't update event yet?
            result.EventDate.ShouldBe(staticEventDate);*/
        }
        
        public void Prevent_duplicate_event()
        {
        }

        public void Prevent_incomplete_event_submissions()
        {
        }
    }
}