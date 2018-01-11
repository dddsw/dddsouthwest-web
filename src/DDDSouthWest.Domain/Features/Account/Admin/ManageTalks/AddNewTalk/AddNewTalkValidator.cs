using FluentValidation;

namespace DDDSouthWest.Domain.Features.Account.Admin.ManageTalks.AddNewTalk
{
    public class AddNewTalkValidator : AbstractValidator<Admin.ManageTalks.AddNewTalk.AddNewTalk.Command>
    {
        public AddNewTalkValidator()
        {
            RuleFor(x => x.TalkTitle).NotEmpty();
            RuleFor(x => x.TalkBody).NotEmpty();
        }
    }
}