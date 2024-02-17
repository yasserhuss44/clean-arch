using Microsoft.AspNetCore.Http;

namespace Core.Interfaces;

public interface ITracingService
{
    void EndRequest();
    void EndWithFailure(Exception ex);
    void LogFailure(Exception ex);
    void StartRequest(HttpContext httpContext);
}