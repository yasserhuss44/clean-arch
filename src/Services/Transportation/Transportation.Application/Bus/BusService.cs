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

    public Task<PagedListDto<BusDto>> SearchBuses(BusFilter filter)
    {
        throw new NotImplementedException();
    }

    public async Task<BusDto> GetBus(Guid id)
    {
        var repository = unitOfWork.GetRepository<Bus>();

        var entity = await repository.GetByIdAsync(id);

        var dto = this.mapper.Map<BusDto>(entity);

        return dto;
    }

    public async Task<bool> CreateNewBus(CreateBusDto dto)
    {
        var repository = unitOfWork.GetRepository<Bus>();

        var bus = mapper.Map<Bus>(dto);

        repository.Add(bus);

        await unitOfWork.SaveChangesAsync();

        return await Task.FromResult(true);
    }

    public async Task<bool> UpdateBus(UpdateBusDto dto)
    {
        var repository = unitOfWork.GetRepository<Bus>();

        var bus = await repository.GetByIdAsync(dto.Id);

        bus.Update(name: dto.Name,
                       nameAr: dto.NameAr);

        repository.Update(bus);

        await unitOfWork.SaveChangesAsync();

        return await Task.FromResult(true);
    }

    public async Task<bool> DeleteBus(Guid id)
    {
        var repository = unitOfWork.GetRepository<Bus>();

        var bus = await repository.GetByIdAsync(id);

        repository.Remove(bus);

        return await Task.FromResult(true);

    }

    public async Task<bool> ReserveBus(Guid busId)
    {
        var repository = unitOfWork.GetRepository<Bus>();

        var bus = await repository.GetByIdAsync(busId);

        if (bus.IsNull())
            throw new Exception("Buss not found");

        bus.Reserve();

        repository.Update(bus);

        await unitOfWork.SaveChangesAsync();

        return await Task.FromResult(true);
    }

    public Task<IEnumerable<BusDto>> GetBuses()
    {
        throw new NotImplementedException();
    }
}

