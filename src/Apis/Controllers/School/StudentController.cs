namespace Apis.Controllers.School;

[ApiController]
[Route("School/[controller]")]
public class StudentController : BaseController
{

    private readonly ILogger<StudentController> logger;
    private readonly IStudentService studentService;

    public StudentController(ILogger<StudentController> logger, IStudentService studentService)
    {
        this.logger = logger;
        this.studentService = studentService;
    }

    [HttpGet(nameof(SearchStudents))]
    [ProducesResponseType(typeof(PagedListDto<StudentDto>), 200)]
    public async Task<IActionResult> SearchStudents(StudentFilter filter)
    {
        var result = await studentService.SearchStudents(filter);

        return Ok(result);
    }

    [HttpGet(nameof(GetStudent))]
    [ProducesResponseType(typeof(StudentDto), 200)]
    public async Task<IActionResult> GetStudent(Guid id)
    {
        var result = await studentService.GetStudent(id);

        return Ok(result);
    }

    [HttpPost(nameof(CreateNewStudent))]
    [ProducesResponseType(typeof(bool), 200)]
    public async Task<IActionResult> CreateNewStudent(CreateStudentDto dto)
    {
        var result = await studentService.CreateNewStudent(dto);

        return Ok(result);
    }

    [HttpPut(nameof(UpdateStudentNames))]
    [ProducesResponseType(typeof(bool), 200)]
    public async Task<IActionResult> UpdateStudentNames(UpdateStudentDto dto)
    {
        var result = await studentService.UpdateStudentNames(dto);

        return Ok(result);
    }

    [HttpPut(nameof(AssignStudentToGrade))]
    [ProducesResponseType(typeof(bool), 200)]
    public async Task<IActionResult> AssignStudentToGrade(AssignStudentToGradeDto dto)
    {
        var result = await studentService.AssignStudentToGrade(dto);

        return Ok(result);
    }

    [HttpDelete(nameof(DeleteStudent))]
    [ProducesResponseType(typeof(bool), 200)]
    public async Task<IActionResult> DeleteStudent(Guid id)
    {
        var result = await studentService.DeleteStudent(id);

        return Ok(result);
    }
}
