namespace DDDSouthWest.Domain.Features.Public.ProposedTalkDetail
{
    public class ProposedTalkDetailModel
    {
        public int TalkId { get; set; }

        public string TalkTitle { get; set; }

        public string TalkBodyHtml { get; set; }

        public string SpeakerGivenName { get; set; }

        public string SpeakerFamilyName { get; set; }

        public string SpeakerFullName => $"{SpeakerGivenName} {SpeakerFamilyName}";

        public string SpeakerBioHtml { get; set; }

        public int SpeakerId { get; set; }
    }
}