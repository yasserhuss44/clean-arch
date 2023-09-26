namespace Core.Exceptions;

public sealed class UnHandledException : Exception
{
    public string DisplayMessage { get; set; }
    public int MessageCode { get; set; }

    public UnHandledException() : base()
    {
        DisplayMessage = "Unknown Error";
        MessageCode = ExceptionCodes.Unhandled.ToInt();
    }
}
