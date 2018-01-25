using System.Collections.Generic;
using DDDSouthWest.Domain.Features.Public.ProposedTalks;

namespace DDDSouthWest.Website.Features.Public.ProposedTalks
{
    public class ProposedTalksViewModel
    {
        public ProposedTalksViewModel()
        {
            ProposedTalk = new List<ProposedTalksModel>();    
        }
        
        public IList<ProposedTalksModel> ProposedTalk { get; set; }
    }
}