namespace Shared.Web.Middleware;

public class ApiKeyMiddleware : IMiddleware
{
    private readonly IAnonymousEndpointsService anonymousEndpointsService;

    public ApiKeyMiddleware(
        IAnonymousEndpointsService anonymousEndpointsService)
        => this.anonymousEndpointsService = anonymousEndpointsService;

    public async Task InvokeAsync(
        HttpContext context ,
        RequestDelegate next)
    {
        if(anonymousEndpointsService.IsAnonymous(context))
        {
            await next(context);

            return;
        }

        try
        {
            if(!context.Request.Headers.TryGetValue(
                SecurityConstants.ApiKeyHeaderName ,
                out var extractedApiKey))
                throw new UnauthorizedClientException(ErrorMessages.ApiKeyNotProvided);

            var configService = context.RequestServices.GetRequiredService<IConfigService>();

            var apiKeys = configService.ApiKeys;

            var isNotValidApiKey = apiKeys.HasNo(k => k.Equals(extractedApiKey));

            if(isNotValidApiKey)
                throw new UnauthorizedClientException(ErrorMessages.ApiKeyNotValid);
        }
        catch(Exception ex) 
        when(ex is not UnauthorizedClientException)
        {
            throw new UnhealthyException(ex.ToString());
        }

        await next(context);
    }
}