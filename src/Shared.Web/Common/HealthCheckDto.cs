namespace Shared.Web.Common;

public class HealthCheckDto
{
    public string Status { get; set; }
    public int StatusCode { get; set; }
    public string VersionNo { get; set; }
    public string ReleaseDate { get; set; }
}