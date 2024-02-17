namespace Core.Exceptions.Model;
 
public record DefaultExceptionModel(
    int MessageCode, 
    string Message, 
    string CorrelationId = default,
    IDictionary<string , string[]> Errors = null ,
    bool IsError = true);
