using System.Collections.Generic;
using DDDSouthWest.Domain.Features.Account.Admin.ManageTalks.ListTalks;

namespace DDDSouthWest.Website.Features.Admin.Account.ManageTalks
{
    public class TalkListViewModel
    {
        public IList<TalkListModel> Talks { get; set; }
    }
}