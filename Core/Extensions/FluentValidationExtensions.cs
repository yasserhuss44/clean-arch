using Core.Utilities;
using FluentValidation;
using System.Linq.Expressions;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace Core.Extensions;

public static class FluentValidationExtensions
{
    private static readonly Regex EnglishLettersOnly = new(@"^[a-zA-Z\s _\-\/]*$");

    private static readonly Regex OnlyLettersAndNumbersRegEx = new(@"^[a-zA-Z\s0-9 _\-\/]*$");

    private static readonly Regex OnlyLettersAndNumbersUsernameRegEx = new(@"^[a-zA-Z\s0-9 @._\-\/]*$");

    private static readonly Regex ArabicLettersOnly = new(@"^[\u0600-\u06FF\u003A\0-9s]{0,4000}$");

    private static readonly Regex ComplexPassword = new(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#$^+=!*()@%&]).{8,}$");

    private static readonly Regex OnlyNumbersRegEx = new(@"^[0-9\.]*$");

    private static readonly Regex EmailAddressRegEx = new(@"^[\w-']+(\.[\w-']+)*@([a-z0-9-]+(\.[a-z0-9-]+)*?\.[a-z]{2,6}|(\d{1,3}\.){3}\d{1,3})(:\d{4})?$");

    private static readonly Regex UrlRegEx = new(@"^[(http(s) ?):\/\/(www\.)?a-zA-Z0-9@:%._\+~#=]{2,256}\.[a-z]{2,6}\b([-a-zA-Z0-9@:%_\+.~#?&//=]*)$");

    private static readonly Regex OnlyUserWithDomainRegEx = new(@"^[a-zA-Z0-9_\\]*$");



    public static IRuleBuilderOptions<T , string> Password<T>(this IRuleBuilder<T , string> ruleBuilder) => ruleBuilder.Matches(ComplexPassword).WithMessage("PasswordIsNotComplex");

    public static IRuleBuilderOptions<T , string> OnlyLetters<T>(this IRuleBuilder<T , string> ruleBuilder) => ruleBuilder.Matches(EnglishLettersOnly).WithMessage("Message_LatinLetters");

    public static IRuleBuilderOptions<T , string> OnlyLettersAndNumbers<T>(this IRuleBuilder<T , string> ruleBuilder) => ruleBuilder.Matches(OnlyLettersAndNumbersRegEx).WithMessage("Message_LatinLettersAndNumber");

    public static IRuleBuilderOptions<T , string> OnlyLettersForUserName<T>(this IRuleBuilder<T , string> ruleBuilder) => ruleBuilder.Matches(OnlyLettersAndNumbersUsernameRegEx).WithMessage("Message_LatinLettersAndNumber");

    public static IRuleBuilderOptions<T , string> OnlyArabicLetters<T>(this IRuleBuilder<T , string> ruleBuilder) => ruleBuilder.Matches(ArabicLettersOnly).WithMessage("OnlyArabicLetters");

    public static IRuleBuilderOptions<T , string> OnlyUserWithDomain<T>(this IRuleBuilder<T , string> ruleBuilder) => ruleBuilder.Matches(OnlyUserWithDomainRegEx).WithMessage("Message_LatinLettersAndNumber");

    public static IRuleBuilderOptions<T , string> NoScriptAllowed<T>(this IRuleBuilder<T , string> ruleBuilder)
    {
        var rule = ruleBuilder.Matches(@"^[^<>{}]+$")
            .WithMessage("ScriptsNotAllowedErrorMessage");
        return rule;
    }

    public static IRuleBuilderOptions<T, Guid> Required<T>(this IRuleBuilder<T, Guid> ruleBuilder)
    {
        var rule = ruleBuilder.NotEmpty()
            .WithMessage("Required");
        return rule;
    }

    public static IRuleBuilderOptions<T , string> Required<T>(this IRuleBuilder<T , string> ruleBuilder)
    {
        var rule = ruleBuilder.NotEmpty()
            .WithMessage("Required");
        return rule;
    }

    public static IRuleBuilderOptions<T , string> RequiredIf<T>(this IRuleBuilder<T , string> ruleBuilder , Func<T , bool> predicate)
    {
        return ruleBuilder.NotEmpty()
        .WithMessage("Required").When(predicate);
    }
    public static IRuleBuilderOptions<T , int?> RequiredIf<T>(this IRuleBuilder<T , int?> ruleBuilder , Func<T , bool> predicate)
    {
        return ruleBuilder.NotEmpty()
        .WithMessage("Required").When(predicate);
    }
    public static IRuleBuilderOptions<T , Guid?> RequiredIf<T>(this IRuleBuilder<T , Guid?> ruleBuilder , Func<T , bool> predicate)
    {

        return ruleBuilder.NotEmpty()
        .WithMessage("Required").When(predicate);
    }

    public static IRuleBuilderOptions<T , bool?> RequiredIf<T>(this IRuleBuilder<T , bool?> ruleBuilder , Func<T , bool> predicate)
    {

        return ruleBuilder.NotEmpty()
        .WithMessage("Required").When(predicate);
    }

    public static IRuleBuilderOptions<T , string> Url<T>(this IRuleBuilder<T , string> ruleBuilder)
    {
        var rule = ruleBuilder.Matches(UrlRegEx)
          .WithMessage("Message_UrlNOtValid");
        return rule;
    }

    public static IRuleBuilderOptions<T , DateTime> Required<T>(this IRuleBuilder<T , DateTime> ruleBuilder)
    {
        var rule = ruleBuilder.NotEmpty()
            .WithMessage("Required");
        return rule;
    }

    public static IRuleBuilderOptions<T , int?> Required<T>(this IRuleBuilder<T , int?> ruleBuilder)
    {
        var rule = ruleBuilder.NotEmpty()
            .WithMessage("Required");
        return rule;
    }

    public static IRuleBuilderOptions<T , long?> Required<T>(this IRuleBuilder<T , long?> ruleBuilder)
    {
        var rule = ruleBuilder.NotEmpty()
            .WithMessage("Required");
        return rule;
    }

    public static IRuleBuilderOptions<T , int> Required<T>(this IRuleBuilder<T , int> ruleBuilder)
    {
        var rule = ruleBuilder.NotEmpty()
            .WithMessage("Required");
        return rule;
    }

    public static IRuleBuilderOptions<T , long> Required<T>(this IRuleBuilder<T , long> ruleBuilder)
    {
        var rule = ruleBuilder.NotEmpty()
            .WithMessage("Required");
        return rule;
    }

    public static IRuleBuilderOptions<T , DateTime?> Required<T>(this IRuleBuilder<T , DateTime?> ruleBuilder)
    {
        var rule = ruleBuilder.NotEmpty()
            .WithMessage("Required");
        return rule;
    }

    public static IRuleBuilderOptions<T , DateTime?> BeforeToDate<T>(this IRuleBuilder<T , DateTime?> ruleBuilder , Expression<Func<T , DateTime?>> toDateExpression)
    {
        var rule = ruleBuilder.LessThan(toDateExpression).WithMessage("StartDateMustBeLessThanOrEqualToEndDate");

        return rule;
    }

    public static IRuleBuilderOptions<T , DateTime> BeforeToday<T>(this IRuleBuilder<T , DateTime> ruleBuilder , bool includeToday = true)
    {
        if(includeToday)
        {
            var rule1 = ruleBuilder.LessThanOrEqualTo(a => DateTime.Now)
                        .WithMessage("BeforeTodayMessage");
            return rule1;
        }
        var rule2 = ruleBuilder.LessThan(a => DateTime.Now)
        .WithMessage("BeforeTodayMessage");
        return rule2;
    }

    public static IRuleBuilderOptions<T , DateTime> DateGreaterThanDate<T>(this IRuleBuilder<T , DateTime> ruleBuilder , DateTime SecondDate)
    {
        var rule = ruleBuilder.GreaterThan(a => SecondDate)
        .WithMessage("MessageDateGreaterThanDate");
        return rule;
    }

    public static IRuleBuilderOptions<T , DateTime> DateLessThanDate<T>(this IRuleBuilder<T , DateTime> ruleBuilder , DateTime SecondDate)
    {
        var rule = ruleBuilder.LessThan(a => SecondDate)
        .WithMessage("MessageDateLessThanDate");
        return rule;
    }

    public static IRuleBuilderOptions<T , DateTime?> DateGreaterThanDate<T>(this IRuleBuilder<T , DateTime?> ruleBuilder , DateTime SecondDate)
    {
        var rule = ruleBuilder.GreaterThan(a => SecondDate)
        .WithMessage("MessageDateGreaterThanDate");
        return rule;
    }

    public static IRuleBuilderOptions<T , DateTime?> DateLessThanDate<T>(this IRuleBuilder<T , DateTime?> ruleBuilder , DateTime SecondDate)
    {
        var rule = ruleBuilder.LessThan(a => SecondDate)
        .WithMessage("MessageDateLessThanDate");
        return rule;
    }

    public static IRuleBuilderOptions<T , DateTime> AfterToday<T>(this IRuleBuilder<T , DateTime> ruleBuilder , bool includeToday = true)
    {
        if(includeToday)
        {
            var rule1 = ruleBuilder.GreaterThanOrEqualTo(a => DateTime.Now)
                        .WithMessage("AfterTodayMessage");
            return rule1;
        }

        var rule2 = ruleBuilder.GreaterThan(a => DateTime.Now)
        .WithMessage("AfterTodayMessage");
        return rule2;
    }

    public static IRuleBuilderOptionsConditions<T , string> IsValidDate<T>(this IRuleBuilder<T , string> ruleBuilder)
    {
        var rule = ruleBuilder.Custom((dateString , context) =>
        {
            try
            {
                dateString.ToCrmDate();
            }
            catch
            {
                context.AddFailure("Date format is invalid.");
            }
        });

        return rule;
    }

    public static IRuleBuilderOptions<T, string> DateIsAfterOrEqual<T>(this IRuleBuilder<T, string> ruleBuilder, Func<T, string> compareTo)
    {
        var rule = ruleBuilder.Must((rootObject, propertyValue, _) =>
        {
            var compareToValue = compareTo(rootObject);

            if (DateTime.TryParse(propertyValue, out var parsedPropertyValue) &&
                DateTime.TryParse(compareToValue, out var parsedCompareToValue))
            {
                return parsedPropertyValue >= parsedCompareToValue;
            }

            return true;
        });

        return rule;
    }


    public static IRuleBuilderOptions<T , decimal> Required<T>(this IRuleBuilder<T , decimal> ruleBuilder)
    {
        var rule = ruleBuilder.NotEmpty()
            .WithMessage("Required");
        return rule;
    }

    public static IRuleBuilderOptions<T , float> Required<T>(this IRuleBuilder<T , float> ruleBuilder)
    {
        var rule = ruleBuilder.NotEmpty()
            .WithMessage("Required");
        return rule;
    }

    public static IRuleBuilderOptions<T , float> GreaterThanZero<T>(this IRuleBuilder<T , float> ruleBuilder)
    {
        var rule = ruleBuilder.GreaterThan(0)
            .WithMessage("Required");
        return rule;
    }

    public static IRuleBuilderOptions<T , float?> GreaterThanZero<T>(this IRuleBuilder<T , float?> ruleBuilder)
    {
        var rule = ruleBuilder.GreaterThan(0)
            .WithMessage("Required");
        return rule;
    }

    public static IRuleBuilderOptions<T , int> GreaterThanZero<T>(this IRuleBuilder<T , int> ruleBuilder)
    {
        var rule = ruleBuilder.GreaterThan(0)
            .WithMessage("Required");
        return rule;
    }

    public static IRuleBuilderOptions<T , int?> GreaterThanZero<T>(this IRuleBuilder<T , int?> ruleBuilder)
    {
        var rule = ruleBuilder.GreaterThan(0)
            .WithMessage("Required");
        return rule;
    }

    public static IRuleBuilderOptions<T , long> GreaterThanZero<T>(this IRuleBuilder<T , long> ruleBuilder)
    {
        var rule = ruleBuilder.GreaterThan(0)
            .WithMessage("Required");
        return rule;
    }

    public static IRuleBuilderOptions<T , long?> GreaterThanZero<T>(this IRuleBuilder<T , long?> ruleBuilder)
    {
        var rule = ruleBuilder.GreaterThan(0)
            .WithMessage("Required");
        return rule;
    }

    public static IRuleBuilderOptions<T , decimal> GreaterThanZero<T>(this IRuleBuilder<T , decimal> ruleBuilder)
    {
        var rule = ruleBuilder.GreaterThan(0)
            .WithMessage("Required");
        return rule;
    }

    public static IRuleBuilderOptions<T , decimal?> GreaterThanZero<T>(this IRuleBuilder<T , decimal?> ruleBuilder)
    {
        var rule = ruleBuilder.GreaterThan(0)
            .WithMessage("Required");
        return rule;
    }

    public static IRuleBuilderOptions<T , string> GreaterThanZero<T>(this IRuleBuilder<T , string> ruleBuilder)
    {
        var rule = ruleBuilder.Matches(@"0^[\d]+$")
            .WithMessage("Required");
        return rule;
    }

    public static IRuleBuilderOptions<T , List<R>> Required<T, R>(this IRuleBuilder<T , List<R>> ruleBuilder)
    {
        var rule = ruleBuilder.NotEmpty()
            .WithMessage("Required");
        return rule;
    }

    public static IRuleBuilderOptions<T , Guid?> Required<T>(this IRuleBuilder<T , Guid?> ruleBuilder)
    {
        var rule = ruleBuilder.NotEmpty()
            .WithMessage("Required");
        return rule;
    }

    public static IRuleBuilderOptions<T , string> OnlyNumbers<T>(this IRuleBuilder<T , string> ruleBuilder)
    {
        var rule = ruleBuilder.Matches(@"^[0-9]*$")
            .WithMessage("NumberOnlyErrorMessage");
        return rule;
    }

    public static IRuleBuilderOptions<T , string> SaudiMobileNumber<T>(this IRuleBuilder<T , string> ruleBuilder)
    {
        var rule = ruleBuilder.Must(a => a.StartsWith("05"))
            .MinimumLength(10)
            .OnlyNumbers()
            .WithMessage("Saudi mobile number must be 10 digits and start with 05");
        return rule;
    }

    public static IRuleBuilderOptions<T , string> MobileNumber<T>(this IRuleBuilder<T , string> ruleBuilder)
    {
        var rule = ruleBuilder.Must(a => a.StartsWith("0"))
            .MinimumLength(10)
            .OnlyNumbers()
            .WithMessage("mobile number must be at least 10 digits and start with 0");

        return rule;
    }

    public static IRuleBuilderOptions<T , string> ValidateCommonRules<T>(this IRuleBuilder<T , string> ruleBuilder , int maximumLength = 100 , string value = "")
    {
        var rule = ruleBuilder
             .MaximumLength(maximumLength).WithMessage(string.Format("MaxLengthMessageError" , maximumLength))
             .NoScriptAllowed()
             .WithMessage("ScriptsNotAllowedErrorMessage");


        return rule;
    }

    public static IRuleBuilderOptions<T , string> MaxLenthCommonRules<T>(this IRuleBuilder<T , string> ruleBuilder , int lenth)
    {
        var rule = ruleBuilder
             .MaximumLength(lenth)
             .WithMessage(string.Format("MaxLengthMessageError" , lenth));
        return rule;
    }

    public static IRuleBuilderOptions<T , string> Email<T>(this IRuleBuilder<T , string> ruleBuilder)
    {
        var rule = ruleBuilder.Must(a => MailAddress.TryCreate(a , out _))
                   .WithMessage("Invalid email address");

        //var rule = ruleBuilder.Matches(EmailAddressRegEx)
        //    .WithMessage("Invalid email address");

        return rule;
    }

    public static IRuleBuilderOptions<T , string> ValidFileExtension<T>(this IRuleBuilder<T , string> ruleBuilder , string[] extensions)
    {
        var rule = ruleBuilder.Must(a =>
        {
            var fileExtension = Path.GetExtension(a.ToLower());
            return extensions.Contains(fileExtension);
        })
        .WithMessage("Invalid file extension");
        return rule;
    }

    public static IRuleBuilderOptions<T , string> ValidFileSize<T>(this IRuleBuilder<T , string> ruleBuilder , int maxFileSize)
    {
        var rule = ruleBuilder.Must(a =>
        {
            var actualSize = FileSizeHelper.GetFileSizeFromBase64String(a , unitsOfMeasurement: UnitsOfMeasurement.MegaByte);

            return actualSize <= maxFileSize;
        })
        .WithMessage("Invalid file extension");

        return rule;
    }
}
