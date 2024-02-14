namespace Shared.Web.Common;

public class AnonymousEndpointsService :
    IAnonymousEndpointsService,
    ISingltonService    
{
    private readonly string[] AnonymousEndpoints = new[] { "health" };

    public bool IsAnonymous(
        HttpContext context)
    {
        var path = context.Request.Path.Value.TrimStart('/');

        return AnonymousEndpoints.HasAny(x => x.IsEqual(path));
    }
}