namespace Shared.Web.Security.Authorization;

public interface IAuthorizationRequirementConfig
{
    string PolicyName { get; }
    int RequestType { get; }
    AuthorizationAccessLevels AuthorizationAccessLevel { get; } 
}