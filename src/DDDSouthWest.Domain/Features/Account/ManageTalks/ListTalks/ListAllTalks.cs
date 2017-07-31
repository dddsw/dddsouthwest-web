using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using MediatR;
using Npgsql;

namespace DDDSouthWest.Domain.Features.Account.ManageTalks.ListTalks
{
    public class ListAllTalks
    {
        public class Query : IRequest<Response>
        {
        }

        public class Handler : IAsyncRequestHandler<Query, Response>
        {
            private readonly ClientConfigurationOptions _options;

            public Handler(ClientConfigurationOptions options)
            {
                _options = options;
            }

            public async Task<Response> Handle(Query message)
            {
                using (var connection = new NpgsqlConnection(_options.Database.ConnectionString))
                {
                    const string sql = "SELECT Id, TalkTitle, TalkFilename, SubmissionDate FROM Talks";
                    var talks = await connection.QueryAsync<TalkListModel>(sql);

                    return new Response
                    {
                        Talks = talks.ToList()
                    };
                }
            }
        }

        public class Response
        {
            public IList<TalkListModel> Talks { get; set; }
        }
    }
}