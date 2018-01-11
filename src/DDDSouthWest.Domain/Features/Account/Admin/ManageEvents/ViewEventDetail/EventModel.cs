using System;

namespace DDDSouthWest.Domain.Features.Account.Admin.ManageEvents.ViewEventDetail
{
    public class EventModel
    {
        public int Id { get; set; }
        public string EventName { get; set; }
        public string EventFilename { get; set; }
        public DateTime EventDate { get; set; }
    }
}