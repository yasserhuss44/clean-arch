namespace Apis.Controllers.School;

[ApiController]
[Route("School/[controller]")]
public class CourseController : BaseController
{
    private readonly ICourseService courseService;

    public CourseController(ICourseService courseService)
    {
        this.courseService = courseService;
    }

    [HttpGet(nameof(SearchCourses))]
    [ProducesResponseType(typeof(PagedListDto<CourseDto>), 200)]
    public async Task<IActionResult> SearchCourses(CourseFilter filter)
    {
        var result = await courseService.SearchCourses(filter);

        return Ok(result);
    }

    [HttpGet(nameof(GetCourse))]
    [ProducesResponseType(typeof(CourseDto), 200)]
    public async Task<IActionResult> GetCourse(Guid id)
    {
        var result = await courseService.GetCourse(id);

        return Ok(result);
    }

    [HttpPost(nameof(CreateNewCourse))]
    [ProducesResponseType(typeof(bool), 200)]
    public async Task<IActionResult> CreateNewCourse(CreateCourseDto id)
    {
        var result = await courseService.CreateNewCourse(id);

        return Ok(result);
    }

    [HttpPut(nameof(UpdateCourse))]
    [ProducesResponseType(typeof(bool), 200)]
    public async Task<IActionResult> UpdateCourse(UpdateCourseDto id)
    {
        var result = await courseService.UpdateCourse(id);

        return Ok(result);
    }

    [HttpDelete(nameof(DeleteCourse))]
    [ProducesResponseType(typeof(bool), 200)]
    public async Task<IActionResult> DeleteCourse(Guid id)
    {
        var result = await courseService.DeleteCourse(id);

        return Ok(result);
    }
}
