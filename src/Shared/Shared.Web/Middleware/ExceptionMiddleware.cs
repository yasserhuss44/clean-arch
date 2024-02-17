//using CrmApiException = Infrastructure.Crm.ApiException;
//using ExternalApiException = Infrastructure.External.ApiException;
using ValidationException = Core.Exceptions.ValidationException;

namespace Shared.Web.Middleware;
public class ExceptionMiddleware
{
    private readonly RequestDelegate next;

    public ExceptionMiddleware(
        RequestDelegate next)
        => this.next = next;

    public async Task InvokeAsync(
    HttpContext context,
    ITracingService tracingService,
    ISecurityService securityService)
    {
        try
        {
            tracingService.StartRequest(context);

            await next(context);

            tracingService.EndRequest();
        }
        catch (Exception ex)
        {
            tracingService.EndWithFailure(ex);

            await HandleExceptionAsync(
                securityService.CurrentUser?.CorrelationId ?? "",
                context,
                ex);
        }
    }

    private static async Task HandleExceptionAsync(
        string correlationId,
        HttpContext context,
        Exception ex)
    {
        var statusCode = StatusCodes.Status200OK;

        DefaultExceptionModel details;

        if (ex is UnhealthyException)
        {
            statusCode = StatusCodes.Status503ServiceUnavailable;
            details = new DefaultExceptionModel(
                ExceptionCodes.Unhealthy.ToInt(),
                ErrorMessages.Unhealthy);
        }
        else if (ex is UnauthorizedClientException)
        {
            details = new DefaultExceptionModel(
                ExceptionCodes.Forbidden.ToInt(),
                ex.Message);
        }
        else if (ex is ForbiddenAccessException)
        {
            details = new DefaultExceptionModel(
                ExceptionCodes.Forbidden.ToInt(),
                ErrorMessages.ActionNotAuthorized);
        }

        // else if(ex is CrmApiException)
        // {
        //     details = new DefaultExceptionModel(
        //         ExceptionCodes.CrmConnectionFailed.ToInt() ,
        //         ErrorMessages.CrmConnectionIssue);
        // }
        // else if(ex is ExternalApiException external)
        // {
        //     details = new DefaultExceptionModel(
        //         ExceptionCodes.IntegrationConnectionFailed.ToInt() ,
        //         $"{external.StatusCode} \n {external.Message}");
        // }
        else if (ex is BusinessRuleException business)
        {
            details = new DefaultExceptionModel(
                business.MessageCode,
                business.Message);
        }
        else if (ex is IntegrationsBusinessException integration)
        {
            details = new DefaultExceptionModel(
                integration.MessageCode,
                integration.Message);
        }
        else if (ex is InvalidCredentialsException)
        {
            details = new DefaultExceptionModel(
                ExceptionCodes.InvalidCredentials.ToInt(),
                ex.Message);
        }
        else if (ex is ValidationException validationException)
        {
            details = new DefaultExceptionModel(
                ExceptionCodes.FluentValidation.ToInt(),
                validationException.Message,
                Errors: validationException.Errors);
        }
        else if (ex is NotFoundException)
        {
            details = new DefaultExceptionModel(
                ExceptionCodes.NotFound.ToInt(),
                ex.Message);
        }
        else
        {
            details = new DefaultExceptionModel(
                ExceptionCodes.Unhandled.ToInt(),
                ErrorMessages.Unknown);
        }

        details = details with { CorrelationId = correlationId };

        context.Response.StatusCode = statusCode;

        await context.Response.WriteAsJsonAsync(details);

        await context.Response.CompleteAsync();
    }
}