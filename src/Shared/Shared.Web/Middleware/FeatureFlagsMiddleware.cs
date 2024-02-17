using Microsoft.AspNetCore.Mvc.Controllers;
using Shared.Application.SystemFeatures;

namespace Shared.Web.Middleware;

public class FeatureFlagsMiddleware : IMiddleware
{
    private readonly ISystemFeaturesService systemFeaturesService;
    public FeatureFlagsMiddleware(
        ISystemFeaturesService systemFeaturesService)
        => this.systemFeaturesService = systemFeaturesService;

    public async Task InvokeAsync(
        HttpContext httpContext,
        RequestDelegate next)
    {
        try
        {
            var controllerActionDescriptor = httpContext
                                            .GetEndpoint()
                                            .Metadata
                                            .GetMetadata<ControllerActionDescriptor>();

            var controllerName = controllerActionDescriptor.ControllerName;


            bool isFeatureEnabled;

            isFeatureEnabled = await systemFeaturesService.IsFeatureEnabledAsync(controllerName);

            if (isFeatureEnabled.IsFalsy())
            {
                await UpdateDisabledRouteResponse(httpContext);

                return;
            }
        }
        catch (Exception ex)
        {
            throw new UnhealthyException(ex.ToString());
        }

        await next(httpContext);
    }

    private static async Task UpdateDisabledRouteResponse(
        HttpContext context)
    {
        context.Response.StatusCode = StatusCodes.Status200OK;

        var details = new DefaultExceptionModel(
            ExceptionCodes.DisabledRoute.ToInt(),
            ErrorMessages.RouteDisabled);

        await context.Response.WriteAsJsonAsync(details);

        await context.Response.CompleteAsync();
    }
}