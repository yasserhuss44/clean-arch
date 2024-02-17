namespace Core.Extensions;
public static class CollectionsExtensions
{
    public static string ToBase64String(this byte[] array) => Convert.ToBase64String(array);

    public static string ToBase64ImageString(this byte[] array) => $"data:image/png;base64,{array.ToBase64String()}";

    public static bool IsNullOrEmpty<T>(this IEnumerable<T> list) => list is null || list.Count().IsEmpty();

    public static bool NotNullOrEmpty<T>(this IEnumerable<T> list) => !list.IsNullOrEmpty();

    /// <summary>
    /// convert list of items to separated string 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="separator">default comma</param>
    /// <returns>single string</returns>
    public static string ToSingleString<T>(this IEnumerable<T> list , string separator = ",") 
        => string.Join<T>(separator , list);

    public static bool NotContain<T>(this IEnumerable<T> list , T item) => !list.Contains(item);

    public static bool HasAny<T>(this IEnumerable<T> list , Func<T , bool> expresstion)
        => list.NotNullOrEmpty() && list.Count(expresstion).NotEmpty();

    public static bool HasNo<T>(this IEnumerable<T> list , Func<T , bool> expresstion)
       => list.IsNullOrEmpty() || list.Count(expresstion).IsEmpty();
}

