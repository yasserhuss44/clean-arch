 

namespace Apis.Controllers;

[ApiController]
[Route("[controller]")]
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
        var result = await this.studentService.SearchStudents(filter);

        return Ok(result);
    }

    [HttpGet(nameof(GetStudent))]
    [ProducesResponseType(typeof(StudentDto), 200)]
    public async Task<IActionResult> GetStudent(Guid id)
    {
        var result = await this.studentService.GetStudent(id);

        return Ok(result);
    }

    [HttpPost(nameof(CreateNewStudent))]
    [ProducesResponseType(typeof(bool), 200)]
    public async Task<IActionResult> CreateNewStudent(CreateStudentDto id)
    {
        var result = await this.studentService.CreateNewStudent(id);

        return Ok(result);
    }

    [HttpPut(nameof(UpdateStudent))]
    [ProducesResponseType(typeof(bool), 200)]
    public async Task<IActionResult> UpdateStudent(UpdateStudentDto id)
    {
        var result = await this.studentService.UpdateStudent(id);

        return Ok(result);
    }

    [HttpDelete(nameof(DeleteStudent))]
    [ProducesResponseType(typeof(bool), 200)]
    public async Task<IActionResult> DeleteStudent(Guid id)
    {
        var result = await this.studentService.DeleteStudent(id);

        return Ok(result);
    }
}
