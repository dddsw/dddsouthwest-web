namespace DDDSouthWest.IdentityServer.Framework
{
    internal sealed class UserModelDataMap
    {
        public int Id { get; set; }

        public string SubjectId => EmailAddress;

        public string GivenName { get; set; }

        public string FamilyName { get; set; }

        public string EmailAddress { get; set; }

        public string Password { get; set; }

        public string Roles { get; set; }

        public string Salt { get; set; }

        public bool IsActivated { get; set; }
        
        public bool IsBlocked { get; set; }
    }
}