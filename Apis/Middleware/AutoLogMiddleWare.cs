using Microsoft.AspNetCore.Http;

namespace API.Middleware;

public class AutoLogMiddleWare : IMiddleware
{
    //private readonly IHarLogger harLogger;
    //private readonly IConfigService configService;

    //public AutoLogMiddleWare(
    //    IHarLogger harLogger ,
    //    IConfigService configService)
    //{
    //    this.harLogger = harLogger;
    //    this.configService = configService;
    //}

    public async Task InvokeAsync(
        HttpContext context ,
        RequestDelegate next)
    {
        try
        {
            var startTime = DateTime.Now;

            var originalBodyStream = context.Response.Body;

            var requestContent = await GetRequestText(context.Request);

            await next(context);

            if(IsLogDisabled(context))
                return;

            await using var responseBody = new MemoryStream();

            context.Response.Body = responseBody;

            var responseContent = await GetResponseText(context.Response);

            await responseBody.CopyToAsync(originalBodyStream);

           // await harLogger.AddEntryFromRequestAsync(context.Request ,
                                      //context.Response ,
                                      //DateTime.Now.Subtract(startTime) ,
                                      //requestContent ,
                                      //responseContent);

        }
        catch(Exception)
        {
            await next(context);
        }
    }

    private bool IsLogDisabled(HttpContext context)
    {
        return true;//
        //var path = context.Request.Path.Value.TrimStart('/');

        //return configService.IsHealthLogEnabled.IsFalsy()
        //    && path.IsEqual(configService.HealthEndPointName)
        //    && context.Response.StatusCode == StatusCodes.Status200OK;
    }

    private static async Task<string> GetRequestText(
        HttpRequest request)
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

    private static async Task<string> GetResponseText(
        HttpResponse response)
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
}


