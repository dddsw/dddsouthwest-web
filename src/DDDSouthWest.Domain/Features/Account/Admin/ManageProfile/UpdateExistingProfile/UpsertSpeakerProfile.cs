using System;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;

namespace DDDSouthWest.Domain.Features.Account.Admin.ManageProfile.UpdateExistingProfile
{
    public class UpsertSpeakerProfile
    {
        public class Command : IRequest
        {
            public int Id { get; set; }
            public string GivenName { get; set; }
            public string FamilyName { get; set; }
            public string Twitter { get; set; }
            public string Website { get; set; }
            public string LinkedIn { get; set; }
            public string Bio { get; set; }
            public string BioMarkdown { get; set; }
            public string BioHtml { get; set; }
            public DateTime LastModified { get; set; }
        }

        public class Handler : IAsyncRequestHandler<Command>
        {
            private readonly UpsertSpeakerProfileValidator _validator;
            private readonly UpsertSpeakerProfileQuery _upsertSpeakerProfileQuery;
            private readonly ClientConfigurationOptions _options;

            public Handler(UpsertSpeakerProfileValidator validator, UpsertSpeakerProfileQuery upsertSpeakerProfileQuery, ClientConfigurationOptions options)
            {
                _validator = validator;
                _upsertSpeakerProfileQuery = upsertSpeakerProfileQuery;
                _options = options;
            }

            public async Task Handle(Command message)
            {
                _validator.ValidateAndThrow(message);
                await _upsertSpeakerProfileQuery.Invoke(message);
            }
        }
    }
}