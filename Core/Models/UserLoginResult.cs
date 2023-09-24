namespace Core.Models;

public sealed class UserLoginResult
{
    public UserInfo UserInfo { get; set; }
    public string Token { get; set; }
}
