using DDDSouthWest.Domain.Features.Account.Admin.ManageProfile.ViewProfile;

namespace DDDSouthWest.Website.Features.Public.Account.ManageProfile
{
    public class ProfileDetailViewModel
    {
        public ProfileDetailModel Profile { get; set; }
        
        public bool HasProfile { get; set; }
    }
}