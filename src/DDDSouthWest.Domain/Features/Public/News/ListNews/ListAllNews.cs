using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using MediatR;
using Npgsql;

namespace DDDSouthWest.Domain.Features.Public.News.ListNews
{
    public class ListAllNews
    {
        public class QueryByLimit : IRequest<Response>
        {
            public int? Limit { get; }

            public QueryByLimit()
            {
            }

            public QueryByLimit(int limit)
            {
                Limit = limit;
            }
        }

        public class Handler : IRequestHandler<QueryByLimit, Response>
        {
            private readonly ClientConfigurationOptions _options;

            public Handler(ClientConfigurationOptions options)
            {
                _options = options;
            }

            public async Task<Response> Handle(QueryByLimit message, CancellationToken cancellationToken)
            {
                using (var connection = new NpgsqlConnection(_options.Database.ConnectionString))
                {
                    var sql = "SELECT Id, Title, Filename, BodyHtml AS Body, IsLive, DatePosted FROM news WHERE IsLive = TRUE ORDER BY DatePosted DESC";
                    if (message.Limit.HasValue && message.Limit.Value > 0)
                        sql += " LIMIT @Count";
                    var results = await connection.QueryAsync<NewsListModel>(sql, new
                    {
                        Count = message.Limit
                    });

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