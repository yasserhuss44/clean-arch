using Transportation.Application.Buses.DTOs;

namespace Transportation.Application.Buses.Validations;

public class CreateBusDtoValidator : BaseFluentValidator<CreateBusDto>
{
    public CreateBusDtoValidator()
    { 

        RuleFor(x => x.NameAr)
            .Required()
            .MaximumLength(100);

        RuleFor(x => x.Name)
            .Required()
            .MaximumLength(100);
    }
}