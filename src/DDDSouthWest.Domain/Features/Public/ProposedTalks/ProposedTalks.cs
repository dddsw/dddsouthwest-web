using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using MediatR;
using Npgsql;

namespace DDDSouthWest.Domain.Features.Public.ProposedTalks
{
    public class ProposedTalks
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
                    var response = await connection.QueryAsync<ProposedTalksModel>("SELECT Id, TalkTitle, TalkSummary FROM talks ORDER BY random()");

                    return new Response
                    {
                        ProposedTalks = response.ToList()
                    };
                }
            }
        }

        public class Response
        {
            public IList<ProposedTalksModel> ProposedTalks { get; set; }
        }
    }
}