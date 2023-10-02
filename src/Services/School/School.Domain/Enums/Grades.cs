


namespace School.Domain.Enums;

public enum Grades
{
    [LookupLocalization(nameAr:"الصف الاول", nameEn:"Grade 1")]
    One = 1,

    [LookupLocalization(nameAr:"الصف الثاني","Grade 2")]
    Two = 2,

    [LookupLocalization(nameAr: "الصف الثالث", nameEn: "Grade 3")]
    Three = 3,

    [LookupLocalization(nameAr: "الصف الرابع", "Grade 4")]
    Four = 4,

    [LookupLocalization(nameAr: "الصف الخامس", nameEn: "Grade 5")]
    Five = 5,

    [LookupLocalization(nameAr: "الصف السادس", "Grade 6")]
    Six = 6,
    [LookupLocalization(nameAr: "الصف السابع", "Grade 7")]
    Seven = 7,
}
