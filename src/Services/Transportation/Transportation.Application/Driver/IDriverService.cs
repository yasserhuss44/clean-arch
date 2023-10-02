using Transportation.Application.Drivers.DTOs;

namespace Transportation.Application.Drivers;

public interface IDriverService : IScopedService
{

    Task<PagedListDto<DriverDto>> SearchDrivers(DriverFilter filter, CancellationToken cancellationToken);

    Task<DriverDto> GetDriver(Guid id, CancellationToken cancellationToken);

    Task<bool> DeleteDriver(Guid id, CancellationToken cancellationToken);

    Task<bool> UpdateDriver(UpdateDriverDto dto, CancellationToken cancellationToken);

    Task<bool> CreateNewDriver(CreateDriverDto dto, CancellationToken cancellationToken);

}