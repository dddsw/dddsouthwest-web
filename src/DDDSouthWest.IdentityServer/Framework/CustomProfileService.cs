using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;

namespace DDDSouthWest.IdentityServer.Framework
{
    // https://github.com/IdentityServer/IdentityServer4/blob/75ac815e744051d8150274743cdd8588eb68abb0/src/IdentityServer4/Test/TestUserProfileService.cs
    public class CustomProfileService : IProfileService
    {
        private readonly IUserStore _userStore;

        public CustomProfileService(IUserStore userStore)
        {
            _userStore = userStore;
        }

        public Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            if (context.RequestedClaimTypes.Any())
            {
                var user = _userStore.FindBySubjectId(context.Subject.GetSubjectId());

                if (user != null)
                    context.AddFilteredClaims(user.Claims);
            }

            return Task.FromResult(0);
        }

        public Task IsActiveAsync(IsActiveContext context)
        {
            var user = _userStore.FindBySubjectId(context.Subject.GetSubjectId());
            context.IsActive = user != null;

            return Task.FromResult(0);
        }
    }
}