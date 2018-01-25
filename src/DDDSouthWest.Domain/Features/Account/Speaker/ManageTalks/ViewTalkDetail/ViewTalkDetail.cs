using System.Threading.Tasks;
using Dapper;
using DDDSouthWest.Domain.Features.Account.Admin.ManagePages.ViewPageDetail;
using MediatR;
using Npgsql;

namespace DDDSouthWest.Domain.Features.Account.Speaker.ManageTalks.ViewTalkDetail
{
    public class ViewTalkDetail
    {
        public class Query : IRequest<TakeDetailModel>
        {
            public int Id { get; set; }
        }

        public class Handler : IAsyncRequestHandler<Query, TakeDetailModel>
        {
            private readonly ClientConfigurationOptions _options;

            public Handler(ClientConfigurationOptions options)
            {
                _options = options;
            }

            public async Task<TakeDetailModel> Handle(Query message)
            {
                using (var connection = new NpgsqlConnection(_options.Database.ConnectionString))
                {
                    var response = await connection.QuerySingleOrDefaultAsync<TakeDetailModel>(
                        "SELECT Id, TitleTalk, TalkSummary, TalkBodyHtml, TalkBodyMarkdown, IsLive FROM pages WHERE Id = @Id LIMIT 1",
                        new
                        {
                            Id = message.Id
                        });

                    return response;
                }                
            }
        }
    }
}