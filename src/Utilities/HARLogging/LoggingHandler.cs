namespace HARLogging;

public class LoggingHandler : DelegatingHandler
{
    private readonly IHarLogger harLogger;
    private readonly IClockService clockService;

    public LoggingHandler(
        IHarLogger harLogger ,
        IClockService clockService)
        : base()
    {
        this.harLogger = harLogger;
        this.clockService = clockService;
    }

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request ,
        CancellationToken cancellationToken)
    {
        try
        {
            var startTime = clockService.Now;

            var response = await base.SendAsync(request , cancellationToken);

            var requestContent = request.Content.NotNull() ? await request.Content!.ReadAsStringAsync(cancellationToken) : string.Empty;

            var responseContent = response.Content.NotNull() ? await response.Content.ReadAsStringAsync(cancellationToken) : string.Empty;

            await harLogger.CreateNewEntry(request , response , clockService.Now.Subtract(startTime) , requestContent , responseContent);

            return response;
        }
        catch(Exception)
        {
            throw;
        }
    }
}
