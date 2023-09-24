namespace HARLogging;

public class HarLogger : IHarLogger
{
    private const int _5mb = 1024 * 1024 * 5;
    private const string _jsonMimeType = "application/json; charset=utf-8";
    private const string _defaultPath = ".\\logs\\";
    private string _logFile;
    DateTime _lastWriteTime;
    Har _harObj;
    string _harJson;

    private readonly IConfigService config;
    private readonly IClockService clock;

    public HarLogger(
        IConfigService configService,
        IClockService clockService)
    {
        config = configService;
        clock = clockService;

        CreateNewLogFile();
    }

    public async Task AddEntryFromRequestAsync(
        HttpRequest request,
        HttpResponse response,
        TimeSpan timeSpan,
        string requestContent,
        string responseContent)
    {
        try
        {
            var responseMessage = GetResponseMessage(response);

            var requestMessage = GetRequestMessage(request);

            await CreateNewEntry(
                requestMessage,
                responseMessage,
                timeSpan,
                requestContent,
                responseContent);
        }
        catch (Exception)
        {
        }
    }

    public async Task<bool> CreateNewEntry(
        HttpRequestMessage request,
        HttpResponseMessage response,
        TimeSpan requestTime,
        string requestContent,
        string responseContent)
    {
        var entry = new Entry()
        {
            PageRef = _harObj.Log.Pages.First().Id,
            StartedDateTime = clock.Now,
            ResourceType = "xhr",
            Timings =
            {
                Send = requestTime.TotalMilliseconds.ToInt() ,
                Wait = 0 ,
                Receive = 0 ,
            },
            Time = requestTime.TotalMilliseconds.ToInt(),
            Request =
            {
                Method = request.Method.Method ,
                HeaderSize = 0 ,
                BodySize = requestContent.Length ,
                Url = request.RequestUri! ,
                HttpVersion = $"HTTP/{request.Version.ToString(2)}" ,
                Headers = MapHeaders(request.Headers) ,
                PostData =
                {
                    Text = requestContent ,
                    MimeType = request.Content.NotNull() && request.Content!.Headers.ContentType.NotNull() ?
                               request.Content.Headers.ContentType!.ToString() :
                               _jsonMimeType ,
                }
            },
            Response =
            {
                Status = response.StatusCode.ToInt(),
                StatusText = response.ReasonPhrase!,
                HeaderSize = 0,
                BodySize = responseContent.Length ,
                HttpVersion = $"HTTP/{response.Version.ToString(2)}",
                Headers = MapHeaders(response.Headers),
                Content =
                {
                    MimeType = response.Content.NotNull() && response.Content.Headers.ContentType.NotNull() ?
                                response.Content.Headers.ContentType!.ToString():
                                _jsonMimeType,
                    Text =  responseContent,
                    Size = responseContent.Length
                }
            }
        };

        _harObj.Log.Entries.Add(entry);

        await FlushToFile();

        return true;
    }

    private async Task FlushToFile()
    {
        if (clock.Now
                .Subtract(_lastWriteTime)
                .TotalSeconds <= 5)
            return;

        _lastWriteTime = clock.Now;

        if (_harJson.Length >= _5mb)
            CreateNewLogFile();

        var har = Har.Deserialize(_harJson);

        foreach (var item in _harObj.Log.Entries)
        {
            har.Log.Entries.Add(item);
        }

        _harObj.Log.Entries.Clear();

        _harJson = Har.Serialize(har);

        await File.WriteAllTextAsync(_logFile, _harJson, Encoding.UTF8);
    }

    private void CreateNewLogFile()
    {
        try
        {
            _lastWriteTime = clock.Now;

            var folderPath = config.HarLogFolderPath ?? _defaultPath;

            _logFile = $"{folderPath}log-{clock.Now.ToHarDateTime()}.har";

            _harObj = new Har();

            _harObj.Log.Pages.Add(new Page()
            {
                Id = Guid.NewGuid().ToString(),
                StartedDateTime = clock.Now,
                Title = "Log Page"
            });

            _harJson = Har.Serialize(_harObj);
        }
        catch (Exception)
        {
        }
    }

    private static IEnumerable<Parameter> MapHeaders(
        HttpHeaders httpHeaders)
    {
        if (httpHeaders.IsNullOrEmpty())
            return Enumerable.Empty<Parameter>();

        var parameters = from header in httpHeaders
                         from headerValue in header.Value
                         select new Parameter()
                         {
                             Name = header.Key,
                             Value = headerValue
                         };

        return parameters;
    }

    private static HttpRequestMessage GetRequestMessage(
        HttpRequest request)
    {
        var requestMessage = new HttpRequestMessage()
        {
            RequestUri = request.GetDisplayUrl().ToUri(),
            Method = request.Method.IsEqual(nameof(HttpMethod.Get)) ? HttpMethod.Get : HttpMethod.Post,
        };

        if (request.Headers.IsNullOrEmpty())
            return requestMessage;

        foreach (var item in request.Headers)
        {
            try
            {
                if (item.Key.NotStartingWith("content-"))
                    requestMessage.Headers.Add(item.Key, new string[] { item.Value });
            }
            catch (Exception)
            {
            }
        }

        return requestMessage;
    }


    private HttpResponseMessage GetResponseMessage(
        HttpResponse response)
    {
        var responseMessage = new HttpResponseMessage(response.StatusCode.ToEnum<HttpStatusCode>());

        if (response.Headers.IsNullOrEmpty())
            return responseMessage;

        foreach (var item in response.Headers)
        {
            try
            {
                //TODO:: fix up headers exception
                if (item.Key.NotEqual("content-type"))
                    responseMessage.Headers.Add(item.Key, new string[] { item.Value });
            }
            catch (Exception)
            {
            }
        }

        //TODO::date format 
        if (responseMessage.Headers.HasNo(h => h.Key.IsEqual("date")))
            responseMessage.Headers.Add("Date", new string[] { clock.UtcNow.ToString("ddd, dd MMM yyyy HH:mm:ss zzzz") });

        return responseMessage;
    }
}
