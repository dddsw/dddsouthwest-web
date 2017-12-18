using System.Threading.Tasks;
using Dapper;
using MediatR;
using Npgsql;

namespace DDDSouthWest.Domain.Features.Public.Page
{
    public class GetPage
    {
        public class Query : IRequest<Response>
        {
            public string Filename { get; set; }
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
                    var response = await connection.QuerySingleOrDefaultAsync<PageDetailModel>(
                        "SELECT Id, Title, BodyHtml, IsLive, LastModified FROM pages WHERE Filename = @filename AND IsLive = TRUE LIMIT 1",
                        new
                        {
                            filename = message.Filename
                        });
                    
                    if (response == null)
                        throw new RecordNotFoundException("Page not found");

                    return new Response
                    {
                        PageDetail = response
                    };
                }
            }
        }

        public class Response
        {
            public PageDetailModel PageDetail;
        }
    }
}