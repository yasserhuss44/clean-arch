using School.Application.Students.DTOs;


namespace School.Application.Students;

public interface IStudentService : IScopedService
{
    Task<PagedListDto<StudentDto>> SearchStudents(StudentFilter filter);

    Task<StudentDto> GetStudent(Guid id);
    
    Task<bool> CreateNewStudent(CreateStudentDto dto);
    
    Task<bool> UpdateStudentNames(UpdateStudentDto dto);

    Task<bool> AssignStudentToGrade(AssignStudentToGradeDto dto);

    Task<bool> DeleteStudent(Guid id);

}