namespace Core.Exceptions.Model;

public record DefaultExceptionModel(
    int MessageCode, 
    string Message, 
    bool IsError = true);
