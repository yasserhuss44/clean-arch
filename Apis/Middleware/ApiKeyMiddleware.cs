using Microsoft.AspNetCore.Http;

namespace API.Middleware;

public class ApiKeyMiddleware : IMiddleware
{
    private readonly IAnonymousEndpointsService anonymousEndpointsService;
    
    public ApiKeyMiddleware(
        IAnonymousEndpointsService anonymousEndpointsService) 
        => this.anonymousEndpointsService = anonymousEndpointsService;

    public async Task InvokeAsync(
        HttpContext context,
        RequestDelegate next)
    {
        if (anonymousEndpointsService.IsAnonymous(context))
        {
            await next(context);

            return;
        }

        if (!context.Request.Headers.TryGetValue(SecurityConstants.ApiKeyHeaderName, out var extractedApiKey))
        {
            await UpdateUnAuthorizedResponse(context, ErrorMessages.ApiKeyNotProvided);

            return;
        }

        var configService = context.RequestServices.GetRequiredService<IConfigService>();

        var apiKeys = configService.ApiKeys;

        var isNotValidApiKey = apiKeys.HasNo(k => k.Equals(extractedApiKey));

        if (isNotValidApiKey)
        {
            await UpdateUnAuthorizedResponse(context, ErrorMessages.ApiKeyNotValid);

            return;
        }

        await next(context);
    }

    private static async Task UpdateUnAuthorizedResponse(
        HttpContext context,
        string message)
    {
        context.Response.StatusCode = StatusCodes.Status401Unauthorized;

        var details = new DefaultExceptionModel(ExceptionCodes.Forbidden.ToInt(), message);

        await context.Response.WriteAsJsonAsync(details);

        await context.Response.CompleteAsync();
    }
}
