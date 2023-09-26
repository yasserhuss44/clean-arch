using Core.Constants;

namespace Core.Extensions;

public static class EnumExtensions
{
    public static bool IsValidEnum<TEnum>(this int value)
        => typeof(TEnum).IsEnumDefined(value);

    public static bool IsValidEnum<TEnum>(this int? value)
        => value.NotNull() && typeof(TEnum).IsEnumDefined(value);

    public static int ToInt(this Enum payLoad)
    {
        return payLoad != null ?
            (int)(IConvertible)payLoad :
            throw new Exception(ErrorMessages.EnumConversion);
    }

    public static string ToNumericString(this Enum payLoad) 
        => payLoad.ToInt().ToString();

    public static TEnum ToEnum<TEnum>(this int value)
    {
        if(value.IsValidEnum<TEnum>())
            return (TEnum)(object)value;

        throw new Exception(ErrorMessages.EnumConversion);
    }

    public static TEnum ToEnum<TEnum>(this int? value)
    {
        if(value.IsValidEnum<TEnum>())
            return (TEnum)(object)value;

        throw new Exception(ErrorMessages.EnumConversion);
    }
}