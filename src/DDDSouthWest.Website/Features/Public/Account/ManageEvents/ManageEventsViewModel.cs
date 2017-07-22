using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;

namespace DDDSouthWest.Website.Features.Public.Account.ManageEvents
{
    public class ManageEventsViewModel
    {
        public ManageEventsViewModel()
        {
            Errors = new List<ValidationFailure>();
        }
        
        public int Id { get; set; }
        
        public string EventName { get; set; }
        
        public string EventFilename { get; set; }
        
        public DateTime EventDate { get; set; }
        
        public IList<ValidationFailure> Errors { get; set; }

        public bool HasErrors => Errors.Any();
    }
}