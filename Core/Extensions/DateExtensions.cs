using System.Globalization;

namespace Core.Extensions;

public static class DateExtensions
{
    public const string _dateFormat = "yyyy-MM-dd";

    public const string _dateTimeFormat = "yyyy-MM-dd HH:mm:ss";

    private const string _harLoggerDateTimeFormat = "yyMMddhhmmsss";

    public static string ToCrmDate(this DateOnly date) => date.ToString(_dateFormat);

    public static string ToCrmDate(this DateTime date) => date.ToString(_dateFormat);

    public static string ToCrmDateTimeString(this DateTime date) => date.ToString(_dateTimeFormat);

    /// <summary>
    /// yyMMddhhmmsss
    /// </summary>
    public static string ToHarDateTime(this DateTime date) => date.ToString(_harLoggerDateTimeFormat);

    public static DateTime? ToCrmDate(this string date)
    {
        return date.NotNullOrEmpty() ?
                    DateTime.ParseExact(date , _dateFormat , CultureInfo.InvariantCulture).Date
                    : new DateTime?();
    }

    public static DateOnly? ToCrmDateOnly(this string date)
    {
        return date.NotNullOrEmpty() ?
                    DateOnly.ParseExact(date , _dateFormat , CultureInfo.InvariantCulture)
                    : new DateOnly?();
    }

    public static DateTime? ToCrmDateTime(this string date)
    {
        return date.NotNullOrEmpty() ?
                    DateTime.ParseExact(date , _dateFormat , CultureInfo.InvariantCulture).Date
                    : new DateTime?();
    }

    public static DateOnly? ToDate(this string date)
        => date.NotNullOrEmpty() ? DateOnly.Parse(date) : new DateOnly?();

    public static DateTime? ToDateTime(this string date)
        => date.NotNullOrEmpty() ? DateTime.Parse(date) : new DateTime?();

    /// <summary>
    /// convert DX Dob field into CRM agreed format "yyyy-MM-dd"
    /// </summary>
    /// <param name="dobDate">Date of birth from DX "dd-MM-yyyy"</param>
    /// <returns>"yyyy-MM-dd" string</returns>
    public static string GetCrmDateFromDx(this string dobDate)
    {
        var array = dobDate.Trim().Split('-');

        return $"{array[2]}-{array[1]}-{array[0]}";
    }
}
