namespace Core.Exceptions;

public sealed class BusinessRuleException : Exception
{
    public int MessageCode { get; set; }

    public string DisplayMessage { get; set; }

    public BusinessRuleException(
        ExceptionCodes messageCode, 
        string message = null)
        : base(message ?? messageCode.ToString())
    {
        MessageCode = messageCode.ToInt();
        DisplayMessage = message;
    }
}
