using System.Threading;

namespace Apis.Controllers.Transportation;

[ApiController]
[Route("Transportation/[controller]")]
public class DriverController : BaseController
{

    private readonly ILogger<DriverController> logger;
    private readonly IDriverService studentService;

    public DriverController(ILogger<DriverController> logger, IDriverService studentService)
    {
        this.logger = logger;
        this.studentService = studentService;
    }
    [HttpGet(nameof(SearchDrivers))]
    [ProducesResponseType(typeof(PagedListDto<DriverDto>), 200)]
    public async Task<IActionResult> SearchDrivers(DriverFilter filter,CancellationToken cancellationToken)
    {
        var result = await studentService.SearchDrivers(filter, cancellationToken);

        return Ok(result);
    }

    [HttpGet(nameof(GetDriver))]
    [ProducesResponseType(typeof(DriverDto), 200)]
    public async Task<IActionResult> GetDriver(Guid id, CancellationToken cancellationToken)
    {
        var result = await studentService.GetDriver(id, cancellationToken);

        return Ok(result);
    }

    [HttpPost(nameof(CreateNewDriver))]
    [ProducesResponseType(typeof(bool), 200)]
    public async Task<IActionResult> CreateNewDriver(CreateDriverDto id, CancellationToken cancellationToken)
    {
        var result = await studentService.CreateNewDriver(id, cancellationToken);

        return Ok(result);
    }

    [HttpPut(nameof(UpdateDriver))]
    [ProducesResponseType(typeof(bool), 200)]
    public async Task<IActionResult> UpdateDriver(UpdateDriverDto id, CancellationToken cancellationToken)
    {
        var result = await studentService.UpdateDriver(id,cancellationToken);

        return Ok(result);
    }

    [HttpDelete(nameof(DeleteDriver))]
    [ProducesResponseType(typeof(bool), 200)]
    public async Task<IActionResult> DeleteDriver(Guid id, CancellationToken cancellationToken)
    {
        var result = await studentService.DeleteDriver(id,cancellationToken);

        return Ok(result);
    }
}
