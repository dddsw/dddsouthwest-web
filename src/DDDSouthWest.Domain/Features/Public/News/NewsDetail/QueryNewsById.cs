using System.Threading.Tasks;
using Dapper;
using DDDSouthWest.Domain.Features.Account.ManageNews.ViewNewsDetail;
using Npgsql;

namespace DDDSouthWest.Domain.Features.Public.News.NewsDetail
{
    public class QueryLiveNewsById
    {
        private readonly ClientConfigurationOptions _options;

        public QueryLiveNewsById(ClientConfigurationOptions options)
        {
            _options = options;
        }

        public async Task<NewsModel> Invoke(int id)
        {
            using (var connection = new NpgsqlConnection(_options.Database.ConnectionString))
            {
                return await connection.QuerySingleOrDefaultAsync<NewsModel>("SELECT Id, Title, Filename, BodyHtml AS Body, IsLive, DatePosted FROM news WHERE Id = @id AND Live = TRUE LIMIT 1", new {id});
            }
        }
    }
}