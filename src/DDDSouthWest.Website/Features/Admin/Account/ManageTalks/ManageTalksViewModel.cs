using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;

namespace DDDSouthWest.Website.Features.Admin.Account.ManageTalks
{
    public class ManageTalksViewModel
    {
        public ManageTalksViewModel()
        {
            Errors = new List<ValidationFailure>();
        }

        public int Id { get; set; }

        public string TalkTitle { get; set; }

        public string TalkBodyHtml { get; set; }

        public string SpeakerGivenName { get; set; }

        public string SpeakerFamilyName { get; set; }

        public string SpeakerFullName => $"{SpeakerGivenName} {SpeakerFamilyName}";

        public string TalkBodyMarkdown { get; set; }

        public string TalkSummary { get; set; }
        
        public bool IsApproved { get; set; }
        
        public IList<ValidationFailure> Errors { get; set; }

        public bool HasErrors => Errors.Any();
        
        public bool IsSubmitted { get; set; }
    }
}