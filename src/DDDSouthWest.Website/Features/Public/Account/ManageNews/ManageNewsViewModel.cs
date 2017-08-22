using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;

namespace DDDSouthWest.Website.Features.Public.Account.ManageNews
{
    public class ManageNewsViewModel
    {
        public ManageNewsViewModel()
        {
            Errors = new List<ValidationFailure>();
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public string Filename { get; set; }

        public DateTime DatePosted { get; set; }

        public string Body { get; set; }

        public bool IsLive { get; set; }

        public IList<ValidationFailure> Errors { get; set; }

        public bool HasErrors => Errors.Any();
    }
}