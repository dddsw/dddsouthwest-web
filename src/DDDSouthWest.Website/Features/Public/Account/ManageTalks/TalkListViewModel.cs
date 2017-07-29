using System.Collections.Generic;
using DDDSouthWest.Domain.Features.Account.ManageEvents.ListEvents;

namespace DDDSouthWest.Website.Features.Public.Account.ManageTalks
{
    public class TalkListViewModel
    {
        public IList<EventListModel> Talks { get; set; }
    }
}