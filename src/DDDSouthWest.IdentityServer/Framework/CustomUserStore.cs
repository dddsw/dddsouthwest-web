using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Dapper;
using IdentityServer4.Test;
using Newtonsoft.Json;
using Npgsql;

namespace DDDSouthWest.IdentityServer.Framework
{
    public class CustomUserStore : IUserStore
    {
        private readonly AuthServerConfigurationOptions _config;

        public CustomUserStore(AuthServerConfigurationOptions config)
        {
            _config = config;
        }

        public virtual bool ValidateCredentials(string username, string password)
        {
            var user = FindByUsername(username);
            if (user.EmailAddress != null || user.IsBlocked || !user.IsActivated)
            {
                var pass = Sha256(user.Salt + password);
                return user.Password.Equals(pass);
            }

            return false;
        }
        
        public UserModel FindBySubjectId(string subjectId)
        {
            var id = int.Parse(subjectId);
            using (var connection = new NpgsqlConnection(_config.Database.ConnectionString))
            {
                var tempUser = connection.QuerySingleOrDefault<UserModelDataMap>(
                    "SELECT Id, EmailAddress, Password, Salt, Roles, FamilyName, GivenName, IsActivated, IsBlocked FROM users WHERE Id = @Id LIMIT 1", new { Id = id });

                //TODO: Do these fields (blocked etc) get serialised into the token?
                var user = new UserModel
                {
                    Id = tempUser.Id,
                    EmailAddress = tempUser.EmailAddress,
                    Password = tempUser.Password,
                    FamilyName = tempUser.FamilyName,
                    GivenName = tempUser.GivenName,
                    IsActivated = tempUser.IsActivated,
                    IsBlocked = tempUser.IsBlocked
                };
                
                // TODO: Change to separate claims to allow separation of name?
                user.Claims.Add(new Claim("name", $"{user.GivenName} {user.FamilyName}"));
                
                var roles = JsonConvert.DeserializeObject<List<string>>(tempUser.Roles);
                foreach (var role in roles)
                {
                    user.Claims.Add(new Claim("role", role));
                }
                
                return user;
            }
        }

        public UserModel FindByUsername(string emailAddress)
        {
            using (var connection = new NpgsqlConnection(_config.Database.ConnectionString))
            {
                var tempUser = connection.QuerySingleOrDefault<UserModelDataMap>(
                    "SELECT Id, EmailAddress, Password, Salt, Roles, FamilyName, GivenName, IsActivated, IsBlocked FROM users WHERE EmailAddress = @Email LIMIT 1", new { Email = emailAddress });

                if (tempUser == null)
                    return null;

                var user = new UserModel
                {
                    Id = tempUser.Id,
                    EmailAddress = tempUser.EmailAddress,
                    Password = tempUser.Password,
                    FamilyName = tempUser.FamilyName,
                    GivenName = tempUser.GivenName,
                    Salt = tempUser.Salt,
                    IsActivated = tempUser.IsActivated,
                    IsBlocked = tempUser.IsBlocked
                };

                // TODO: Change to separate claims to allow separation of name?
                user.Claims.Add(new Claim("name", $"{user.GivenName} {user.FamilyName}"));
                
                var roles = JsonConvert.DeserializeObject<List<string>>(tempUser.Roles);
                foreach (var role in roles)
                {
                    user.Claims.Add(new Claim("role", role));
                }
                
                return user;
            }
        }

        public UserModel FindByExternalProvider(string provider, string userId)
        {
            throw new NotImplementedException();
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