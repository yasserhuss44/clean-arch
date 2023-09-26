using System.Text.Json;

namespace Core.Extensions;

public static class ObjectExtensions
{
    public static bool IsNull(this object obj) => obj is null;

    public static bool NotNull(this object obj) => obj is not null;

    public static T ToObject<T>(this object obj)
    {
        if(obj.IsNull())
            throw new ArgumentNullException("cannot convert null to another type");

        var response = JsonSerializer.Deserialize(json: obj.ToString() , typeof(T) , new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true ,
        });

        return (T)response;
    }
}
