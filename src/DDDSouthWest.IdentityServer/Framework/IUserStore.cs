namespace DDDSouthWest.IdentityServer.Framework
{
    public interface IUserStore
    {
        bool ValidateCredentials(string username, string password);
        
        UserModel FindBySubjectId(string subjectId);
        
        UserModel FindByUsername(string emailAddress);
        
        UserModel FindByExternalProvider(string provider, string userId);
    }
}