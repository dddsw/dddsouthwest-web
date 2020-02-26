using System.Collections.Generic;
using System.Linq;
using DDDSouthWest.Domain.Features.Account.Admin.ManageTalks.ListTalks;

namespace DDDSouthWest.Website.Features.Admin.Account.ManageTalks
{
    public class ListViewModel
    {
        public IList<TalkListModel> Talks { get; set; }

        public int TotalTalks
            => Talks.Count;

        public int UniqueSpeakers
            => Talks
                .GroupBy(x => x.SpeakerId)
                .Select(x => x.First())
                .ToList().Count;
    }
}