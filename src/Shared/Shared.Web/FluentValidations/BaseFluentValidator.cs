using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;

namespace Shared.Web.FluentValidations;

public class BaseFluentValidator<T> : 
    AbstractValidator<T>, 
    IValidatorInterceptor
{
    public ValidationResult AfterAspNetValidation(
        ActionContext actionContext,
        IValidationContext validationContext,
        ValidationResult result)
    {
        if (result.Errors.NotNullOrEmpty())
            throw new Core.Exceptions.ValidationException(
                result.Errors ,
                "Invalid model: please fix the model issues and try again");

        return result;
    }

    public IValidationContext BeforeAspNetValidation(
        ActionContext actionContext,
        IValidationContext commonContext)
    {
        return commonContext;
    }
}