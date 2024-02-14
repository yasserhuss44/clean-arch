namespace Logging.Har;

public class LoggingHandler : DelegatingHandler
{
    private readonly IHarLogger harLogger;
    private readonly IClockService clockService;
    private readonly ISecurityService securityService;

    public LoggingHandler(
        IHarLogger harLogger ,
        IClockService clockService,
        ISecurityService securityService)
        : base()
    {
        this.harLogger = harLogger;
        this.clockService = clockService;
        this.securityService = securityService;
    }

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request ,
        CancellationToken cancellationToken)
    {
        try
        {
            var startTime = clockService.Now;

            var response = await base.SendAsync(
                request ,
                cancellationToken);

            var requestContent = request.Content.NotNull() ?
                await request.Content!.ReadAsStringAsync(cancellationToken)
                : string.Empty;

            var responseContent = response.Content.NotNull() ?
                await response.Content.ReadAsStringAsync(cancellationToken)
                : string.Empty;

            await harLogger.CreateNewEntry(
                request ,
                response ,
                clockService.Now.Subtract(startTime) ,
                requestContent ,
                responseContent,
                securityService.CurrentUser?.CorrelationId ?? "");

            return response;
        }
        catch(Exception)
        {
            throw;
        }
    }
}
