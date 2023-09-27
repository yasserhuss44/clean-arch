using AutoMapper;
using Transportation.Application.Buses;

namespace School.Infrastructure.Integration.Transport;

public class TransportProxy : ITransportProxy
{
    private readonly IBusService busService;
    private readonly IMapper mapper;

    public TransportProxy(IBusService busService,IMapper mapper)
    {
        this.busService = busService;
        this.mapper = mapper;
    }
    public async Task<IEnumerable<SchooBusDto>> GetBuses()
    {
        var buses= await this.busService.GetBuses();

        return this.mapper.Map<IEnumerable<SchooBusDto>>(buses);
    }

    public async Task<bool> ReserveBus(Guid busId)
    {
        return await this.busService.ReserveBus(busId);
    }
}

