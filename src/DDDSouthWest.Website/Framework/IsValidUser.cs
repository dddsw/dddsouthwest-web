using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DDDSouthWest.Website.Framework
{
    public class UsernameRequirement : IAuthorizationRequirement
    {
        public string Username { get; set; }

        public UsernameRequirement(string username)
        {
            Username = username;
        }
    }

    public class IsValidUser : AuthorizationHandler<UsernameRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, UsernameRequirement requirement)
        {
            // https://github.com/aspnet/Security/issues/400
            var controllerContext = context.Resource as AuthorizationFilterContext;

            Claim user = context.User.FindFirst(ClaimTypes.Email);
            if (user != null && user.Value == requirement.Username)
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }

            return Task.CompletedTask;
        }
    }
}