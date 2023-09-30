using Core.Base;
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
        if (value.IsValidEnum<TEnum>())
            return (TEnum)(object)value;

        throw new Exception(ErrorMessages.EnumConversion);
    }

    public static TEnum ToEnum<TEnum>(this int? value)
    {
        if (value.IsValidEnum<TEnum>())
            return (TEnum)(object)value;

        throw new Exception(ErrorMessages.EnumConversion);
    }



    public static List<LookupEntityBase> GetEnumLookups(this Type e)
    {
        Array values = Enum.GetValues(e);
        var list = new List<LookupEntityBase>();
        foreach (int val in values)
        {

            var memInfo = e.GetMember(e.GetEnumName(val));
            var LocalizedAttribute = memInfo[0]
                .GetCustomAttributes(typeof(LookupLocalizationAttribute), false)
                .FirstOrDefault() as LookupLocalizationAttribute;
            var nameAr = memInfo[0].Name;
            var nameEn = memInfo[0].Name;

            if (LocalizedAttribute != null)
            {
                nameAr = LocalizedAttribute.NameAr;
                nameEn = LocalizedAttribute.NameEn;
            }

            list.Add(new LookupEntityBase(val, nameAr, nameEn));

        }
        return list;
    }

}

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
public class LookupLocalizationAttribute : Attribute
{
    public string NameAr { get; set; }
    public string NameEn { get; set; } 

    public LookupLocalizationAttribute(string nameAr, string nameEn)
    {
        NameAr = nameAr;
        NameEn = nameEn;
    }

}

