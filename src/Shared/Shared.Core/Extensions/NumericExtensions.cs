namespace Core.Extensions;

public static class NumericExtensions
{
    public static bool IsEmpty(this int number) => number == 0;

    public static bool NotEmpty(this int number) => !number.IsEmpty();

    public static bool IsEmpty(this int? number) => !number.HasValue || number.Value.IsEmpty();

    public static bool NotEmpty(this int? number) => !number.IsEmpty();

    public static bool IsEmpty(this float number) => number == 0;

    public static bool NotEmpty(this float number) => !number.IsEmpty();

    public static bool IsEmpty(this float? number) => !number.HasValue || number.Value.IsEmpty();

    public static bool NotEmpty(this float? number) => !number.IsEmpty();

    public static int ToInt(this double number) => (int) number;

    public static int ToInt(this float number) => (int)number;

}
