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

    public Task<PagedListDto<CourseDto>> SearchCourses(CourseFilter filter)
    {
        throw new NotImplementedException();
    }

    public async Task<CourseDto> GetCourse(Guid id)
    {
        var repository = unitOfWork.GetRepository<Course>();

        var entity = await repository.GetByIdAsync(id);

        var dto = this.mapper.Map<CourseDto>(entity);

        return dto;
    }

    public async Task<bool> CreateNewCourse(CreateCourseDto dto)
    {
        var repository = unitOfWork.GetRepository<Course>();

        var course = mapper.Map<Course>(dto);

        repository.Add(course);

        await unitOfWork.SaveChangesAsync();

        return await Task.FromResult(true);
    }

    public async Task<bool> UpdateCourse(UpdateCourseDto dto)
    {
        var repository = unitOfWork.GetRepository<Course>();

        var course = await repository.GetByIdAsync(dto.Id);

        course.Update(name: dto.Name,
                       nameAr: dto.NameAr);

        repository.Update(course);

        await unitOfWork.SaveChangesAsync();

        return await Task.FromResult(true);
    }

    public async Task<bool> DeleteCourse(Guid id)
    {
        var repository = unitOfWork.GetRepository<Course>();

        var course = await repository.GetByIdAsync(id);

        repository.Remove(course);

        return await Task.FromResult(true);

    }

}

