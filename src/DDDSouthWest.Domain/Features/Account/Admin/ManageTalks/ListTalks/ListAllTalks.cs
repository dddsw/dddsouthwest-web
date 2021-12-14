using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using MediatR;
using Npgsql;

namespace DDDSouthWest.Domain.Features.Account.Admin.ManageTalks.ListTalks
{
    public class ListAllTalks
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
                    const string sql = "SELECT t.Id, t.TalkTitle, t.TalkSummary, t.DateAdded, t.LastModified, t.SubmissionDate, t.IsApproved, t.IsSubmitted, u.Id AS SpeakerId, u.GivenName AS SpeakerGivenName, u.FamilyName AS SpeakerFamilyName FROM Talks t LEFT JOIN Users u ON t.UserId = u.Id ORDER By DateAdded DESC";
                    var talks = await connection.QueryAsync<TalkListModel>(sql);

                    return new Response
                    {
                        Talks = talks.ToList()
                    };
                }
            }
        }

        public class Response
        {
            public IList<TalkListModel> Talks { get; set; }
        }
    }
}