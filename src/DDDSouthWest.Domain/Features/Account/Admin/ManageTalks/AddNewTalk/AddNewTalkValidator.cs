using FluentValidation;

namespace DDDSouthWest.Domain.Features.Account.Admin.ManageTalks.AddNewTalk
{
    public class AddNewTalkValidator : AbstractValidator<AddNewTalk.Command>
    {
        public AddNewTalkValidator()
        {
            RuleFor(x => x.TalkTitle).NotEmpty();
            RuleFor(x => x.TalkSummary).NotEmpty();
            RuleFor(x => x.TalkBodyMarkdown).NotEmpty().WithMessage("'Talk Abstract' must not be empty");
        }
    }
}