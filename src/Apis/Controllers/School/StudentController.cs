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
    public async Task<IActionResult> SearchStudents(StudentFilter filter, CancellationToken cancellationToken)
    {
        var result = await studentService.SearchStudents(filter, cancellationToken);

        return Ok(result);
    }

    [HttpGet(nameof(GetStudent))]
    [ProducesResponseType(typeof(StudentDto), 200)]
    public async Task<IActionResult> GetStudent(Guid id, CancellationToken cancellationToken)
    {
        var result = await studentService.GetStudent(id, cancellationToken);

        return Ok(result);
    }

    [HttpPost(nameof(CreateNewStudent))]
    [ProducesResponseType(typeof(bool), 200)]
    public async Task<IActionResult> CreateNewStudent(CreateStudentDto dto, CancellationToken cancellationToken)
    {
        var result = await studentService.CreateNewStudent(dto, cancellationToken);

        return Ok(result);
    }

    [HttpPut(nameof(UpdateStudentNames))]
    [ProducesResponseType(typeof(bool), 200)]
    public async Task<IActionResult> UpdateStudentNames(UpdateStudentDto dto, CancellationToken cancellationToken)
    {
        var result = await studentService.UpdateStudentNames(dto, cancellationToken);

        return Ok(result);
    }

    [HttpPut(nameof(AssignStudentToGrade))]
    [ProducesResponseType(typeof(bool), 200)]
    public async Task<IActionResult> AssignStudentToGrade(AssignStudentToGradeDto dto, CancellationToken cancellationToken)
    {
        var result = await studentService.AssignStudentToGrade(dto, cancellationToken);

        return Ok(result);
    }

    [HttpDelete(nameof(DeleteStudent))]
    [ProducesResponseType(typeof(bool), 200)]
    public async Task<IActionResult> DeleteStudent(Guid id, CancellationToken cancellationToken)
    {
        var result = await studentService.DeleteStudent(id, cancellationToken);

        return Ok(result);
    }
}
