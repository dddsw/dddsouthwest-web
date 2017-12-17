using System;
using System.Collections.Generic;
using FluentValidation.Results;

namespace DDDSouthWest.Website.Features.Public.Account.ManagePages
{
    public class ManagePagesViewModel
    {
        public ManagePagesViewModel()
        {
            Errors = new List<ValidationFailure>();
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public string Filename { get; set; }

        public string BodyMarkdown { get; set; }

        public DateTime LastModified { get; set; }

        public List<ValidationFailure> Errors { get; set; }
    }
}