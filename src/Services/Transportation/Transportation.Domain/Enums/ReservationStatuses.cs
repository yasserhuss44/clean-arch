

namespace Transportation.Domain.Enums;

public enum ReservationStatuses
{
    [LookupLocalization(nameAr:"محجوز", nameEn:"Reserved")]
    Reserved = 100,

    [LookupLocalization(nameAr:"غير محجوز", "Not Reserved")]

    NotReserved = 200
}
