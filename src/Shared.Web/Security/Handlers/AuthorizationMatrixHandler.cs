using Shared.Web.Security.Authorization;

namespace Shared.Web.Security.Handlers;

public class AuthorizationMatrixHandler :
    AuthorizationHandler<AuthorizationMatrixRequirement>,
    IScopedService
{
    private readonly Core.Interfaces.IAuthorizationService authorizationMatrixService;

    public AuthorizationMatrixHandler(
        Core.Interfaces.IAuthorizationService authorizationMatrixService)
        => this.authorizationMatrixService = authorizationMatrixService;

    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context ,
        AuthorizationMatrixRequirement requirement)
    {
        var isAuhtorized = await authorizationMatrixService.IsUserAuthorized(
            requirement.RequestType ,
            requirement.AccessLevel );

        if(isAuhtorized)
        {
            context.Succeed(requirement);
        }

        return;
    }
}