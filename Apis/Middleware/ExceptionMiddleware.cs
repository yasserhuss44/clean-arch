using Microsoft.AspNetCore.Http;

namespace API.Middleware;

public class ExceptionMiddleware : IMiddleware
{
    public async Task InvokeAsync(
        HttpContext httpContext ,
        RequestDelegate next)
    {
        try
        {
            await next(httpContext);
        }
        catch(Exception ex)
        {
            await UpdateUnHealthyResponse(httpContext);

            return;
        }
    }

    private static async Task UpdateUnHealthyResponse(
        HttpContext context)
    {
        context.Response.StatusCode = StatusCodes.Status503ServiceUnavailable;

        var details = new DefaultExceptionModel(
                            ExceptionCodes.Unhealthy.ToInt() ,
                            ErrorMessages.Unhealthy);

        await context.Response.WriteAsJsonAsync(details);

        await context.Response.CompleteAsync();
    }
}
