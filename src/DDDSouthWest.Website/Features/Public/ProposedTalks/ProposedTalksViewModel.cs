using System.Collections.Generic;
using DDDSouthWest.Domain.Features.Public.ProposedTalks;

namespace DDDSouthWest.Website.Features.Public.ProposedTalks
{
    public class ProposedTalksViewModel
    {
        public ProposedTalksViewModel()
        {
            ProposedTalks = new List<ProposedTalkViewModel>();    
        }
        
        public IList<ProposedTalkViewModel> ProposedTalks { get; set; }
    }

    public class ProposedTalkViewModel
    {
        public int TalkId { get; set; }

        public string TalkTitle { get; set; }

        public string TalkSummary { get; set; }

        public string SpeakerGivenName { get; set; }

        public string SpeakerFamilyName { get; set; }

        public int SpeakerId { get; set; }

        public string ProfileImage { get; set; }
        
        public bool ProfileImageExists { get; set; }
    }
}