using FluentValidation;

namespace DDDSouthWest.Domain.Features.Account.Speaker.ManageTalks.UpdateExistingTalk
{
    public class UpdateExistingTalkValidator : AbstractValidator<UpdateExistingTalk.Command>
    {
        public UpdateExistingTalkValidator()
        {
            RuleFor(x => x.TalkTitle).NotEmpty();
            RuleFor(x => x.TalkSummary).NotEmpty().WithMessage("'Short Abstract' must not be empty");
            RuleFor(x => x.TalkBodyMarkdown).NotEmpty().WithMessage("'Long' Abstract' must not be empty");
        }
    }
}