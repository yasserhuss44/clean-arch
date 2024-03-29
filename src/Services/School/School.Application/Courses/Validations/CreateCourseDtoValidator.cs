﻿using School.Application.Courses.DTOs;

namespace School.Application.Courses.Validations;

public class CreateCourseDtoValidator : BaseFluentValidator<CreateCourseDto>
{
    public CreateCourseDtoValidator()
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