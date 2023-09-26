using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using ValidationException = Core.Exceptions.ValidationException;

namespace API.Filters;

public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
{
    private readonly IDictionary<Type , Action<ExceptionContext>> _exceptionHandlers;
    private readonly ILogger<ApiExceptionFilterAttribute> logger;
    public ApiExceptionFilterAttribute(
        ILogger<ApiExceptionFilterAttribute> Logger)
    {
        _exceptionHandlers = new Dictionary<Type , Action<ExceptionContext>>
        {
            //{typeof(CrmApiException),HandleCrmException },
            //{typeof(Integration.Crm.ApiException<Integration.Crm.ErrorCrmDto>),HandleCrmExceptionWithModel },
            //{typeof(ExternalApiException),HandleIntegrationException },
           // {typeof(Integration.External.ApiException<object>),HandleIntegrationExceptionWithModel },

            {typeof(IntegrationsBusinessException),HandleIntegrationsBusinessException },
            {typeof(ValidationException), HandleValidationException },
            {typeof(BusinessRuleException), HandleBusinessRuleException },
            {typeof(NotFoundException), HandleNotFoundException },
            {typeof(ForbiddenAccessException), HandleForbiddenAccess },
            {typeof(InvalidCredentialsException),HandleInvalidCredentialsException }
        };

        logger = Logger;
    }

    /// <summary>
    /// Called when [exception].
    /// </summary>
    /// <param name="context">The context.</param>
    /// <inheritdoc />
    public override void OnException(
        ExceptionContext context)
    {
        if(context.HttpContext.Response.Headers.ContainsKey("cache-control"))
            context.HttpContext.Response.Headers.Remove("cache-control");

        logger.LogError(context.Exception.ToString());

        HandleException(context);

        base.OnException(context);
    }

    private void HandleException(
        ExceptionContext context)
    {
        var type = context.Exception.GetType();
        if(_exceptionHandlers.ContainsKey(type))
        {
            _exceptionHandlers[type].Invoke(context);
            return;
        }

        if(!context.ModelState.IsValid)
        {
            HandleInvalidModelStateException(context);
            return;
        }

        HandleUnknownException(context);
    }

    private void HandleForbiddenAccess(
        ExceptionContext context)
    {
        var exception = context.Exception as ForbiddenAccessException;

        var details = new DefaultExceptionModel(ExceptionCodes.Forbidden.ToInt() ,
                                                "User is not authorized to perform this action");

        context.Result = new ObjectResult(details)
        {
            StatusCode = StatusCodes.Status200OK
        };

        context.ExceptionHandled = true;
    }

    private void HandleCrmException(
        ExceptionContext context)
    {
        //var exception = context.Exception as CrmApiException;

        //var details = new DefaultExceptionModel(ExceptionCodes.CrmConnectionFailed.ToInt() ,
        //                                        exception.StatusCode + "\n" + exception.Message);

        //context.Result = new ObjectResult(details)
        //{
        //    StatusCode = StatusCodes.Status200OK
        //};

        //context.ExceptionHandled = true;
    }

    private void HandleCrmExceptionWithModel(
        ExceptionContext context)
    {
        //var exception = context.Exception as Integration.Crm.ApiException<Integration.Crm.ErrorCrmDto>;

        //var errorModel = exception.Result;

        //var details = new DefaultExceptionModel(ExceptionCodes.CrmConnectionFailed.ToInt() ,
        //                                        $"HTTP Status Code : {exception.StatusCode}," +
        //                                        $"CRM Error Code : {errorModel.ErrorCode} " +
        //                                        $"\n CRM Error Message : {errorModel.MessageEn} " +
        //                                        $"\n {exception.Message}");

        //context.Result = new ObjectResult(details)
        //{
        //    StatusCode = StatusCodes.Status200OK
        //};

        //context.ExceptionHandled = true;
    }

    private void HandleIntegrationException(
        ExceptionContext context)
    {
    //    var exception = context.Exception as ExternalApiException;

    //    var details = new DefaultExceptionModel(ExceptionCodes.IntegrationConnectionFailed.ToInt() ,
    //                                            exception.StatusCode + "\n" + exception.Message);

    //    context.Result = new ObjectResult(details)
    //    {
    //        StatusCode = StatusCodes.Status200OK
    //    };

    //    context.ExceptionHandled = true;
    }

    private void HandleIntegrationExceptionWithModel(
        ExceptionContext context)
    {
        //var exception = context.Exception as Integration.External.ApiException<object>;

        //var details = new DefaultExceptionModel(ExceptionCodes.IntegrationConnectionFailed.ToInt() ,
        //                                        exception.StatusCode + "\n" + exception.Message);

        //context.Result = new ObjectResult(details)
        //{
        //    StatusCode = StatusCodes.Status200OK
        //};

        //context.ExceptionHandled = true;
    }

    private void HandleBusinessRuleException(
        ExceptionContext context)
    {
        var exception = context.Exception as BusinessRuleException;

        var details = new DefaultExceptionModel(exception.MessageCode ,
                                                exception.MessageCode.ToString());

        context.Result = new ObjectResult(details)
        {
            StatusCode = StatusCodes.Status200OK
        };

        context.ExceptionHandled = true;
    }

    private void HandleIntegrationsBusinessException(
        ExceptionContext context)
    {
        var exception = context.Exception as IntegrationsBusinessException;

        var details = new DefaultExceptionModel(exception.MessageCode ,
                                                exception.Message);

        context.Result = new ObjectResult(details)
        {
            StatusCode = StatusCodes.Status200OK
        };

        context.ExceptionHandled = true;
    }

    private void HandleInvalidCredentialsException(
        ExceptionContext context)
    {
        var exception = context.Exception as InvalidCredentialsException;

        var details = new DefaultExceptionModel(ExceptionCodes.InvalidCredentials.ToInt() ,
                                                exception.DisplayMessage);

        context.Result = new ObjectResult(details)
        {
            StatusCode = StatusCodes.Status200OK
        };

        context.ExceptionHandled = true;
    }

    private void HandleValidationException(
        ExceptionContext context)
    {
        var exception = context.Exception as ValidationException;

        var details = new ValidationExceptionModel(ExceptionCodes.FluentValidation.ToInt() ,
                                                    exception.DisplayMessage ,
                                                    exception.Errors);

        context.Result = new ObjectResult(details)
        {
            StatusCode = StatusCodes.Status200OK
        };

        context.ExceptionHandled = true;
    }

    private static void HandleInvalidModelStateException(
        ExceptionContext context)
    {
        var messageCode = ExceptionCodes.ModelStateValidation.ToInt();

        var details = new DefaultExceptionModel(messageCode ,
                                                "Model State Is Invalid");

        context.Result = new ObjectResult(details)
        {
            StatusCode = StatusCodes.Status200OK
        };

        context.ExceptionHandled = true;
    }

    private void HandleNotFoundException(
        ExceptionContext context)
    {
        var exception = context.Exception as NotFoundException;

        var details = new DefaultExceptionModel(ExceptionCodes.NotFound.ToInt() ,
                                                exception.Message);

        context.Result = new ObjectResult(details)
        {
            StatusCode = StatusCodes.Status200OK
        };

        context.ExceptionHandled = true;
    }

    private static void HandleUnknownException(
        ExceptionContext context)
    {
        var ex = context.Exception as UnHandledException;

        var details = new DefaultExceptionModel(ExceptionCodes.Unhandled.ToInt() ,
                                                "Unknown Error");
        if(ex.NotNull())
            details = new DefaultExceptionModel(ex.MessageCode , ex.Message);

        context.Result = new ObjectResult(details)
        {
            StatusCode = StatusCodes.Status200OK
        };

        context.ExceptionHandled = true;
    }
}
