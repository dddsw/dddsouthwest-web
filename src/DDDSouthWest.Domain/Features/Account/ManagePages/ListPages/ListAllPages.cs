using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using MediatR;
using Npgsql;

namespace DDDSouthWest.Domain.Features.Account.ManagePages.ListPages
{
    public class ListAllPages
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
                    const string sql =
                        "SELECT Id, Title, IsLive, LastModified FROM pages WHERE IsDeleted = FALSE ORDER BY PageOrder";
                    var results = await connection.QueryAsync<PageListModel>(sql);

                    return new Response
                    {
                        Pages = results.ToList()
                    };
                }
            }
        }

        public class Response
        {
            public IList<PageListModel> Pages { get; set; }
        }
    }
}