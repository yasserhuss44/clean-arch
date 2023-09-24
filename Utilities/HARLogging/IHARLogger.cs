namespace HARLogging;
public interface IHarLogger
{
    Task AddEntryFromRequestAsync(
        HttpRequest request,
        HttpResponse response, 
        TimeSpan timeSpan,
        string requestContent, 
        string responseContent);

    Task<bool> CreateNewEntry(
        HttpRequestMessage request ,
        HttpResponseMessage response ,
        TimeSpan requestTime , 
        string requestContent ,
        string responseContent);
}