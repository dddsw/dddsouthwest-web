using Microsoft.AspNetCore.Authorization;

namespace DDDSouthWest.Website.Framework
{
    public class OrganiserAuthAttribute : AuthorizeAttribute
    {
        public OrganiserAuthAttribute() : base("IsValidUser")
        {
        }
    }
}