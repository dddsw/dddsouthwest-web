using System.Threading.Tasks;
using Dapper;
using FluentValidation;
using MediatR;
using Npgsql;

namespace DDDSouthWest.Domain.Features.Account.ManageTalks.AddNewTalk
{
    public class AddNewTalk
    {
        public class Command : IRequest<Response>
        {
            public string TalkTitle { get; set; }
            public string TalkBody { get; set; }
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
                    const string addNewTalkSql = "INSERT INTO Talks (TalkTitle, TalkFilename, SubmissionDate) Values (@TalkTitle, @TalkFilename, current_timestamp) RETURNING Id";
                    return new Response
                    {
                        Id = await connection.QuerySingleAsync<int>(addNewTalkSql, message)
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