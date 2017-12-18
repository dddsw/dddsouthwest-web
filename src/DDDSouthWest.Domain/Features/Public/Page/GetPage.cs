using System;
using System.Threading.Tasks;
using Dapper;
using DDDSouthWest.Domain.Features.Account.ManageNews.ViewNewsDetail;
using MediatR;
using Npgsql;

namespace DDDSouthWest.Domain.Features.Public.Page
{
    public class GetPage
    {
        public class Query : IRequest<Reponse>
        {
            public string Filename { get; set; }
        }

        public class Handler : IAsyncRequestHandler<Query, Reponse>
        {
            private readonly ClientConfigurationOptions _options;

            public Handler(ClientConfigurationOptions options)
            {
                _options = options;
            }
            
            public Task<Reponse> Handle(Query message)
            {
                using (var connection = new NpgsqlConnection(_options.Database.ConnectionString))
                {
                    return await connection.QuerySingleOrDefaultAsync<PageDetailModel>("SELECT Id, Title, Filename, BodyHtml AS Body, IsLive, LastModified FROM page WHERE Id = @id AND Live = TRUE LIMIT 1", new {id});
                }            }
        }

        public class Reponse
        {
            public string Title { get; set; }

            public string Filename { get; set; }

            public string BodyContent { get; set; }
        }
    }
}