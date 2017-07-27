using System.Threading.Tasks;
using Dapper;
using Npgsql;

namespace DDDSouthWest.Domain.Features.Account.ManageEvents.GetEvent
{
    public class QueryEventById
    {
        private readonly ClientConfigurationOptions _options;

        public QueryEventById(ClientConfigurationOptions options)
        {
            _options = options;
        }

        public async Task<EventModel> Invoke(int id)
        {
            using (var connection = new NpgsqlConnection(_options.Database.ConnectionString))
            {
                return await connection.QuerySingleOrDefaultAsync<EventModel>("SELECT Id, EventName, EventFilename FROM events WHERE Id = @id LIMIT 1", new {id});
            }
        }
    }
}