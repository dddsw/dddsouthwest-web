using System.Threading;
using System.Threading.Tasks;
using Dapper;
using MediatR;
using Npgsql;

namespace DDDSouthWest.Domain.Features.Account.Admin.ManageProfile.ViewProfile
{
    public class ViewProfileDetail
    {
        public class Query : IRequest<Response>
        {
            public Query(int id)
            {
                Id = id;
            }

            public int Id { get; }
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
                    const string sql =
                        @"SELECT u.givenname, u.familyname, p.Id, p.Twitter, p.Website, p.LinkedIn, p.BioMarkdown, p.BioHtml, p.LastModified
                        FROM profiles AS p LEFT JOIN users AS u ON p.userid = u.id WHERE p.UserId = @Id";

                    var result = await connection.QuerySingleOrDefaultAsync<ProfileDetailModel>(sql, new {message.Id});
                    return new Response
                    {
                        ProfileDetailModel = result
                    };
                }
            }
        }

        public class Response
        {
            public ProfileDetailModel ProfileDetailModel { get; set; }
        }
    }
}