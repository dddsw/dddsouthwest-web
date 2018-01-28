using FluentValidation;

namespace DDDSouthWest.Domain.Features.Account.Speaker.ManageTalks.UpdateExistingTalk
{
    public class UpdateExistingTalkValidator : AbstractValidator<UpdateExistingTalk.Command>
    {
        public UpdateExistingTalkValidator()
        {
            RuleFor(x => x.TalkTitle).NotEmpty();
            RuleFor(x => x.TalkSummary).NotEmpty();
            RuleFor(x => x.TalkBodyMarkdown).NotEmpty().WithMessage("'Talk Body' must not be empty");
        }
    }
}