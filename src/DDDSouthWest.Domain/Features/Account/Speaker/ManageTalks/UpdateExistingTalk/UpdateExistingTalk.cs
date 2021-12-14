using System.Threading;
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
            public bool IsSubmitted { get; set; }
            public int UserId { get; set; }
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly UpdateExistingTalkValidator _validator;
            private readonly ClientConfigurationOptions _options;

            public Handler(UpdateExistingTalkValidator validator, ClientConfigurationOptions options)
            {
                _validator = validator;
                _options = options;
            }

            public async Task<Unit> Handle(Command message, CancellationToken cancellationToken)
            {
                _validator.ValidateAndThrow(message);

                using (var connection = new NpgsqlConnection(_options.Database.ConnectionString))
                {
                    const string query = @"UPDATE Talks SET TalkTitle = @TalkTitle, TalkBodyHtml = @TalkBodyHtml, TalkBodyMarkdown = @TalkBodyMarkdown, TalkSummary = @TalkSummary, IsSubmitted = @IsSubmitted, LastModified = current_timestamp WHERE Id = @Id AND UserId = @UserId";
                    await connection.ExecuteAsync(query, new
                    {
                        Id = message.Id,
                        TalkTitle = message.TalkTitle,
                        TalkBodyHtml = message.TalkBodyHtml,
                        TalkBodyMarkdown = message.TalkBodyMarkdown,
                        TalkSummary = message.TalkSummary,
                        IsSubmitted = message.IsSubmitted
                    });
                }
                
                return Unit.Value;
            }
        }
    }
}