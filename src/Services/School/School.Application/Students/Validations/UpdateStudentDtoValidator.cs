using School.Application.Students.DTOs;

namespace School.Application.Students.Validations;

public class UpdateStudentDtoValidator : BaseFluentValidator<UpdateStudentDto>
{
    public UpdateStudentDtoValidator()
    {
        RuleFor(x => x.Id)
            .Required();
         
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