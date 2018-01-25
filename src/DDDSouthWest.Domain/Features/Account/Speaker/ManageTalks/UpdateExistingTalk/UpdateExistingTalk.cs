using System.Threading.Tasks;
using Dapper;
using FluentValidation;
using MediatR;
using Npgsql;

namespace DDDSouthWest.Domain.Features.Account.Speaker.ManageTalks.UpdateExistingTalk
{
    public class UpdateExistingTalk
    {
        public class Command : IRequest
        {
            public string TalkTitle { get; set; }
            public string TalkSummary { get; set; }
            public string TalkBodyMarkdown { get; set; }
            public string TalkBodyHtml { get; set; }
            public int UserId { get; set; }
        }

        public class Handler : IAsyncRequestHandler<Command>
        {
            private readonly UpdateExistingTalkValidator _validator;
            private readonly ClientConfigurationOptions _options;

            public Handler(UpdateExistingTalkValidator validator, ClientConfigurationOptions options)
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
                        /*Id = message.Id,
                        Title = message.Title,
                        Filename = message.Filename,
                        LastModified = message.LastModified,
                        BodyMarkdown = message.BodyMarkdown,
                        BodyHtml = message.BodyHtml,
                        IsLive = message.IsLive,
                        PageOrder = message.PageOrder*/
                    });
                }
            }
        }
    }
}