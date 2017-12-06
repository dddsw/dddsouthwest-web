using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using MediatR;
using Npgsql;

namespace DDDSouthWest.Domain.Features.Account.ManageNews.ListNews
{
    public class ListAllNews
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
                    const string sql = "SELECT Id, Title, Filename, IsLive, DatePosted FROM news WHERE IsDeleted = FALSE ORDER BY DatePosted";
                    var results = await connection.QueryAsync<NewsListModel>(sql);

                    return new Response
                    {
                        News = results.ToList()
                    };
                }
            }
        }

        public class Response
        {
            public IList<NewsListModel> News { get; set; }
        }
    }
}