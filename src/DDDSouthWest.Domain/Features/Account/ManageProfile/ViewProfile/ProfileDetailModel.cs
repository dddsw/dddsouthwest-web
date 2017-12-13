namespace DDDSouthWest.Domain.Features.Account.ManageProfile.ViewProfile
{
    public class ProfileDetailModel
    {
        public int Id { get; set; }
 
        public string GivenName { get; set; }

        public string FamilyName { get; set; }

        public string Twitter { get; set; }

        public string Website { get; set; }

        public string LinkedIn { get; set; }

        public string Bio { get; set; }

        public int UserId { get; set; }
    }
}