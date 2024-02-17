using HttpArchive;

namespace Logging.Har.Core;

/// <summary>
/// Entrypoint for HTTP archive (de)serializing.
/// </summary>
public class HarEntrypoint
{
    private static readonly JsonSerializerSettings SerializerSettings = new()
    {
        DateFormatHandling = DateFormatHandling.IsoDateFormat ,
        Formatting = Formatting.None ,
        NullValueHandling = NullValueHandling.Ignore
    };

    /// <summary>
    /// Initializes a new instance of the <see cref="HarEntrypoint"/> class.
    /// </summary>
    public HarEntrypoint()
    {
        Log = new Log();
    }

    /// <summary>
    /// The root object of a HTTP archive.
    /// </summary>
    [JsonProperty("log")]
    public Log Log { get; set; }

    /// <summary>
    /// Deserialize the HTTP archive string.
    /// </summary>
    /// <param name="har">The HTTP archive string.</param>
    /// <returns>The HTTP archive object.</returns>
    public static HarEntrypoint Deserialize(string har)
        => JsonConvert.DeserializeObject<HarEntrypoint>(har , SerializerSettings);

    /// <summary>
    /// Serialize the HTTP archive object.
    /// </summary>
    /// <param name="har">The HTTP archive object.</param>
    /// <returns>The HTTP archive string.</returns>
    /// <exception cref="InvalidOperationException">If the page ids contain duplicates or unknown page ids are referenced from an entry.</exception>
    public static string Serialize(
        HarEntrypoint har)
    {
        if(har == null)
            throw new ArgumentNullException(nameof(har));

        var countPages = har.Log.Pages.Count();

        var pageIds = har.Log.Pages.Select(p => p.Id).Distinct();

        if(countPages > pageIds.Count())
            throw new InvalidOperationException(Resources.PageIdsNotUnique);

        if(har.Log.Entries.Any(e => !pageIds.Contains(e.PageRef)))
            throw new InvalidOperationException(Resources.EntryPageRefNotFound);

        return JsonConvert.SerializeObject(har , SerializerSettings);
    }
}