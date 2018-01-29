using System;

namespace DDDSouthWest.Domain.Features.Account.Admin.ManageUsers.ListUsers
{
    public class UsersListModel
    {
        public int Id { get; set; }

        public string FullName => $"{GivenName ?? ""} {FamilyName ?? ""}";
        
        public string GivenName { get ; set; }
        
        public string FamilyName { get; set; }
        
        public string EmailAddress { get; set; }
        
        public bool IsBlocked { get; set; }
        
        public bool IsActivated { get; set; }
        
        public bool ReceiveNewsletter { get; set; }
        
        public DateTime DateRegistered { get; set; }
        
        public string Roles { get; set; }

        public string Twitter { get; set; }

        public string Website { get; set; }
        
        /*var roles = JsonConvert.DeserializeObject<List<string>>(tempUser.Roles);*/
    }
}