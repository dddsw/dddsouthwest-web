namespace DDDSouthWest.Website.Features.Public.Account.ManageTalks
{
    public class TalkDetailViewModel
    {
        public int Id { get; set; }

        public string TalkTitle { get; set; }

        public string TalkSummary { get; set; }

        public string TalkBodyMarkdown { get; set; }

        public string TalkBodyHtml { get; set; }
        
        public bool IsSubmitted { get; set; }
        
        public bool IsApproved { get; set; }
    }
}