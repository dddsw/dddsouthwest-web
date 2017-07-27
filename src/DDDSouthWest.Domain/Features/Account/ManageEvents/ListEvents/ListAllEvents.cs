using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using MediatR;
using Npgsql;

namespace DDDSouthWest.Domain.Features.Account.ManageEvents.ListEvents
{
    public class ListAllEvents
    {
        public class Query : IRequest<Response>
        {
            public int Id { get; set; }
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
                    const string sql = "SELECT Id, EventName, EventFilename FROM events";
                    var events = await connection.QueryAsync<EventListModel>(sql);

                    return new Response
                    {
                        Events = events.ToList()
                    };
                }
            }
        }

        public class Response
        {
            public IList<EventListModel> Events { get; set; }
        }
    }
}