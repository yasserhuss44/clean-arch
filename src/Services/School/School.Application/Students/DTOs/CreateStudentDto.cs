namespace School.Application.Students.DTOs;
public class CreateStudentDto : StudentDto
{
    public Nullable<Guid> BusId { get; set; }
}

