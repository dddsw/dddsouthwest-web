using System.Threading.Tasks;
using IdentityServer4.Test;
using IdentityServer4.Validation;

namespace DDDSouthWest.IdentityServer.Framework
{
    /// <summary>Resource owner password validator for test users</summary>
    /// <seealso cref="T:IdentityServer4.Validation.IResourceOwnerPasswordValidator" />
    public class CustomValidator : IResourceOwnerPasswordValidator
    {
        private readonly DummyUserStore _users;

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:IdentityServer4.Test.TestUserResourceOwnerPasswordValidator" />
        ///     class.
        /// </summary>
        /// <param name="users">The users.</param>
        public CustomValidator(DummyUserStore users)
        {
            _users = users;
        }

        /// <summary>Validates the resource owner password credential</summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            if (_users.ValidateCredentials(context.UserName, context.Password))
            {
                var byUsername = _users.FindByUsername(context.UserName);
                context.Result = new GrantValidationResult(byUsername.SubjectId, "pwd", byUsername.Claims, "local", null);
            }
            return Task.FromResult(0);
        }
    }
}