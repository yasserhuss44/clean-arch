 
namespace School.Infrastructure.Integration.Transport;

public interface ITransportProxy:IScopedService
{
    Task<IEnumerable<SchooBusDto>> GetBuses(CancellationToken cancellationToken);
    
    Task<bool> ReserveBus(Guid busId, CancellationToken cancellationToken);

}

