using Application.Students.DTOs;


namespace Application.Students;

public interface IStudentService : IScopedService
{

    Task<PagedListDto<StudentDto>> GetStudents(StudentFilter filter);

    Task<StudentDto> GetStudent(Guid id);
    
    Task<bool> DeleteStudent(Guid id);
    
    Task<bool> UpdateStudent(UpdateStudentDto dto);
    
    Task<bool> CreateNewStudent(CreateStudentDto dto);

}