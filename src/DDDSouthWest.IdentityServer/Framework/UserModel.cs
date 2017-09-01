using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Security.Claims;

namespace DDDSouthWest.IdentityServer.Framework
{
    public class UserModel
    {
        public UserModel()
        {
            Claims = new Collection<Claim>();
        }

        public int Id { get; set; }

        public string SubjectId => Id.ToString();

        public string GivenName { get; set; }

        public string FamilyName { get; set; }

        public string EmailAddress { get; set; }

        public string Password { get; set; }

        public string Salt { get; set; }
        
        public ICollection<Claim> Claims { get; set; }
        
        public bool IsActivated { get; set; }
        
        public bool IsBlocked { get; set; }
    }
}