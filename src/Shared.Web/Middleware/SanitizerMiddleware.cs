using Ganss.Xss;


namespace Shared.Web.Middleware;

/// <summary>
/// middleware to check XSS attacks
/// </summary>
public class SanitizerMiddleware : IMiddleware
{
    public async Task InvokeAsync(
        HttpContext context,
        RequestDelegate next)
    {
        try
        {
            context.Request.EnableBuffering();

            using var streamReader = new StreamReader(context.Request.Body, Encoding.UTF8, leaveOpen: true);

            var raw = await streamReader.ReadToEndAsync();

            // workaround:.net 6 JSON comes through with Unicode brackets. => \u003Cscript\u003E
            raw = raw.Replace("\\u003C", "<")
                     .Replace("\\u003E", ">")
                     .Replace("&", "&amp;");

            var sanitizer = new HtmlSanitizer();

            var sanitized = sanitizer.Sanitize(raw);

            if (raw != sanitized)
            {
                await UpdateResponse(context);

                return;
            }

            context.Request.Body.Seek(0, SeekOrigin.Begin);
        }
        catch (Exception ex)
        {
            throw new UnhealthyException(ex.ToString());
        }

        await next(context);
    }

    private static async Task UpdateResponse(
        HttpContext context)
    {
        context.Response.StatusCode = StatusCodes.Status200OK;

        var details = new DefaultExceptionModel(
            ExceptionCodes.XssViolation.ToInt() ,
            "XSS injection detected , and this is prohibited action");

        await context.Response.WriteAsJsonAsync(details);

        await context.Response.CompleteAsync();
    }
}