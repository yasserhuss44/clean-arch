using School.Application.Courses.DTOs;

namespace School.Application.Courses;

public interface ICourseService : IScopedService
{

    Task<PagedListDto<CourseDto>> SearchCourses(CourseFilter filter);

    Task<CourseDto> GetCourse(Guid id);

    Task<bool> DeleteCourse(Guid id);

    Task<bool> UpdateCourse(UpdateCourseDto dto);

    Task<bool> CreateNewCourse(CreateCourseDto dto);

}