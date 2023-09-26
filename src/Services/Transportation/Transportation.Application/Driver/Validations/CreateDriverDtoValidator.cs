using Transportation.Application.Drivers.DTOs;

namespace Transportation.Application.Drivers.Validations;

public class CreateDriverDtoValidator : BaseFluentValidator<CreateDriverDto>
{
    public CreateDriverDtoValidator()
    { 

        RuleFor(x => x.NameAr)
            .Required()
            .MaximumLength(100);

        RuleFor(x => x.Name)
            .Required()
            .MaximumLength(100);
    }
}