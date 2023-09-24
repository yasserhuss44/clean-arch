using System.Security.Cryptography;
using System.Text;

namespace Core.Extensions;

public static class StringExtensions
{
    public static bool IsEqual(this string a , string b) => a.Equals(b , StringComparison.OrdinalIgnoreCase);

    public static bool NotEqual(this string a , string b) => !a.IsEqual(b);

    public static bool IsNullOrEmpty(this string str) => string.IsNullOrWhiteSpace(str);

    public static bool NotNullOrEmpty(this string str) => !string.IsNullOrWhiteSpace(str);

    public static string ToBase64String(this string str) => Convert.ToBase64String(Encoding.UTF8.GetBytes(str));

    public static string ToBase64ImageString(this string str) => $"data:image/jpg;base64,{str}";

    public static string FromBase64String(this string str) => Encoding.UTF8.GetString(Convert.FromBase64String(str));

    public static bool ToBool(this string str) => bool.TryParse(str , out var result) && result;

    public static int ToInt(this string str) => int.TryParse(str , out var result) ? result : 0;

    public static float ToFloat(this string str) => float.TryParse(str , out var result) ? result : 0;

    public static long ToLong(this string str) => long.TryParse(str , out var result) ? result : 0;

    public static Guid ToGuid(this string str) => Guid.TryParse(str , out var result) ? result : Guid.Empty;

    public static Guid? ToNullableGuid(this string str) => Guid.TryParse(str , out var result) ? result : null;

    public static bool StartingWith(this string str , string value) => str.StartsWith(value , StringComparison.OrdinalIgnoreCase);

    public static bool NotStartingWith(this string str , string value) => !str.StartingWith(value);

    public static Uri ToUri(this string str)=> new Uri(str);

    /// <summary>
    /// generate number from string using  MD5 Hashing
    /// </summary>
    /// <param name="valueToHash">string to hash</param>
    /// <param name="numberOfCharacters">number of characters to return</param>
    /// <returns>return number of character from the numeric value</returns>
    public static string GenerateHashedNumber(this string valueToHash , int numberOfCharacters = 4)
    {
        var md5Hasher = MD5.Create();

        var hashed = md5Hasher.ComputeHash(Encoding.UTF8.GetBytes(valueToHash));

        var number = (uint)BitConverter.ToInt32(hashed , 0);

        return number.ToString()[..numberOfCharacters];
    }
}
