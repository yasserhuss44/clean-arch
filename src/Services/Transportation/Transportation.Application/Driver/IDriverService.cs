using Transportation.Application.Drivers.DTOs;

namespace Transportation.Application.Drivers;

public interface IDriverService : IScopedService
{

    Task<PagedListDto<DriverDto>> SearchDrivers(DriverFilter filter);

    Task<DriverDto> GetDriver(Guid id);

    Task<bool> DeleteDriver(Guid id);

    Task<bool> UpdateDriver(UpdateDriverDto dto);

    Task<bool> CreateNewDriver(CreateDriverDto dto);

}