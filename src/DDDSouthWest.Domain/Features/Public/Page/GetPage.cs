using System.Threading;
using System.Threading.Tasks;
using Dapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace DDDSouthWest.Domain.Features.Public.Page
{
    public class GetPage
    {
        public class Query : IRequest<Response>
        {
            public string Filename { get; set; }
        }

        public class Handler : IRequestHandler<Query, Response>
        {
            private readonly ClientConfigurationOptions _options;
            private readonly ILogger _logger;

            public Handler(ClientConfigurationOptions options, ILogger<GetPage> logger)
            {
                _options = options;
                _logger = logger;
            }

            public async Task<Response> Handle(Query message, CancellationToken cancellationToken)
            {
                _logger.LogInformation("Getting page {filename}", message.Filename);
                using (var connection = new NpgsqlConnection(_options.Database.ConnectionString))
                {
                    var response = await connection.QuerySingleOrDefaultAsync<PageDetailModel>(
                        "SELECT Id, Title, BodyHtml, IsLive, LastModified FROM pages WHERE Filename = @filename AND IsLive = TRUE LIMIT 1",
                        new
                        {
                            filename = message.Filename
                        });

                    if (response == null)
                    {
                        _logger.LogInformation("Page {filename} not found", message.Filename);
                        throw new RecordNotFoundException("Page not found");
                    }

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