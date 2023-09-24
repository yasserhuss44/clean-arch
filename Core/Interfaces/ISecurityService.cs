using Core.Constants;
using Core.Models;

namespace Core.Interfaces;

public interface ISecurityService
{
    /// <summary>
    /// Gets the user.
    /// </summary>
    /// <param name="dxUser">The user from Dx</param>
    /// <returns>user with all profile properties</returns>
    User GetUser(DxUser dxUser);

    /// <summary>
    /// Determines whether current user has role type on current profile or not
    /// </summary>   
    /// <param name="roleType">role of current  user on the current profile</param>
    /// <returns>true if has role type ,false otherwise</returns>
    bool IsCurrentUserHasRoleAndProfileAccess(RoleTypes roleType, bool isExternalUser = false);

    /// <summary>
    /// Determines whether current user has role type on current profile or not
    /// </summary>   
    /// <param name="roleTypes">role list of current  user on the current profile</param>
    /// <returns>true if has role type ,false otherwise</returns>
    bool IsCurrentUserHasRoleAndProfileAccess(RoleTypes[] roleTypes, bool isExternalUser = false);

    bool IsCurrentUserHasRole(RoleTypes[] roleTypes);

    /// <summary>
    /// Gets the current login user.
    /// </summary>   
    User CurrentUser { get; }
}
