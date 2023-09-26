namespace Core.Exceptions;

public sealed class ForbiddenAccessException : Exception
{
    public string DisplayMessage { get; set; }
    public int MessageCode { get; set; }

    public ForbiddenAccessException() : base()
    {
        MessageCode = ExceptionCodes.Forbidden.ToInt();
        DisplayMessage = "User is not authorized to perform this action";
    }
}
