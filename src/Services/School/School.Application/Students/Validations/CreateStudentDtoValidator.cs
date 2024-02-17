using School.Application.Students.DTOs;

namespace School.Application.Students.Validations;

public class CreateStudentDtoValidator : BaseFluentValidator<CreateStudentDto>
{
    public CreateStudentDtoValidator()
    { 

        RuleFor(x => x.NameAr)
            .Required()
            .MinimumLength(6)
            .MaximumLength(100);

        RuleFor(x => x.Name)
            .Required()
            .MinimumLength(6)
            .MaximumLength(100);
    }
}