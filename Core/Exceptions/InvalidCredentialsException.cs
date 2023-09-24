namespace Core.Exceptions;

public sealed class InvalidCredentialsException : Exception
{
    public string DisplayMessage { get; set; }

    public InvalidCredentialsException(string message = null)
        : base(message)
    {
        DisplayMessage = "user credentials is not valid";
    }
}
