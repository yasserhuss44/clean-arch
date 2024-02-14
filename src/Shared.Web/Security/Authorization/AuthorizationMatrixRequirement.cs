namespace Shared.Web.Security.Authorization;

public record AuthorizationMatrixRequirement(
    int RequestType,
    int AccessLevel)
    : IAuthorizationRequirement;