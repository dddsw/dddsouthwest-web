using System.Threading.Tasks;
using Dapper;
using FluentValidation;
using MediatR;
using Npgsql;

namespace DDDSouthWest.Domain.Features.Account.Speaker.ManageTalks.AddNewTalk
{
    public class AddNewTalk
    {
        public class Command : IRequest<Response>
        {
            public string TalkTitle { get; set; }
            public string TalkSummary { get; set; }
            public string TalkBodyMarkdown { get; set; }
            public string TalkBodyHtml { get; set; }
            public bool IsSubmitted { get; set; }
            public int UserId { get; set; }
        }

        public class Handler : IAsyncRequestHandler<Command, Response>
        {
            private readonly AddNewTalkValidator _validator;
            private readonly ClientConfigurationOptions _options;

            public Handler(AddNewTalkValidator validator, ClientConfigurationOptions options)
            {
                _validator = validator;
                _options = options;
            }

            public async Task<Response> Handle(Command message)
            {
                _validator.ValidateAndThrow(message);

                using (var connection = new NpgsqlConnection(_options.Database.ConnectionString))
                {
                    const string addNewTalkSql = "INSERT INTO Talks (TalkTitle, TalkBodyHtml, TalkBodyMarkdown, TalkSummary, DateAdded, LastModified, UserId, IsSubmitted) Values (@TalkTitle, @TalkBodyHtml, @TalkBodyMarkdown, @TalkSummary, current_timestamp, current_timestamp, @UserId, @IsSubmitted) RETURNING Id";
                    return new Response
                    {
                        Id = await connection.QuerySingleAsync<int>(addNewTalkSql, new
                        {
                            TalkTitle = message.TalkTitle,
                            TalkSummary = message.TalkSummary,
                            TalkBodyHtml = message.TalkBodyHtml,
                            TalkBodyMarkdown = message.TalkBodyMarkdown,
                            UserId = message.UserId,
                            IsSubmitted = message.IsSubmitted
                        })
                    };
                }                
            }
        }

        public class Response
        {
            public int Id { get; set; }
        }
    }
}