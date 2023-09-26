using Transportation.Application.Buses.DTOs;

namespace Transportation.Application.Buses.Validations;

public class UpdateBusDtoValidator : BaseFluentValidator<UpdateBusDto>
{
    public UpdateBusDtoValidator()
    {
        RuleFor(x => x.Id)
            .Required();

        //RuleFor(x => x.EntityId)
        //    .RequiredIf(x => x.ComplaintEntityTypeId == EntityRegistrationTypes.Registered.ToInt() && isSubmit);


        RuleFor(x => x.NameAr)
            .Required()
            .MaximumLength(100);

        RuleFor(x => x.Name)
            .Required()
            .MaximumLength(100);
    }
}