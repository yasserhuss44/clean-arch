using Core.Models;

namespace Core.Interfaces;

public interface IAuthorizationService
{
    Task<bool> IsUserAuthorized(
        int requestTypeId, 
        int accessLevel,
        CancellationToken cancellationToken = default);
}
