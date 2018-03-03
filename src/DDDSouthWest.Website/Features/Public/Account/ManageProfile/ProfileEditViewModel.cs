using System.Collections.Generic;
using FluentValidation.Results;

namespace DDDSouthWest.Website.Features.Public.Account.ManageProfile
{
    public class ProfileEditViewModel
    {
        public int Id { get; set; }
 
        public string GivenName { get; set; }

        public string FamilyName { get; set; }

        public string Twitter { get; set; }

        public string Website { get; set; }

        public string LinkedIn { get; set; }

        public string BioMarkdown { get; set; }
        
        public string BioHtml { get; set; }

        public IList<ValidationFailure> Errors { get; set; }
        
        public bool HasProfile { get; set; }
        
        public string ProfilePicturePath { get; set; }

        public ProfileEditViewModel()
        {
            Errors = new List<ValidationFailure>();
        }
    }
}