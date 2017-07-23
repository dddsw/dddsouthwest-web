using System;
using System.Collections.Generic;
using FluentValidation.Results;

namespace DDDSouthWest.Website.Features.Public.Account.ManageSpeakers
{
    public class ManageSpeakersViewModel
    {
        public ManageSpeakersViewModel()
        {
            Errors = new List<ValidationFailure>();
        }

        public int Id { get; set; }

        public string SpeakerFirstName { get; set; }

        public string SpeakerFamilyName { get; set; }

        public string SpeakerBio { get; set; }
        
        public string WebsiteUrl { get; set; }
        
        public string TwitterHandle { get; set; }

        public DateTime LastModified { get; set; }

        public List<ValidationFailure> Errors { get; set; }
    }
}