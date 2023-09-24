namespace Core.Models;

public class AuthorizationMatrixResult
{
    public int RequestType { get; set; }

    public List<int> AccessLevels { get; set; }
}
