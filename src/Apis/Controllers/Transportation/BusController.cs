namespace Apis.Controllers.Transportation;

[ApiController]
[Route("Transportation/[controller]")]
public class BusController : BaseController
{

    private readonly ILogger<BusController> logger;
    private readonly IBusService studentService;

    public BusController(ILogger<BusController> logger, IBusService studentService)
    {
        this.logger = logger;
        this.studentService = studentService;
    }
    [HttpGet(nameof(SearchBuses))]
    [ProducesResponseType(typeof(PagedListDto<BusDto>), 200)]
    public async Task<IActionResult> SearchBuses(BusFilter filter, CancellationToken cancellationToken)
    {
        var result = await studentService.SearchBuses(filter, cancellationToken);

        return Ok(result);
    }

    [HttpGet(nameof(GetBus))]
    [ProducesResponseType(typeof(BusDto), 200)]
    public async Task<IActionResult> GetBus(Guid id, CancellationToken cancellationToken)
    {
        var result = await studentService.GetBus(id, cancellationToken);

        return Ok(result);
    }

    [HttpPost(nameof(CreateNewBus))]
    [ProducesResponseType(typeof(bool), 200)]
    public async Task<IActionResult> CreateNewBus(CreateBusDto id, CancellationToken cancellationToken)
    {
        var result = await studentService.CreateNewBus(id, cancellationToken);

        return Ok(result);
    }

    [HttpPut(nameof(UpdateBus))]
    [ProducesResponseType(typeof(bool), 200)]
    public async Task<IActionResult> UpdateBus(UpdateBusDto dto, CancellationToken cancellationToken)
    {
        var result = await studentService.UpdateBus(dto,cancellationToken);

        return Ok(result);
    }

    [HttpDelete(nameof(DeleteBus))]
    [ProducesResponseType(typeof(bool), 200)]
    public async Task<IActionResult> DeleteBus(Guid id, CancellationToken cancellationToken)
    {
        var result = await studentService.DeleteBus(id,cancellationToken);

        return Ok(result);
    }
}
