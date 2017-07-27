using FluentValidation;

namespace DDDSouthWest.Domain.Features.Account.ManageEvents.CreateNewEvent
{
    public class CreateEventValidation : AbstractValidator<CreateNewEvent.Command>
    {
        public CreateEventValidation()
        {
            RuleFor(x => x.EventName).NotEmpty();
            RuleFor(x => x.EventFilename).NotEmpty();
        }
    }
}