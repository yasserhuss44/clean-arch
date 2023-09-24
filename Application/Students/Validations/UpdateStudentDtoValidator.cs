using Application.Students.DTOs;

namespace Application.Students.Validations;

public class UpdateStudentDtoValidator : BaseFluentValidator<UpdateStudentDto>
{
    public UpdateStudentDtoValidator()
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