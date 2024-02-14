namespace Logging.Har;
public interface IHarLogger
{
    Task AddEntryFromRequestAsync(
        HttpRequest request ,
        HttpResponse response ,
        TimeSpan timeSpan ,
        string requestContent ,
        string responseContent ,
        string correlationId);

    Task<bool> CreateNewEntry(
        HttpRequestMessage request ,
        HttpResponseMessage response ,
        TimeSpan requestTime ,
        string requestContent ,
        string responseContent,
        string correlationId);
}