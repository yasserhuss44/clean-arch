 
namespace School.Infrastructure.Integration.Transport;

public interface ITransportProxy:IScopedService
{
    Task<IEnumerable<SchooBusDto>> GetBuses();
    
    Task<bool> ReserveBus(Guid busId);

}

