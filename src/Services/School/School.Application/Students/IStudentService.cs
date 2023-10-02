using School.Application.Students.DTOs;


namespace School.Application.Students;

public interface IStudentService : IScopedService
{
    Task<PagedListDto<StudentDto>> SearchStudents(StudentFilter filter, CancellationToken cancellationToken);

    Task<StudentDto> GetStudent(Guid id, CancellationToken cancellationToken);
    
    Task<bool> CreateNewStudent(CreateStudentDto dto, CancellationToken cancellationToken);
    
    Task<bool> UpdateStudentNames(UpdateStudentDto dto, CancellationToken cancellationToken);

    Task<bool> AssignStudentToGrade(AssignStudentToGradeDto dto, CancellationToken cancellationToken);

    Task<bool> DeleteStudent(Guid id, CancellationToken cancellationToken);

}