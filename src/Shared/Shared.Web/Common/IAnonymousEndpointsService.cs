namespace Shared.Web.Common;

public interface IAnonymousEndpointsService
{
    bool IsAnonymous(HttpContext context);
}