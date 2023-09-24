namespace Core.Exceptions;

public sealed class NotFoundException : Exception
{
    public string DisplayMessage { get; set; }
    public int MessageCode { get; set; }

    public NotFoundException() : base()
    {
        MessageCode = ExceptionCodes.NotFound.ToInt();
        DisplayMessage = ExceptionCodes.NotFound.ToString();
    }

    public NotFoundException(string message) : base(message)
    {
        MessageCode = ExceptionCodes.NotFound.ToInt();
        DisplayMessage = message;
    }
}
