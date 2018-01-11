using System.Threading.Tasks;
using Dapper;
using MediatR;
using Npgsql;

namespace DDDSouthWest.Domain.Features.Account.Admin.ManageNews.DeleteNews
{
    public class DeleteNews
    {
        public class Command : IRequest
        {
            public int Id { get; set; }
        }

        public class Handler : IAsyncRequestHandler<Command>
        {
            private readonly ClientConfigurationOptions _options;

            public Handler(ClientConfigurationOptions options)
            {
                _options = options;
            }

            public async Task Handle(Command message)
            {
                using (var connection = new NpgsqlConnection(_options.Database.ConnectionString))
                {
                    await connection.ExecuteAsync("UPDATE news SET IsDeleted=TRUE WHERE Id = @Id", new
                    {
                        Id = message.Id
                    });   
                }
            }
        }
    }
}