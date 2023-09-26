using Microsoft.AspNetCore.Http;

namespace Apis.Common;

public interface IAnonymousEndpointsService
{
    bool IsAnonymous(HttpContext context);
}
