using School.Application.Courses.DTOs;
 

namespace School.Application.Services.SystemFeatures;

public sealed class CourseMapping : Profile
{
    public CourseMapping()
    {

        CreateMap<Course, CourseDto>();


        CreateMap<CreateCourseDto,Course >();


        CreateMap<UpdateCourseDto,Course >();
    }
}

