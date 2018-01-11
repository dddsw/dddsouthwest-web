using System.Collections.Generic;
using DDDSouthWest.Domain.Features.Account.ManageEvents.ListEvents;

namespace DDDSouthWest.Website.Features.Admin.Account.ManageEvents
{
    public class EventListViewModel
    {
        public IList<EventListModel> Events { get; set; }
    }
}