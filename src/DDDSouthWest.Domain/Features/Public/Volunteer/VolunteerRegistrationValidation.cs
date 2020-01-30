using FluentValidation;

namespace DDDSouthWest.Domain.Features.Public.Volunteer
{
    public class VolunteerRegistrationValidation : AbstractValidator<VolunteerRegistration.Command>
    {
        public VolunteerRegistrationValidation()
        {
            RuleFor(x => x.FullName).NotEmpty();
            RuleFor(x => x.EmailAddress).NotEmpty();
            RuleFor(x => x.PhoneNumber).NotEmpty();
        }
    }
}