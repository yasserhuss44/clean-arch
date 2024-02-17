using Microsoft.AspNetCore.Authorization.Policy;

namespace Shared.Web.Security.Middleware;

public class AuthorizationResultMiddleware : 
    IAuthorizationMiddlewareResultHandler, 
    ISingltonService
{
    private readonly IAuthorizationMiddlewareResultHandler _handler;

    public AuthorizationResultMiddleware()
        => _handler = new AuthorizationMiddlewareResultHandler();

    public async Task HandleAsync(
        RequestDelegate requestDelegate ,
        HttpContext httpContext ,
        AuthorizationPolicy authorizationPolicy ,
        PolicyAuthorizationResult policyAuthorizationResult)
    {
        if(policyAuthorizationResult.Forbidden)
        {
            httpContext.Response.StatusCode = StatusCodes.Status200OK;

            var result = new DefaultExceptionModel(ExceptionCodes.Forbidden.ToInt() ,
                ErrorMessages.ActionNotAuthorized);

            await httpContext.Response.WriteAsJsonAsync(result);

            await httpContext.Response.CompleteAsync();

            return;
        }

        await _handler.HandleAsync(
            requestDelegate ,
            httpContext ,
            authorizationPolicy ,
            policyAuthorizationResult);
    }
}