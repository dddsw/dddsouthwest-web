using FluentValidation;

namespace DDDSouthWest.Domain.Features.Account.Speaker.ManageTalks.AddNewTalk
{
    public class AddNewTalkValidator : AbstractValidator<AddNewTalk.Command>
    {
        public AddNewTalkValidator()
        {
            RuleFor(x => x.TalkTitle).NotEmpty();
            RuleFor(x => x.TalkSummary).NotEmpty().WithMessage("'Short Abstract' must not be empty");
            RuleFor(x => x.TalkBodyMarkdown).NotEmpty().WithMessage("'Long' Abstract' must not be empty");
        }
    }
}