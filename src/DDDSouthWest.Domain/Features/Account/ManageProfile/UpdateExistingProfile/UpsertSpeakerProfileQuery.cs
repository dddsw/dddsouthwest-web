using System.Threading.Tasks;
using Dapper;
using Npgsql;

namespace DDDSouthWest.Domain.Features.Account.ManageProfile.UpdateExistingProfile
{
    public class UpsertSpeakerProfileQuery
    {
        private readonly ClientConfigurationOptions _options;

        public UpsertSpeakerProfileQuery(ClientConfigurationOptions options)
        {
            _options = options;
        }

        public async Task Invoke(UpsertSpeakerProfile.Command command)
        {
            using (var connection = new NpgsqlConnection(_options.Database.ConnectionString))
            {
                // TODO: This is all a bit ugly, perhaps revise relational model?
                await InsertUserDetails(connection, command);

                if (!await SpeakerProfileExists(connection, command.Id))
                {
                    await InsertSpeakerProfile(connection, command);
                    return;
                }
                
                await UpdateSpeakerProfile(connection, command);
            }
        }

        private static async Task UpdateSpeakerProfile(NpgsqlConnection connection, UpsertSpeakerProfile.Command command)
        {
            const string query =
                "UPDATE Profiles SET Twitter = @Twitter, Website = @Website, LinkedIn = @LinkedIn, BioMarkdown = @BioMarkdown, BioHtml = @BioHtml, LastModified = current_timestamp WHERE UserId = @UserId";

            await connection.ExecuteAsync(query, new
            {
                Twitter = command.Twitter,
                Website = command.Website,
                LinkedIn = command.LinkedIn,
                BioMarkdown = command.BioMarkdown,
                BioHtml = command.BioHtml,
                UserId = command.Id
            });
        }

        private static async Task InsertUserDetails(NpgsqlConnection connection, UpsertSpeakerProfile.Command command)
        {
            const string updateUserQuery =
                "UPDATE Users SET GivenName = @GivenName, FamilyName = @FamilyName WHERE Id = @UserId";

            await connection.ExecuteAsync(updateUserQuery, new
            {
                GivenName = command.GivenName,
                FamilyName = command.FamilyName,
                UserId = command.Id
            });    
        }
        
        private static async Task InsertSpeakerProfile(NpgsqlConnection connection, UpsertSpeakerProfile.Command command)
        {
            const string updateProfileQuery =
                "INSERT INTO Profiles (Twitter, Website, LinkedIn, BioMarkdown, BioHtml, UserId, LastModified) Values (@Twitter, @Website, @LinkedIn, @BioMarkdown, @BioHtml, @UserId, current_timestamp) RETURNING Id";

            await connection.QuerySingleAsync<int>(updateProfileQuery, new
            {
                Twitter = command.Twitter,
                Website = command.Website,
                LinkedIn = command.LinkedIn,
                BioMarkdown = command.BioMarkdown,
                BioHtml = command.BioHtml,
                UserId = command.Id
            });
        }

        private static async Task<bool> SpeakerProfileExists(NpgsqlConnection connection, int userId)
        {
            const string query = "SELECT COUNT(*) FROM Profiles WHERE UserId = @Id";
            var count = await connection.QuerySingleOrDefaultAsync<int>(query, new { Id = userId });
            return count > 0;
        }
    }
}