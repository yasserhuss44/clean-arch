using School.Application.Courses.DTOs;

namespace School.Application.Courses.Validations;

public class CreateCourseDtoValidator : BaseFluentValidator<CreateCourseDto>
{
    public CreateCourseDtoValidator()
    { 

        RuleFor(x => x.NameAr)
            .Required()
            .MaximumLength(100);

        RuleFor(x => x.Name)
            .Required()
            .MaximumLength(100);
    }
}