using System.Collections.Generic;
using DDDSouthWest.Domain.Features.Public.ProposedTalks;

namespace DDDSouthWest.Website.Features.Public.ProposedTalks
{
    public class ProposedTalksViewModel
    {
        public ProposedTalksViewModel()
        {
            ProposedTalks = new List<ProposedTalksModel>();    
        }
        
        public IList<ProposedTalksModel> ProposedTalks { get; set; }
    }
}