using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using DDDSouthWest.Domain.Features.Account.Admin.ManageTalks.ListTalks;
using MediatR;
using Npgsql;

namespace DDDSouthWest.Domain.Features.Account.Admin.ManageUsers.ListUsers
{
    public class ListAllUsers
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
                    const string sql = "SELECT u.GivenName, u.FamilyName, u.EmailAddress, u.IsBlocked, u.IsActivated, u.ReceiveNewsletter, u.DateRegistered, u.Roles, p.Twitter, p.Website FROM Users u LEFT JOIN profiles p ON u.Id = p.UserId ORDER BY dateregistered DESC";
                    var users = await connection.QueryAsync<UsersListModel>(sql);

                    return new Response
                    {
                        Users = users.ToList()
                    };
                }
            }
        }

        public class Response
        {
            public Response()
            {
                Users = new List<UsersListModel>();                
            }
            
            public IList<UsersListModel> Users { get; set; }
        }
    }
}