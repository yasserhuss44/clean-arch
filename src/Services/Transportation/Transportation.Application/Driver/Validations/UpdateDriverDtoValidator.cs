using Transportation.Application.Drivers.DTOs;

namespace Transportation.Application.Drivers.Validations;

public class UpdateDriverDtoValidator : BaseFluentValidator<UpdateDriverDto>
{
    public UpdateDriverDtoValidator()
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