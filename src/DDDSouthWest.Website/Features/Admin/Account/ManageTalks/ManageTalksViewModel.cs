using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;

namespace DDDSouthWest.Website.Features.Admin.Account.ManageTalks
{
    public class ManageTalksViewModel
    {
        public ManageTalksViewModel()
        {
            Errors = new List<ValidationFailure>();
        }

        public int Id { get; set; }

        public string TalkTitle { get; set; }

        public string TalkFilename { get; set; }

        public string TalkSummary { get; set; }

        public string TalkBody { get; set; }

        public IList<ValidationFailure> Errors { get; set; }

        public bool HasErrors => Errors.Any();
    }
}