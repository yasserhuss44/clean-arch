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
    public async Task<IActionResult> SearchBuses(BusFilter filter)
    {
        var result = await studentService.SearchBuses(filter);

        return Ok(result);
    }

    [HttpGet(nameof(GetBus))]
    [ProducesResponseType(typeof(BusDto), 200)]
    public async Task<IActionResult> GetBus(Guid id)
    {
        var result = await studentService.GetBus(id);

        return Ok(result);
    }

    [HttpPost(nameof(CreateNewBus))]
    [ProducesResponseType(typeof(bool), 200)]
    public async Task<IActionResult> CreateNewBus(CreateBusDto id)
    {
        var result = await studentService.CreateNewBus(id);

        return Ok(result);
    }

    [HttpPut(nameof(UpdateBus))]
    [ProducesResponseType(typeof(bool), 200)]
    public async Task<IActionResult> UpdateBus(UpdateBusDto id)
    {
        var result = await studentService.UpdateBus(id);

        return Ok(result);
    }

    [HttpDelete(nameof(DeleteBus))]
    [ProducesResponseType(typeof(bool), 200)]
    public async Task<IActionResult> DeleteBus(Guid id)
    {
        var result = await studentService.DeleteBus(id);

        return Ok(result);
    }
}