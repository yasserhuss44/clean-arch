using FluentValidation.Results;

namespace Core.Exceptions;

public sealed class ValidationException : Exception
{
    public IDictionary<string , string[]> Errors { get; }
    public string DisplayMessage { get; set; }
    public int MessageCode { get; set; }

    public ValidationException() : base("One or more validation failures have occurred.")
    {
        Errors = new Dictionary<string, string[]>();
        MessageCode = ExceptionCodes.FluentValidation.ToInt();
        DisplayMessage = "One or more validation failures have occurred.";
    }

    public ValidationException(
        IEnumerable<ValidationFailure> failures,
        string displayMessage) : this()
    {
        Errors = failures.GroupBy(e => e.PropertyName, e => e.ErrorMessage)
                         .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());

        DisplayMessage = displayMessage;

        MessageCode = ExceptionCodes.FluentValidation.ToInt();
    }

    public ValidationException(
        IEnumerable<ValidationFailure> failures)
    {
        Errors = failures.GroupBy(e => e.PropertyName , e => e.ErrorMessage)
                         .ToDictionary(failureGroup => failureGroup.Key , failureGroup => failureGroup.ToArray());

        MessageCode = ExceptionCodes.FluentValidation.ToInt();
    }
}