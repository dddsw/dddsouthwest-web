using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using MediatR;
using Npgsql;

namespace DDDSouthWest.Domain.Features.Public.ProposedTalks
{
    public class ProposedTalks
    {
        public class Query : IRequest<Response>
        {
        }

        public class Handler : IRequestHandler<Query, Response>
        {
            private readonly ClientConfigurationOptions _options;

            public Handler(ClientConfigurationOptions options)
            {
                _options = options;
            }

            public async Task<Response> Handle(Query message, CancellationToken cancellationToken)
            {
                using (var connection = new NpgsqlConnection(_options.Database.ConnectionString))
                {
                    const string query =
                        "SELECT t.Id AS TalkId, t.TalkTitle, t.TalkSummary, t.UserId AS SpeakerId, u.GivenName AS SpeakerGivenName, u.FamilyName AS SpeakerFamilyName FROM talks AS t LEFT JOIN users AS u ON u.id = t.userid WHERE t.IsApproved = TRUE AND u.IsActivated = TRUE ORDER By DateAdded DESC";
                    var response = await connection.QueryAsync<ProposedTalksModel>(query);

                    return new Response
                    {
                        ProposedTalks = response.ToList()
                    };
                }
            }
        }

        public class Response
        {
            public IList<ProposedTalksModel> ProposedTalks { get; set; }
        }
    }
}