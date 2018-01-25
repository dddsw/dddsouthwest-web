using FluentValidation;

namespace DDDSouthWest.Domain.Features.Account.Admin.ManageProfile.UpdateExistingProfile
{
    public class UpsertSpeakerProfileValidator : AbstractValidator<UpsertSpeakerProfile.Command>
    {
        public UpsertSpeakerProfileValidator()
        {
            RuleFor(x => x.GivenName).NotEmpty();
            RuleFor(x => x.FamilyName).NotEmpty();
        }
    }
}