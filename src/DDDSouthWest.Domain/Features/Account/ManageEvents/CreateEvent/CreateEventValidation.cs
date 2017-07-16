using FluentValidation;

namespace DDDSouthWest.Domain.Features.Account.ManageEvents.CreateEvent
{
    public class CreateEventValidation : AbstractValidator<CreateEvent.Command>
    {
        public CreateEventValidation()
        {
            RuleFor(x => x.EventName).NotEmpty();
            RuleFor(x => x.EventFilename).NotEmpty();
        }
    }
}