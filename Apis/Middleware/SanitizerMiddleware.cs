using Ganss.Xss;
using Microsoft.AspNetCore.Http;

namespace API.Middleware;

/// <summary>
/// middleware to check XSS attacks
/// </summary>
public class SanitizerMiddleware : IMiddleware
{
    public async Task InvokeAsync(
        HttpContext context ,
        RequestDelegate next)
    {
        // enable buffering so that
        // the request can be read by the model binders next
        context.Request.EnableBuffering();

        // leaveOpen: true to leave the stream open after disposing,
        // so it can be read by the model binders
        using var streamReader = new StreamReader(context.Request.Body , Encoding.UTF8 , leaveOpen: true);

        var raw = await streamReader.ReadToEndAsync();

        // workaround:.net 6 JSON comes through with Unicode brackets. => \u003Cscript\u003E
        raw = raw.Replace("\\u003C" , "<")
                 .Replace("\\u003E" , ">")
                 .Replace("&", "&amp;");

        var sanitizer = new HtmlSanitizer();

        var sanitized = sanitizer.Sanitize(raw);

        //if(raw != sanitized)
        //{
        //    await UpdateResponse(context);

        //    return;
        //}

        // rewind the stream for the next middleware
        context.Request.Body.Seek(0 , SeekOrigin.Begin);

        await next.Invoke(context);
    }

    private static async Task UpdateResponse(
        HttpContext context)
    {
        context.Response.StatusCode = StatusCodes.Status200OK;

        var details = new DefaultExceptionModel(ExceptionCodes.XssViolation.ToInt() ,
            "XSS injection detected , and this is prohibited action");

        await context.Response.WriteAsJsonAsync(details);

        await context.Response.CompleteAsync();
    }
}
