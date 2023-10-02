using School.Application.Courses.DTOs;

namespace School.Application.Courses;

public interface ICourseService : IScopedService
{

    Task<PagedListDto<CourseDto>> SearchCourses(CourseFilter filter, CancellationToken cancellationToken);

    Task<CourseDto> GetCourse(Guid id, CancellationToken cancellationToken);

    Task<bool> DeleteCourse(Guid id, CancellationToken cancellationToken);

    Task<bool> UpdateCourse(UpdateCourseDto dto, CancellationToken cancellationToken);

    Task<bool> CreateNewCourse(CreateCourseDto dto, CancellationToken cancellationToken);

}