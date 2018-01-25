using System;

namespace DDDSouthWest.Domain.Features.Account.Speaker.ManageTalks.ListTalks
{
    public class TalkListModel
    {
        public int Id { get; set; }

        public string TalkTitle { get; set; }

        public string TalkFilename { get; set; }

        public string TalkBody { get; set; }

        public bool IsSubmitted { get; set; }

        public bool IsApproved { get; set; }

        public DateTime DateAdded { get; set; }

        public DateTime LastModified { get; set; }
    }
}