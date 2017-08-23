using System.Threading.Tasks;
using Dapper;
using DDDSouthWest.Domain.Features.Account.ManageEvents.GetEvent;
using Npgsql;

namespace DDDSouthWest.Domain.Features.Account.ManageNews.ViewNewsDetail
{
    public class QueryAnyNewsById
    {
        private readonly ClientConfigurationOptions _options;

        public QueryAnyNewsById(ClientConfigurationOptions options)
        {
            _options = options;
        }

        public async Task<NewsModel> Invoke(int id)
        {
            using (var connection = new NpgsqlConnection(_options.Database.ConnectionString))
            {
                return await connection.QuerySingleOrDefaultAsync<NewsModel>("SELECT Id, Title, Filename, Body, IsLive, DatePosted FROM news WHERE Id = @id LIMIT 1", new {id});
            }
        }
    }
}