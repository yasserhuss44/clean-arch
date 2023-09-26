using School.Application.Courses.DTOs;

namespace School.Application.Courses.Validations;

public class UpdateCourseDtoValidator : BaseFluentValidator<UpdateCourseDto>
{
    public UpdateCourseDtoValidator()
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