using Core.Models;

namespace Core.Interfaces;

public interface IAuthorizationService
{
    /// <summary>
    /// get allowed action for requests to be executed
    /// </summary>
    /// <param name="requestTypeId">request type</param>
    /// <returns></returns>
    Task<List<Guid>> GetAllowedRequestActionsAsync(
        int requestTypeId ,
        CancellationToken cancellationToken = default);

    Task<List<int>> GetCurrentUserAllowedRequestTypes(
        CancellationToken cancellationToken = default);

    Task<List<int>> GetCurrentUserAllowedExecutionRequestTypes(
        CancellationToken cancellationToken = default);


    /// <summary>
    /// get authorization matrix based on current user
    /// </summary>
    /// <returns></returns>
    Task<List<AuthorizationMatrixResult>> GetCurrentUserAuthMatrixResult(
        CancellationToken cancellationToken = default);

    /// <summary>
    /// is current user authorized to do this action or not
    /// </summary>
    /// <param name="requestTypeId">request type</param>
    /// <param name="accessLevel">access level required</param>
    /// <returns>true if access granted , otherwise false</returns>
    Task<bool> IsUserAuthorized(
        int requestTypeId, 
        int accessLevel,
        bool isExternal,
        CancellationToken cancellationToken = default);
}
