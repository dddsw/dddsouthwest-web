using FluentValidation;

namespace DDDSouthWest.Domain.Features.Account.ManageProfile.UpdateExistingProfile
{
    public class UpsertSpeakerProfileValidator : AbstractValidator<UpsertSpeakerProfile.Command>
    {
        public UpsertSpeakerProfileValidator()
        {
            RuleFor(x => x.GivenName).NotEmpty();
            RuleFor(x => x.FamilyName).NotEmpty();
            RuleFor(x => x.BioMarkdown).NotEmpty().WithMessage("'Bio' should not be empty");
        }
    }
}