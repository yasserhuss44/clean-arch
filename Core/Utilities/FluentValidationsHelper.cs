using Core.Exceptions;
using FluentValidation.Results;

namespace Core.Utilities;
public static class FluentValidationsHelper
{
    public static void ThrowInputRequiredValidationExcption(string propertyName)
    {
        throw new ValidationException(
            new List<ValidationFailure>
            {
                new ValidationFailure(propertyName,$"{propertyName} is required")
            } ,
            "Invalid model: please fix the model issues and try again");
    }
}
