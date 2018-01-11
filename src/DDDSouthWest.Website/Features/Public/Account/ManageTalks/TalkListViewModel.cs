using System.Collections.Generic;
using DDDSouthWest.Domain.Features.Account.Speaker.ManageTalks.ListTalks;

namespace DDDSouthWest.Website.Features.Public.Account.ManageTalks
{
    public class TalkListViewModel
    {
        public IList<TalkListModel> Talks { get; set; }
    }
}