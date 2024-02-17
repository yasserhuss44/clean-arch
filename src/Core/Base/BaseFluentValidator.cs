using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace Core.Base;

public class BaseFluentValidator<T> : AbstractValidator<T>, IValidatorInterceptor
{
    public ValidationResult AfterAspNetValidation(
        ActionContext actionContext, 
        IValidationContext validationContext,
        ValidationResult result)
    {
        if (result.Errors != null && result.Errors.Any())
            throw new Exceptions.ValidationException(
                                      result.Errors, 
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
