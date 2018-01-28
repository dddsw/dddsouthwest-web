using System.Threading.Tasks;
using Dapper;
using DDDSouthWest.Domain.Features.Account.Admin.ManagePages.ViewPageDetail;
using MediatR;
using Npgsql;

namespace DDDSouthWest.Domain.Features.Account.Speaker.ManageTalks.ViewTalkDetail
{
    public class ViewTalkDetail
    {
        public class Query : IRequest<TalkDetailModel>
        {
            public int Id { get; set; }
            
            public int UserId { get; set; }
        }

        public class Handler : IAsyncRequestHandler<Query, TalkDetailModel>
        {
            private readonly ClientConfigurationOptions _options;

            public Handler(ClientConfigurationOptions options)
            {
                _options = options;
            }

            public async Task<TalkDetailModel> Handle(Query message)
            {
                using (var connection = new NpgsqlConnection(_options.Database.ConnectionString))
                {
                    var response = await connection.QuerySingleOrDefaultAsync<TalkDetailModel>(
                        "SELECT Id, TalkTitle, TalkSummary, TalkBodyHtml, TalkBodyMarkdown, IsApproved, IsSubmitted, UserId FROM talks WHERE Id = @Id AND UserId = @UserId LIMIT 1",
                        new
                        {
                            Id = message.Id,
                            UserId = message.UserId
                        });

                    return response;
                }                
            }
        }
    }
}