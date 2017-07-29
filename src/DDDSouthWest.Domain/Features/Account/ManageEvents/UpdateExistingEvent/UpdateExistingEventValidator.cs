using FluentValidation;

namespace DDDSouthWest.Domain.Features.Account.ManageEvents.UpdateExistingEvent
{
    public class UpdateExistingEventValidator : AbstractValidator<UpdateExistingEvent.Command>
    {
        public UpdateExistingEventValidator()
        {
            RuleFor(x => x.EventName).NotEmpty();
            RuleFor(x => x.EventFilename).NotEmpty();
        }
    }
}