using Humanizer;
using Transportation.Application.Buses.DTOs;

namespace Transportation.Application.Buses;
public class BusService : IBusService
{
    private readonly ITransportationUnitOfWork<TransportationDbContext> unitOfWork;
    private readonly IMapper mapper;

    public BusService(ITransportationUnitOfWork<TransportationDbContext> unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public Task<PagedListDto<BusDto>> SearchBuses(BusFilter filter ,CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<BusDto> GetBus(Guid id,CancellationToken cancellationToken)
    {
        var repository = unitOfWork.GetRepository<Bus>();

        var entity = await repository.GetByIdAsync(id, cancellationToken);

        var dto = this.mapper.Map<BusDto>(entity);

        return dto;
    }

    public async Task<bool> CreateNewBus(CreateBusDto dto, CancellationToken cancellationToken)
    {
        var repository = unitOfWork.GetRepository<Bus>();

        var bus = mapper.Map<Bus>(dto);

        repository.Add(bus, cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return await Task.FromResult(true);
    }

    public async Task<bool> UpdateBus(UpdateBusDto dto, CancellationToken cancellationToken)
    {
        var repository = unitOfWork.GetRepository<Bus>();

        var bus = await repository.GetByIdAsync(dto.Id, cancellationToken);

        bus.Update(name: dto.Name,
                       nameAr: dto.NameAr);

        repository.Update(bus, cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return await Task.FromResult(true);
    }

    public async Task<bool> DeleteBus(Guid id, CancellationToken cancellationToken)
    {
        var repository = unitOfWork.GetRepository<Bus>();

        var bus = await repository.GetByIdAsync(id, cancellationToken);

        repository.Remove(bus, cancellationToken);

        return await Task.FromResult(true);

    }

    public async Task<bool> ReserveBus(Guid busId, CancellationToken cancellationToken)
    {
        var repository = unitOfWork.GetRepository<Bus>();

        var bus = await repository.GetByIdAsync(busId, cancellationToken);

        if (bus.IsNull())
            throw new Exception("Buss not found");

        bus.Reserve();

        repository.Update(bus, cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return await Task.FromResult(true);
    }

    public Task<IEnumerable<BusDto>> GetBuses( CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

