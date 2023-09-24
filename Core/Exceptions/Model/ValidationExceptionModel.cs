namespace Core.Exceptions.Model;

public record ValidationExceptionModel(
    int MessageCode,
    string Message,
    IDictionary<string, string[]> Errors = null,
    bool IsError = true);
