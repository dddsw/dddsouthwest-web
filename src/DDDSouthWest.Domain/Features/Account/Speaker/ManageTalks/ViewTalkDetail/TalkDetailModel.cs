using System;

namespace DDDSouthWest.Domain.Features.Account.Speaker.ManageTalks.ViewTalkDetail
{
    public class TalkDetailModel
    {
        public int Id { get; set; }

        public string TalkTitle { get; set; }

        public string TalkSummary { get; set; }

        public string TalkBodyMarkdown { get; set; }
            
        public string TalkBodyHtml { get; set; }

        public bool IsApproved { get; set; }

        public bool IsSubmitted { get; set; }
        
        public DateTime LastModified { get; set; }
    }
}