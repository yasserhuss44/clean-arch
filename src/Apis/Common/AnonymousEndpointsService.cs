using Microsoft.AspNetCore.Http;

namespace Apis.Common;

public class AnonymousEndpointsService : ISingltonService, IAnonymousEndpointsService
{
    private readonly string[] AnonymousEndpoints = new[] { "health" };

    public bool IsAnonymous(HttpContext context)
    {
        var path = context.Request.Path.Value.TrimStart('/');

        return AnonymousEndpoints.HasAny(x => x.IsEqual(path));
    }
}
