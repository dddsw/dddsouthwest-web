using Dapper;
using MediatR;
using Npgsql;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DDDSouthWest.Domain.Features.Public.MeetTheTeam
{
    public class Team
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
                        "SELECT " +
                        "t.Id AS MemberId, " +
                        "t.FullName as FullName, " +
                        "t.EmailAddress as EmailAddress, " +
                        "t.PicturePath as PicturePath, " +
                        "t.YearJoined as YearJoined, " +
                        "t.Twitter as Twitter " +
                        "FROM team t";

                    var response = await connection.QueryAsync<TeamMemberModel>(query);

                    return new Response
                    {
                        TeamMembers = response.ToList()
                    };
                }
            }
        }

        public class Response
        {
            public IList<TeamMemberModel> TeamMembers { get; set; }
        }

    }
}