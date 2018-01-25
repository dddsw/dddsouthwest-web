using System;

namespace DDDSouthWest.Domain.Features.Account.Admin.ManageTalks.ListTalks
{
    public class TalkListModel
    {
        public int Id { get; set; }

        public string TalkTitle { get; set; }

        public string TalkSummary { get; set; }

        public DateTime DateAdded { get; set; }

        public DateTime LastModified { get; set; }

        public bool SubmissionDate { get; set; }

        public bool IsApproved { get; set; }

        public bool IsSubmitted { get; set; }
    }
}