using System.Threading.Tasks;
using IdentityServer4.Validation;

namespace DDDSouthWest.IdentityServer.Framework
{
    public class CustomValidator : IResourceOwnerPasswordValidator
    {
        private readonly IUserStore _userStore;

        public CustomValidator(IUserStore userStore)
        {
            _userStore = userStore;
        }

        public Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            if (_userStore.ValidateCredentials(context.UserName, context.Password))
            {
                var user = _userStore.FindByUsername(context.UserName);
                context.Result = new GrantValidationResult(user.SubjectId, "pwd", user.Claims);
            }
            return Task.FromResult(0);
        }
    }
}