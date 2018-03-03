namespace DDDSouthWest.Domain.Features.Public.ProposedTalks
{
    public class ProposedTalksModel
    {
        public int TalkId { get; set; }

        public string TalkTitle { get; set; }

        public string TalkSummary { get; set; }

        public string SpeakerGivenName { get; set; }

        public string SpeakerFamilyName { get; set; }

        public int SpeakerId { get; set; }
    }
}