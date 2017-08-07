using System.Collections.Generic;
using System.Security.Claims;
using DDDSouthWest.IdentityServer.Quickstart;
using IdentityServer4.Test;

namespace DDDSouthWest.IdentityServer.Framework
{
    public class UserStore
    {
        public bool ValidateCredentials(string username, string password)
        {
            var byUsername = FindByUsername(username);
            if (byUsername != null)
                return byUsername.Password.Equals(password);
            return false;
        }

        public TestUser FindByUsername(string username)
        {
            return new TestUser
            {
                SubjectId = "1",
                Username = "josephwoodward",
                Password = "password",
                Claims = new List<Claim>
                {
                    new Claim("name", "Joseph Woodward"),
                    new Claim("role", Role.Organiser),
                    new Claim("role", Role.Speaker),
                    new Claim("role", Role.Registered)
                }
            };
        }

        public TestUser FindByExternalProvider(string provider, string userId)
        {
            throw new System.NotImplementedException();
        }

        public TestUser AutoProvisionUser(string provider, string userId, List<Claim> claims)
        {
            throw new System.NotImplementedException();
        }

        public TestUser FindBySubjectId(string getSubjectId)
        {
            return new TestUser
            {
                SubjectId = "1",
                Username = "josephwoodward",
                Password = "password",
                Claims = new List<Claim>
                {
                    new Claim("name", "Joseph Woodward"),
                    new Claim("role", Role.Organiser),
                    new Claim("role", Role.Speaker),
                    new Claim("role", Role.Registered)
                }
            };
        }
    }
}