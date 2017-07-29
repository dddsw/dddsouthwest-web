using DDDSouthWest.Domain.Features.Account.ManageTalks;
using DDDSouthWest.Domain.Features.Account.ManageTalks.AddNewTalk;
using FluentValidation;

namespace DDDSouthWest.Domain.Features.Account.ManageEvents.CreateNewEvent
{
    public class CreateEventValidator : AbstractValidator<AddNewTalk.Command>
    {
        public CreateEventValidator()
        {
            RuleFor(x => x.TalkTitle).NotEmpty();
            RuleFor(x => x.TalkBody).NotEmpty();
        }
    }
}