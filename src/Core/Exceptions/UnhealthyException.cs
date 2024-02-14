using Core.Constants;

namespace Core.Exceptions;

public sealed class UnhealthyException : Exception
{
    public string DisplayMessage { get; set; }
    public int MessageCode { get; set; }

    public UnhealthyException() : base()
    {
        MessageCode = ExceptionCodes.Forbidden.ToInt();
        DisplayMessage = ErrorMessages.Unhealthy;
    }

    public UnhealthyException(string message) : base(message)
    {
        MessageCode = ExceptionCodes.Unhealthy.ToInt();
        DisplayMessage = message;
    }
}
