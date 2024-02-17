namespace Logging.Har.Core.Utility;

internal class VersionConverter : JsonConverter
{
    public override bool CanConvert(
        Type objectType)
        => objectType == typeof(Version);

    public override object ReadJson(
        JsonReader reader ,
        Type objectType ,
        object existingValue ,
        JsonSerializer serializer)
        => new Version((string)reader.Value);

    public override void WriteJson(
        JsonWriter writer ,
        object value ,
        JsonSerializer serializer)
        => writer.WriteValue(((Version)value).ToString());
}