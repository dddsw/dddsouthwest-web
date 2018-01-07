using System.Collections.Generic;
using DDDSouthWest.Domain.Features.Account.ManageProfile.ViewProfile;
using FluentValidation.Results;

namespace DDDSouthWest.Website.Features.Public.Account.ManageProfile
{
    public class ProfileEditViewModel
    {
        public ProfileDetailModel Profile { get; set; }

        public List<ValidationFailure> Errors { get; set; }
        
        public ProfileEditViewModel()
        {
            Errors = new List<ValidationFailure>();
        }
    }
}