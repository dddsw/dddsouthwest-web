using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Npgsql;

namespace DDDSouthWest.Domain.Features.Account.RegisterNewUser
{
    public class CreateNewRegisteredUser
    {
        private readonly ClientConfigurationOptions _options;

        public CreateNewRegisteredUser(ClientConfigurationOptions options)
        {
            _options = options;
        }

        public async Task Invoke(RegisterNewUser.Command command)
        {
            string pass = Guid.NewGuid() + command.Password;

            using (var connection = new NpgsqlConnection(_options.Database.ConnectionString))
            {
                const string createUserSql =
                    "INSERT INTO users (EmailAddress, Password, Salt, Blocked, Roles) Values (@EmailAddress, @Password, @Salt, FALSE, '[\"registered\"]') RETURNING Id";
                await connection.QuerySingleAsync<int>(createUserSql,
                    new {command.EmailAddress, Password = pass, Salt = Sha256(pass)});
            }
        }

        private static string Sha256(string input)
        {
            using (var shA256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(input);
                return Convert.ToBase64String(shA256.ComputeHash(bytes));
            }
        }
    }
}