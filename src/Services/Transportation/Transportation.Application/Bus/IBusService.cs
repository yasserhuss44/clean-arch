using Transportation.Application.Buses.DTOs;


namespace Transportation.Application.Buses;

public interface IBusService : IScopedService
{

    Task<PagedListDto<BusDto>> SearchBuses(BusFilter filter, CancellationToken cancellationToken);

    Task<BusDto> GetBus(Guid id, CancellationToken cancellationToken);
    
    Task<bool> DeleteBus(Guid id, CancellationToken cancellationToken);
    
    Task<bool> UpdateBus(UpdateBusDto dto, CancellationToken cancellationToken);
    
    Task<bool> CreateNewBus(CreateBusDto dto, CancellationToken cancellationToken);

    Task<bool> ReserveBus(Guid busId, CancellationToken cancellationToken);

    Task<IEnumerable<BusDto>> GetBuses(CancellationToken cancellationToken);
}