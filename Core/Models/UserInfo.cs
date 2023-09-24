using System.Security.Claims;

namespace Core.Models;

public sealed class UserInfo
{
    public string UserId { get; set; }

    public IEnumerable<Claim> Claims { get; set; }
}
