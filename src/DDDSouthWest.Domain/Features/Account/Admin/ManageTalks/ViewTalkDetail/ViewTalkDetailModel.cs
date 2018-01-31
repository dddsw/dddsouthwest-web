namespace DDDSouthWest.Domain.Features.Account.Admin.ManageTalks.ViewTalkDetail
{
    public class ViewTalkDetailModel
    {
        public int Id { get; set; }

        public string TalkTitle { get; set; }

        public string TalkBodyHtml { get; set; }
        
        public string TalkBodyMarkdown { get; set; }

        public string TalkSummary { get; set; }

        public string SpeakerGivenName { get; set; }

        public string SpeakerFamilyName { get; set; }

        public string SpeakerFullName => $"{SpeakerGivenName} {SpeakerFamilyName}";

        public bool IsApproved { get; set; }

        public bool IsSubmitted { get; set; }
    }
}