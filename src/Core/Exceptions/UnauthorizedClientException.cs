using Core.Constants;

namespace Core.Exceptions;

public sealed class UnauthorizedClientException : Exception
{
    public string DisplayMessage { get; set; }
    public int MessageCode { get; set; }

    public UnauthorizedClientException() : base()
    {
        MessageCode = ExceptionCodes.Forbidden.ToInt();
        DisplayMessage = ErrorMessages.ApiKeyNotProvided;
    }

    public UnauthorizedClientException(string message) : base(message)
    {
        MessageCode = ExceptionCodes.Forbidden.ToInt();
        DisplayMessage = message;
    }
}
