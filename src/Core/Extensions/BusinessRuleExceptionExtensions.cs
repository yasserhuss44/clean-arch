using Core.Exceptions;

namespace Core.Extensions;
public static class BusinessRuleExceptionExtensions
{
    public static bool IsNotFound(this BusinessRuleException ex) 
        => ex.MessageCode == ExceptionCodes.NotFound.ToInt();

    public static bool IsCrmOperationFailed(this BusinessRuleException ex) 
        => ex.MessageCode == ExceptionCodes.CrmOperationFailed.ToInt();
}
