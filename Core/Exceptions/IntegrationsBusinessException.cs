namespace Core.Exceptions;

public sealed class IntegrationsBusinessException : Exception
{
    public int MessageCode { get; set; }

    public string DisplayMessage { get; set; }

    public IntegrationsBusinessException(
        ExceptionCodes messageCode, 
        string message) : base(message)
    {
        MessageCode = messageCode.ToInt();
        DisplayMessage = message;
    }
}
