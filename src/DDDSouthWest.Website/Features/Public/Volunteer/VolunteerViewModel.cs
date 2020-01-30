using System.Collections.Generic;
using FluentValidation.Results;

namespace DDDSouthWest.Website.Features.Public.Volunteer
{
    public class VolunteerViewModel
    {
        public string FullName { get; set; }

        public string EmailAddress { get; set; }

        public string PhoneNumber { get; set; }

        public bool HelpSetup { get; set; }

        public IList<ValidationFailure> Errors { get; set; } = new List<ValidationFailure>();
    }
}