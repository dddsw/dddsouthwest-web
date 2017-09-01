using System.Threading.Tasks;
using Dapper;
using Npgsql;

namespace DDDSouthWest.Domain.Features.Account.RegisterNewUser
{
    public interface IRegistrationConfirmation
    {
        Task Notify(string messageEmailAddress);
    }

    public class SendEmailConfirmation : IRegistrationConfirmation
    {
        private readonly ClientConfigurationOptions _options;

        public SendEmailConfirmation(ClientConfigurationOptions options)
        {
            _options = options;
        }

        public async Task Notify(string emailAddress)
        {
            if (!_options.WebsiteSettings.RequireNewAccountConfirmation)
                using (var connection = new NpgsqlConnection(_options.Database.ConnectionString))
                {
                    await connection.ExecuteAsync(
                        @"UPDATE users SET IsActivated = TRUE WHERE EmailAddress = @EmailAddress",
                        new {EmailAddress = emailAddress});
                }
        }
    }
}