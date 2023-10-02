using School.Application.Students.DTOs;
using School.Infrastructure.Integration.Transport;
using System.Threading;

namespace School.Application.Students;
public class StudentService : IStudentService
{
    private readonly ISchoolUnitOfWork<SchoolDbContext> unitOfWork;
    private readonly IMapper mapper;
    private readonly ITransportProxy transportProxy;

    public StudentService(ISchoolUnitOfWork<SchoolDbContext> unitOfWork, IMapper mapper, ITransportProxy transportProxy)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
        this.transportProxy = transportProxy;
    }

    public Task<PagedListDto<StudentDto>> SearchStudents(StudentFilter filter, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<StudentDto> GetStudent(Guid id,CancellationToken cancellationToken)
    {
        var repository = unitOfWork.GetRepository<Student>();

        var entity = await repository.GetByIdAsync(id, cancellationToken);

        var dto = this.mapper.Map<StudentDto>(entity);

        return dto;
    }

    public async Task<bool> CreateNewStudent(CreateStudentDto dto, CancellationToken cancellationToken)
    {
        var repository = unitOfWork.GetRepository<Student>();

        var student = mapper.Map<Student>(dto);

        repository.Add(student, cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        if (dto.BusId.HasValue)
            await this.transportProxy.ReserveBus(dto.BusId.Value, cancellationToken);

        return await Task.FromResult(true);
    } 

    public async Task<bool> DeleteStudent(Guid id, CancellationToken cancellationToken)
    {
        var repository = unitOfWork.GetRepository<Student>();

        var student = await repository.GetByIdAsync(id, cancellationToken);

        repository.Remove(student, cancellationToken);

        return await Task.FromResult(true);

    }

    public async Task<bool> UpdateStudentNames(UpdateStudentDto dto, CancellationToken cancellationToken)
    {
        var repository = unitOfWork.GetRepository<Student>();

        var student = await repository.GetByIdAsync(dto.Id, cancellationToken);

        student.UpdateNames(name: dto.Name,
                       nameAr: dto.NameAr);

        repository.Update(student, cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return await Task.FromResult(true);
    }

    public async Task<bool> AssignStudentToGrade(AssignStudentToGradeDto dto, CancellationToken cancellationToken)
    {
        var repository = unitOfWork.GetRepository<Student>();

        var student = await repository.GetByIdAsync(dto.StudentId, cancellationToken);

        student.AssignToGrade(dto.GradeId);

        repository.Update(student, cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return await Task.FromResult(true);
    }
}

