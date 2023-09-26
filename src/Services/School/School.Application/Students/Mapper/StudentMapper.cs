using School.Application.Students.DTOs;
 

namespace School.Application.Services.SystemFeatures;

public sealed class StudentMapping : Profile
{
    public StudentMapping()
    {

        CreateMap<Student, StudentDto>();


        CreateMap<CreateStudentDto,Student >();


        CreateMap<UpdateStudentDto,Student >();
    }
}

