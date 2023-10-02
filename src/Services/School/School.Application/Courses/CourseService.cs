using School.Application.Courses.DTOs;

namespace School.Application.Courses;
public class CourseService : ICourseService
{
    private readonly ISchoolUnitOfWork<SchoolDbContext> unitOfWork;
    private readonly IMapper mapper;

    public CourseService(ISchoolUnitOfWork<SchoolDbContext> unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public Task<PagedListDto<CourseDto>> SearchCourses(CourseFilter filter, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<CourseDto> GetCourse(Guid id, CancellationToken cancellationToken)
    {
        var repository = unitOfWork.GetRepository<Course>();

        var entity = await repository.GetByIdAsync(id, cancellationToken);

        var dto = this.mapper.Map<CourseDto>(entity);

        return dto;
    }

    public async Task<bool> CreateNewCourse(CreateCourseDto dto, CancellationToken cancellationToken)
    {
        var repository = unitOfWork.GetRepository<Course>();

        var course = mapper.Map<Course>(dto);

        repository.Add(course, cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return await Task.FromResult(true);
    }

    public async Task<bool> UpdateCourse(UpdateCourseDto dto, CancellationToken cancellationToken)
    {
        var repository = unitOfWork.GetRepository<Course>();

        var course = await repository.GetByIdAsync(dto.Id , cancellationToken);

        course.Update(name: dto.Name,
                       nameAr: dto.NameAr);

        repository.Update(course, cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return await Task.FromResult(true);
    }

    public async Task<bool> DeleteCourse(Guid id, CancellationToken cancellationToken)
    {
        var repository = unitOfWork.GetRepository<Course>();

        var course = await repository.GetByIdAsync(id, cancellationToken);

        repository.Remove(course, cancellationToken);

        return await Task.FromResult(true);

    }

}

