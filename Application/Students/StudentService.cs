using Application.Students.DTOs;

namespace Application.Students;
public class StudentService : IStudentService
{
    private readonly ISchoolUnitOfWork<SchoolDbContext> unitOfWork;
    private readonly IMapper mapper;

    public StudentService(ISchoolUnitOfWork<SchoolDbContext> unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public Task<PagedListDto<StudentDto>> GetStudents(StudentFilter filter)
    {
        throw new NotImplementedException();
    }

    public async Task<StudentDto> GetStudent(Guid id)
    {
        var repository = unitOfWork.GetRepository<Student>();

        var entity = await repository.GetByIdAsync(id);

        var dto = this.mapper.Map<StudentDto>(entity);

        return dto;
    }

    public async Task<bool> CreateNewStudent(CreateStudentDto dto)
    {
        var repository = unitOfWork.GetRepository<Student>();

        var student = mapper.Map<Student>(dto);

        repository.Add(student);

        await unitOfWork.SaveChangesAsync();

        return await Task.FromResult(true);
    }

    public async Task<bool> UpdateStudent(UpdateStudentDto dto)
    {
        var repository = unitOfWork.GetRepository<Student>();

        var student = await repository.GetByIdAsync(dto.Id);

        student.Update(name: dto.Name,
                       nameAr: dto.NameAr);

        repository.Update(student);

        await unitOfWork.SaveChangesAsync();

        return await Task.FromResult(true);
    }

    public async Task<bool> DeleteStudent(Guid id)
    {
        var repository = unitOfWork.GetRepository<Student>();

        var student = await repository.GetByIdAsync(id);

        repository.Remove(student);

        return await Task.FromResult(true);

    }

}

