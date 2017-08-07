using System;
using System.Threading.Tasks;
using IdentityServer4.Validation;

namespace DDDSouthWest.IdentityServer.Framework
{
    // https://github.com/IdentityServer/IdentityServer4/blob/75ac815e744051d8150274743cdd8588eb68abb0/src/IdentityServer4/Test/TestUserResourceOwnerPasswordValidator.cs
    public class UserValidator : IResourceOwnerPasswordValidator
    {
        private readonly UserStore _userStore;

        public UserValidator(UserStore userStore)
        {
            _userStore = userStore;
        }

        public Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            // Query database here
            var res = 1;
            throw new NotImplementedException();
        }
    }
}