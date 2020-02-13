using System.Threading.Tasks;
using Dapper;
using MediatR;
using Npgsql;

namespace DDDSouthWest.Domain.Features.Public.ProposedTalkDetail
{
    public class ProposedTalkDetail
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
                using var connection = new NpgsqlConnection(_options.Database.ConnectionString);
                const string query =
                    "SELECT t.Id AS TalkId, t.TalkTitle, t.TalkBodyHtml, t.UserId AS SpeakerId, u.GivenName AS SpeakerGivenName, u.FamilyName AS SpeakerFamilyName, p.BioHtml AS SpeakerBioHtml FROM talks AS t LEFT JOIN users AS u ON u.id = t.userid LEFT JOIN Profiles AS p ON p.UserId = t.userid WHERE t.IsApproved = TRUE AND u.IsActivated = TRUE AND t.Id = @Id";
                var response = await connection.QuerySingleOrDefaultAsync<ProposedTalkDetailModel>(query, new
                {
                    Id = message.Id
                });

                return new Response
                {
                    ProposedTalkDetail = response
                };
            }
        }

        public class Response
        {
            public ProposedTalkDetailModel ProposedTalkDetail { get; set; }
        }
    }
}