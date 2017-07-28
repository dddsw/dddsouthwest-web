using FluentValidation;

namespace DDDSouthWest.Domain.Features.Account.ManageEvents.UpdateExistingEvent
{
    public class UpdateExistingEventValidation : AbstractValidator<UpdateExistingEvent.Command>
    {
        public UpdateExistingEventValidation()
        {
            RuleFor(x => x.EventName).NotEmpty();
            RuleFor(x => x.EventFilename).NotEmpty();
        }
    }
}