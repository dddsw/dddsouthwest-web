using System.Collections.Generic;
using DDDSouthWest.Domain.Features.Account.ManageEvents.ListEvents;

namespace DDDSouthWest.Website.Features.Public.Account.ManageEvents
{
    public class EventListViewModel
    {
        public IList<EventListModel> Events { get; set; }
    }
}