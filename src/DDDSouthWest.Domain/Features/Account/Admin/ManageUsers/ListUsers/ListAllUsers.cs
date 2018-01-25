using System.Collections.Generic;
using System.Linq;
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
                    const string sql = "SELECT GivenName, FamilyName, EmailAddress, IsBlocked, IsActivated, ReceiveNewsletter, DateRegistered, Roles FROM Users ORDER BY dateregistered DESC";
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
            public IList<UsersListModel> Users { get; set; }
        }
    }
}