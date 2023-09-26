using Core.Models;

namespace Core.Interfaces;

public interface IAuthenticationService
{
    Task<UserLoginResult> AuthenticateAsync(DxUser dxUser, CancellationToken cancellationToken);
}
