using Transportation.Application.Buses.DTOs;


namespace Transportation.Application.Buses;

public interface IBusService : IScopedService
{

    Task<PagedListDto<BusDto>> SearchBuses(BusFilter filter);

    Task<BusDto> GetBus(Guid id);
    
    Task<bool> DeleteBus(Guid id);
    
    Task<bool> UpdateBus(UpdateBusDto dto);
    
    Task<bool> CreateNewBus(CreateBusDto dto);

    Task<bool> ReserveBus(Guid busId);

    Task<IEnumerable<BusDto>> GetBuses();
}