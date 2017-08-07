using System;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;

namespace DDDSouthWest.IdentityServer.Framework
{
    //https://github.com/IdentityServer/IdentityServer4/blob/dev/src/IdentityServer4/Test/TestUserProfileService.cs
    public class UserProfileService : IProfileService
    {
        private readonly UserStore _userStore;

        public UserProfileService(UserStore userStore)
        {
            _userStore = userStore;
        }
        public Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            if (context.RequestedClaimTypes.Any())
            {
                //var user = _userStore.FindBySubjectId(context.Subject.GetSubjectId());
                /*if (user != null)
                {
                    //context.AddRequestedClaims(user.Claims);
                }*/
            }

            return Task.FromResult(0);
        }

        public Task IsActiveAsync(IsActiveContext context)
        {
            throw new NotImplementedException();
        }
    }
}