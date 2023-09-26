using School.Application.Students.DTOs;


namespace School.Application.Students;

public interface IStudentService : IScopedService
{

    Task<PagedListDto<StudentDto>> SearchStudents(StudentFilter filter);

    Task<StudentDto> GetStudent(Guid id);
    
    Task<bool> DeleteStudent(Guid id);
    
    Task<bool> UpdateStudent(UpdateStudentDto dto);
    
    Task<bool> CreateNewStudent(CreateStudentDto dto);

}