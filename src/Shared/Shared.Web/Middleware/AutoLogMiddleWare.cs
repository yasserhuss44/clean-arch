using Logging.Har;

namespace Shared.Web.Middleware;

public class AutoLogMiddleWare
{
    private readonly RequestDelegate next;
    private readonly IHarLogger harLogger;
    private readonly IConfigService configService;

    public AutoLogMiddleWare(
        IHarLogger harLogger ,
        IConfigService configService ,
        RequestDelegate next)
    {
        this.harLogger = harLogger;
        this.configService = configService;
        this.next = next;
    }

    public async Task InvokeAsync(
        HttpContext context ,
        ITracingService tracingService ,
        ISecurityService securityService)
    {
        var startTime = DateTime.Now;

        var originalBodyStream = context.Response.Body;

        await using var responseBody = new MemoryStream();

        var requestContent = string.Empty;

        requestContent = await GetRequestText(
            context.Request ,
            tracingService);

        context.Response.Body = responseBody;


        await next(context);

        if(IsLogDisabled(context))
            return;

        var responseContent = await GetResponseText(
            context.Response ,
            tracingService);

        await responseBody.CopyToAsync(originalBodyStream);

        await harLogger.AddEntryFromRequestAsync(
            context.Request ,
            context.Response ,
            DateTime.Now.Subtract(startTime) ,
            requestContent ,
            responseContent ,
            securityService.CurrentUser?.CorrelationId ?? "");
    }

    private bool IsLogDisabled(
        HttpContext context)
    {
        try
        {
            var path = context.Request.Path.Value.TrimStart('/');

            return configService.IsHealthLogEnabled.IsFalsy()
                && path.IsEqual(configService.HealthEndPointName)
                && context.Response.StatusCode == StatusCodes.Status200OK;
        }
        catch
        {
            return false;
        }
    }

    private static async Task<string> GetRequestText(
        HttpRequest request ,
        ITracingService tracingService)
    {
        try
        {
            request.EnableBuffering();

            // Leave the body open
            // so the next middleware can read it.
            using var reader = new StreamReader(
                request.Body ,
                encoding: Encoding.UTF8 ,
                detectEncodingFromByteOrderMarks: false ,
                bufferSize: 64000 ,
                leaveOpen: true);

            var body = await reader.ReadToEndAsync();

            // Reset the request body stream position
            // so the next middleware can read it
            request.Body.Position = 0;

            return body;
        }
        catch(Exception ex)
        {
            tracingService.LogFailure(ex);

            return default;
        }
    }

    private static async Task<string> GetResponseText(
        HttpResponse response ,
        ITracingService tracingService)
    {
        try
        {
            response.Body.Seek(0 , SeekOrigin.Begin);

            //Create stream reader to write entire stream     
            using var reader = new StreamReader(
                response.Body ,
                encoding: Encoding.UTF8 ,
                detectEncodingFromByteOrderMarks: false ,
                bufferSize: 64000 ,
                leaveOpen: true);

            var body = await reader.ReadToEndAsync();

            response.Body.Seek(0 , SeekOrigin.Begin);

            return body;
        }
        catch(Exception ex)
        {
            tracingService.LogFailure(ex);
            return default;
        }
    }
}