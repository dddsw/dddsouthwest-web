using System.Threading.Tasks;
using Dapper;
using MediatR;
using Npgsql;

namespace DDDSouthWest.Domain.Features.Account.Admin.ManageTalks.ViewTalkDetail
{
    public class ViewTalkDetail
    {
        public class Query : IRequest<Response>
        {
            public readonly int Id;

            public Query(int id)
            {
                Id = id;
            }
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
                    const string query =
                        "SELECT t.Id, t.TalkTitle, t.TalkBodyHtml, t.TalkBodyMarkdown, t.TalkSummary, t.IsApproved, t.IsSubmitted, u.GivenName AS SpeakerGivenName, u.FamilyName AS SpeakerFamilyName FROM talks AS t LEFT JOIN users AS u ON u.Id = t.UserId LEFT JOIN Profiles AS p ON p.UserId = t.userid WHERE t.Id = @Id";
                    var response = await connection.QuerySingleOrDefaultAsync<ViewTalkDetailModel>(query, new
                    {
                        Id = message.Id
                    });

                    return new Response
                    {
                        ViewTalkDetailModel = response
                    };
                }
            }
        }

        public class Response
        {
            public ViewTalkDetailModel ViewTalkDetailModel { get; set; }
        }
    }
}