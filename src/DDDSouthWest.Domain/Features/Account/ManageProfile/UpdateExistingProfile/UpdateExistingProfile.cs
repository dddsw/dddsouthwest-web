using System;
using System.Threading.Tasks;
using Dapper;
using FluentValidation;
using MediatR;
using Npgsql;

namespace DDDSouthWest.Domain.Features.Account.ManageProfile.UpdateExistingProfile
{
    public class UpdateExistingProfile
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
            public string BodyMarkdown { get; set; }
            public string BodyHtml { get; set; }
            public DateTime LastModified { get; set; }
        }

        public class Handler : IAsyncRequestHandler<Command>
        {
            private readonly UpdateExistingProfileValidator _validator;
            private readonly ClientConfigurationOptions _options;

            public Handler(UpdateExistingProfileValidator validator, ClientConfigurationOptions options)
            {
                _validator = validator;
                _options = options;
            }

            public async Task Handle(Command message)
            {
                _validator.ValidateAndThrow(message);

                using (var connection = new NpgsqlConnection(_options.Database.ConnectionString))
                {
                    const string query = @"UPDATE pages SET Title = @Title, Filename = @Filename, BodyMarkdown = @BodyMarkdown, BodyHtml = @BodyHtml, LastModified = current_timestamp, IsLive = @IsLive, PageOrder = @PageOrder WHERE Id = @Id";
                    await connection.ExecuteAsync(query, new
                    {
                        Id = message.Id,
                        LastModified = message.LastModified,
                        BodyMarkdown = message.BodyMarkdown,
                        BodyHtml = message.BodyHtml
                    });
                }
            }
        }
    }
}