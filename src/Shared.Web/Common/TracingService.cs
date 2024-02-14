namespace Shared.Web.Common;

public class TracingService : 
    ITracingService, 
    IScopedService
{
    private readonly ILogger<TracingService> logger;
    private readonly ISecurityService securityService;

    public TracingService(
        ILogger<TracingService> logger ,
        ISecurityService securityService)
    {
        this.logger = logger;
        this.securityService = securityService;
    }

    public void StartRequest(
        HttpContext httpContext)
    {
        var requestMetadata = new
        {
            Uri = httpContext.Request.GetDisplayUrl().ToUri() ,
            httpContext.Request.Method
        };

        logger.LogInformation(
            "\r\r***** {CorrelationId} Request Started *****\r{Request}"+
             "\r==========================================================\r" ,
            securityService.CurrentUser?.CorrelationId ?? "" ,
            requestMetadata);
    }

    public void LogFailure(Exception ex)
    {
        logger.LogError(
            "\r\r##### {CorrelationId} Error Happened #####\r{Exception}\r" ,
            securityService.CurrentUser?.CorrelationId ?? "" ,
            ex.ToString());
    }

    public void EndWithFailure(Exception ex)
    {
        logger.LogError(
            "\r\r##### {CorrelationId} Request Failure #####\r{Exception}\r\r" ,
            securityService.CurrentUser?.CorrelationId ?? "" ,
            ex.ToString());
    }

    public void EndRequest()
    {
        logger.LogInformation(
            "\r\r***** {CorrelationId} Request Ended *****" +
            "\r -----------------------------------------------------------\r\r\r" ,
            securityService.CurrentUser?.CorrelationId ?? "");
    }
}