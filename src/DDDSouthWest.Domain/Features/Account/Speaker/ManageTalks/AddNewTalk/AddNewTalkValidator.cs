using FluentValidation;

namespace DDDSouthWest.Domain.Features.Account.Speaker.ManageTalks.AddNewTalk
{
    public class AddNewTalkValidator : AbstractValidator<AddNewTalk.Command>
    {
        public AddNewTalkValidator()
        {
            RuleFor(x => x.TalkTitle).NotEmpty();
            RuleFor(x => x.TalkBody).NotEmpty();
        }
    }
}