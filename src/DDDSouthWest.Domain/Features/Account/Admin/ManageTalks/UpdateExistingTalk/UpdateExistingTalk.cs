using System.Threading;
using System.Threading.Tasks;
using Dapper;
using FluentValidation;
using MediatR;
using Npgsql;

namespace DDDSouthWest.Domain.Features.Account.Admin.ManageTalks.UpdateExistingTalk
{
    public class UpdateExistingTalk
    {
        public class Command : IRequest
        {
            public string TalkTitle { get; set; }
            public string TalkSummary { get; set; }
            public string TalkBodyMarkdown { get; set; }
            public string TalkBodyHtml { get; set; }
            public bool IsApproved { get; set; }
            public int UserId { get; set; }
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly UpdateExistingTalkAsAdminValidator _validator;
            private readonly ClientConfigurationOptions _options;

            public Handler(UpdateExistingTalkAsAdminValidator validator, ClientConfigurationOptions options)
            {
                _validator = validator;
                _options = options;
            }

            public async Task<Unit> Handle(Command message, CancellationToken cancellationToken)
            {
                _validator.ValidateAndThrow(message);

                using (var connection = new NpgsqlConnection(_options.Database.ConnectionString))
                {
                    const string query =
                        @"UPDATE Talks SET TalkTitle = @TalkTitle, TalkBodyHtml = @TalkBodyHtml, TalkBodyMarkdown = @TalkBodyMarkdown, TalkSummary = @TalkSummary, IsApproved = @IsApproved WHERE Id = @Id AND IsSubmitted = TRUE";
                    await connection.ExecuteAsync(query, new
                    {
                        Id = message.Id,
                        TalkTitle = message.TalkTitle,
                        TalkBodyHtml = message.TalkBodyHtml,
                        TalkBodyMarkdown = message.TalkBodyMarkdown,
                        TalkSummary = message.TalkSummary,
                        IsApproved = message.IsApproved
                    });
                }
                
                return Unit.Value;
            }
        }
    }
}